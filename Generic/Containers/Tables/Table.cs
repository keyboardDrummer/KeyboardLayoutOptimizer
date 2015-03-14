using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generic.Containers.Collections.Enumerables;
using Generic.Containers.Collections.Enumerators;
using Generic.InputOutput.Printing;
using Generic.InputOutput.Printing.Sizable;
using Generic.Maths.Vectors;

namespace Generic.Containers.Tables
{
    public class Table<T> : DefaultEnumerable<T>, IPrintable
    {
        readonly T[,] data;
        readonly IntVector2 size;

        public Table(IntVector2 size) : this(new T[size.X,size.Y],size)
        {
        }

        public Table(T[,] data, IntVector2 size)
        {
            this.data = data;
            this.size = size;
        }

        public IntVector2 Size
        {
            get { return size; }
        }

        public T this[IntVector2 point]
        {
            get { return data[point.X, point.Y]; }
        }

        public T this[int x,int y]
        {
            get { return data[x, y]; }
            set { data[x, y] = value; }
        }

        public IEnumerable<IntVector2> Points
        {
            get
            {
                for (int y = 0; y < size.Y; y++)
                {
                    for (var x = 0; x < size.X; x++)
                        yield return new IntVector2(x, y);
                }
            }
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return Points.Select(point => this[point]).GetEnumerator();
        }

        public Table<T> Reverse()
        {
            var result = new Table<T>(new IntVector2(size.Y, size.X));
            foreach (var point in Points)
                result[point.Y, point.X] = this[point];
            return result;
        }

        public Document Print()
        {
            return Print(t => t.Print());
        }

        public Document Print(Func<T,Document> printT)
        {
            var documentWriter = new DocumentWriter();
            for (int y = 0; y < size.Y; y++)
            {
                for (var x = 0; x < size.X; x++)
                {
                    documentWriter.Write(printT(this[x, y]));
                }
                documentWriter.WriteLine();
            }
            return documentWriter.ToDocument();
        }
    }
}
