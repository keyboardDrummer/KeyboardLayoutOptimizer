namespace Generic.Cloneables
{
    public interface IGenCloneable<out T>
    {
        T Clone();
    }
}
