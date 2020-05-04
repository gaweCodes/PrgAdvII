namespace Task1_4_TypeConstraints
{
    internal class MyClass<T> where T : MyClass<T>
    {
    }
    internal class MyConcreteClass : MyClass<MyConcreteClass>
    {
    }

}
