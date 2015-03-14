using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Generic.Common;
using Generic.Containers.Collections.List;
using Generic.Containers.Collections.Set;
using Generic.InputOutput.Printing.Sizable;
using Generic.InputOutput.Printing.Sized;
using Generic.Containers.Collections.Dictionaries;

namespace Generic.InputOutput.Printing
{
    public static class DocumentUtil
    {
        public static readonly Document LeftParens = "(";
        public static readonly Document RightParens = ")";
        public static readonly Document LeftBrace = "{";
        public static readonly Document RightBrace = "}";
        public static readonly Document Semi = ";";
        public static readonly Document LeftBracket = "[";
        public static readonly Document RightBracket = "]";
        public static readonly Document Colon = ":";
        public static readonly Document Space = " ";
        public static readonly Document Empty = new WrappedDocument(SizedDocument.Empty);
        public static readonly Document Comma = ',';

        public static Document Print(this string text)
        {
            return new WrappedDocument(new Text(text));
        }
         
        public static Document Print(IEnumerable list)
        {
            return list.Cast<object>().Select<object, Document>(Print).Seperated(", ").InParens();
        }

        public static Document Print(this char chr)
        {
            return chr.ToString();
        }

        public static Document Print(this double dbl)
        {
            return dbl.ToString("F3",CultureInfo.InvariantCulture);
        }

        public static Document Print(this object obj)
        {
            return GenericPrint(obj, new HashSet<object>(new PointerEqualityComparer()));
        }

        public static Document InParens(this Document document)
        {
            return LeftParens + document + RightParens;
        }

        public static Document InBraces(this Document document)
        {
            return LeftBrace + document + RightBrace;
        }


        public static Document InBrackets(this Document document)
        {
            return LeftBracket + document + RightBracket;
        }

        public static Document Seperated<T, U>(this IEnumerable<T> docs, U seperator)
        {
            return docs.Aggregate(Empty, 
                (current, doc) => current.Concat(Print(seperator), Print(doc)));
        }

        public static Document HorizontalConcat(this IEnumerable<object> docs)
        {
            return docs.Aggregate(Empty, (current, doc) => current + doc.Print());
        }

        public static Document VerticalConcat(this IEnumerable<object> docs)
        {
            return docs.Aggregate(Empty, (current, doc) => current ^ doc.Print());
        }

        public static Document Parameters<T>(this IEnumerable<T> arguments)
        {
            return Seperated(arguments, ",").InParens();
        }

        public static Document Wrap<T>(this IList<T> objects)
        {
            return new WrappedList(objects.SelectList(obj => Print(obj)));
        }

        static DocumentUtil()
        {
            AddAnyObjectPrinter();
            AddTypePrinter<double>((_, p) => p.Print());
            AddTypePrinter<string>((_, p) => p);
            AddTypePrinter<int>((_, p) => p.ToString(CultureInfo.InvariantCulture));
            AddTypePrinter<char>((_, p) => p.ToString(CultureInfo.InvariantCulture));
            AddTypePrinter<bool>((_, p) => p.ToString(CultureInfo.InvariantCulture));
            AddTypePrinter<IPrintable>((_, p) => p.Print());
            AddTypePrinter<IEnumerable<bool>>((closed, enumerable) => Print(enumerable.Select(elem => elem.Print()).ToList()));
            AddTypePrinter<IEnumerable<object>>((closed, enumerable) => Print(enumerable.Select(elem => GenericPrint(elem, closed)).ToList()));
            AddTypePrinter<IEnumerable<int>>((closed, enumerable) => Print(enumerable.Select(elem => Print(elem)).ToList()));
            AddTypePrinter<IEnumerable<double>>((closed, enumerable) => Print(enumerable.Select(elem => Print(elem)).ToList()));
            AddTypePrinter<IList<Document>>((_,list) => Print(list));
            
            genericTypePrinters[typeof(IEnumerable<>)] = (set, obj, type) => 
                {
                    var getEnumerator = type.GetMethod("GetEnumerator");
                    var rator = getEnumerator.Invoke(obj,new object[]{});
                    var oldRatorType = typeof(IEnumerator);
                    var genericRatorType = typeof(IEnumerator<>);
                    var ratorType = genericRatorType.MakeGenericType(type.GetGenericArguments());
                    var current = ratorType.GetProperty("Current");
                    var moveNext = oldRatorType.GetMethod("MoveNext");
                    var documents = new List<Document>();
                    while ((bool)moveNext.Invoke(rator, new object[0]))
                    {
                        Console.Write("");
                        documents.Add(Print(current.GetValue(rator, new object[0])));
                    }
                    return documents.Print();
                };
        }

