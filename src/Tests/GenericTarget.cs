public class GenericTarget<T1>
{
    public void Method()
    {
    }

    public T1 GenericMethod1<T3>() => default!;
}

public class GenericTarget<T1, T2>
{
    public void Method()
    {
    }

    public T1 GenericMethod2<T3>() => default!;
}