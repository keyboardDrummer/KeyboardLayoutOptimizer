namespace Generic.Maths.Vectors
{
    interface IVector<T> where T : IVector<T>
    {
        T Substract(T t);
        float Distance(T t);
    }
}
