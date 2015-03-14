namespace Generic.Cloneables
{
    public static class CloneableUtil
    {
        public static Container Clone1<Container,Element>(this Container container) 
            where Element : IGenCloneable<Element> 
            where Container : IGenCloneable1<Container,Element> 
        {
            return container.Clone(e => e.Clone());
        }
    }
}
