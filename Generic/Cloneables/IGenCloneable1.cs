

using System;

namespace Generic.Cloneables
{
    public interface IGenCloneable1<out Container,Element> where Container : IGenCloneable1<Container,Element>
    {
        Container Clone(Func<Element, Element> cloneElement); 
    }
}
