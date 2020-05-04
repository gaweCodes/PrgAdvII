namespace Task1_4_TypeConstraints
{
    internal class NewConstraint<T>
    {
        public T GetInstance<T>() where T : new() => new T();
    }
}
