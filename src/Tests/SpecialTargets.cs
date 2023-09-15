// ReSharper disable UnusedTypeParameter
namespace MyNamespace
{
    class TypeInNamespace
    {
        class NestedType;
    }
}

public class GenericTarget<T1>
{
    public void Method()
    {
    }

    public T1 GenericMethod1<T3>() => default!;

    public void Overload()
    {
    }

    public void Overload(string a, string b)
    {
    }

    public void Overload(string a, double b)
    {
    }
}

public class GenericTarget<T1, T2>
{
    public void Method()
    {
    }

    public T1 GenericMethod2<T3>() => default!;
}

public class OuterType
{
    private class NestedType
    {
        private class NestedNestedType;
    }
}