        static void AddAnyObjectPrinter()
        {
            AddTypePrinter<object>((closed, obj) =>
            {
                if (!closed.Add(obj))
                    return "closed";

                var type = obj.GetType();
                Document document = type.Name;
                foreach (var field in type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(prop => prop.GetIndexParameters().Length == 0))
                {
                    try
                    {
                        document = document ^ (field.Name + Colon - GenericPrint(field.GetValue(obj, null), closed));
                    }
                    catch(PrintException)
                    {
                        throw;
                    }
                    catch (Exception)
                    {
                    }
                }
                closed.Remove(obj);
                return document;
            });
        }

        public static Document Print(this IList<Document> list)
        {
            Document commaSep = ", ";
            var documents = list.Count == 0 ? Empty : Wrap(list.TakeList(list.Count - 1).SelectList(elem => (Print(elem) + commaSep))
                .ConcatList(Print(list[list.Count - 1]).Singleton()));
            return documents.InParens();
        }

        static void AddTypePrinter<T>(Func<ISet<object>, T, Document> printer)
        {
            typePrinters.Add(Tuple.Create<Type,Func<ISet<object>,object,Document>>(typeof(T), (c,p) => printer(c,(T)p)));
        }

        static readonly IList<Tuple<Type, Func<ISet<object>, object, Document>>> typePrinters = new List<Tuple<Type, Func<ISet<object>, object, Document>>>();
        static readonly IDictionary<Type, Func<ISet<object>, object, Type, Document>> genericTypePrinters 
            = new Dictionary<Type, Func<ISet<object>, object, Type, Document>>(); 
        static Document GenericPrint(object obj, ISet<object> closed)
        {
            if (obj == null)
                return "null";

            var type = obj.GetType();

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Tuple<,>))
            {
                var item1 = type.GetProperty("Item1");
                var item2 = type.GetProperty("Item2");
                return (GenericPrint(item1.GetValue(obj,null), closed) + Comma - GenericPrint(item2.GetValue(obj,null), closed)).InParens();
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Tuple<,,>))
            {
                var item1 = type.GetProperty("Item1");
                var item2 = type.GetProperty("Item2");
                var item3 = type.GetProperty("Item3");
                return (GenericPrint(item1.GetValue(obj, null), closed) + 
                    Comma - GenericPrint(item2.GetValue(obj, null), closed) +
                    Comma - GenericPrint(item3.GetValue(obj, null),closed)).InParens();
            }

            //if (type.IsGenericType)
            //{
            //    var genericValidPrinters = genericTypePrinters.Keys.Where(
            //        k => k.MakeGenericType(type.GetGenericArguments()).IsAssignableFrom(type)).ToHashSet();
            //    var genericRootPrinters = genericValidPrinters.Where(t => !genericValidPrinters.Any(u => t != u && t.IsAssignableFrom(u))).ToList();
            //    if (genericRootPrinters.Count > 1)
            //    {
            //        throw new InvalidOperationException(("Ambiguous type printer of type" - type.Print()
            //            ^ "type options were" ^ genericRootPrinters.VerticalConcat()).ToString());
            //    }
            //    if (genericRootPrinters.Any())
            //        return genericTypePrinters[genericRootPrinters.First()](closed, obj, type);
            //}

            var validPrinters = typePrinters.Where(k => k.Item1.IsAssignableFrom(type)).ToHashSet();
            var rootPrinters = validPrinters.Where(t => !validPrinters.Any(u => t.Item1 != u.Item1 && t.Item1.IsAssignableFrom(u.Item1))).ToList();
            //if (rootPrinters.Count > 1)
            //{
            //    throw new PrintException(("Ambiguous type printer of type" - type.Print()
            //        ^ "type options were" ^ rootPrinters.VerticalConcat()).ToString());
            //}
            return rootPrinters.First().Item2(closed,obj);
        }

        class PrintException : Exception
        {
            public PrintException(string message)
                : base(message)
            {
            }
        }
    }
}
