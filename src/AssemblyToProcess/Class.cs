using System.Globalization;

namespace AssemblyToProcess;

public class Class
{
    double field;

    public void Method()
    {
        var x = "SomeText" + field.ToString(CultureInfo.CurrentCulture);
        Trace.WriteLine(x);
    }

    public int Property { get; set; }
}
