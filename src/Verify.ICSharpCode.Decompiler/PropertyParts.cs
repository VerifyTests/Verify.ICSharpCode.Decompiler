namespace VerifyTests;

[Flags]
public enum PropertyParts
{
    /// <summary>
    /// Disassembles only the property definition
    /// </summary>
    Definition = 0,
    /// <summary>
    /// Disassembles the property getter
    /// </summary>
    Getter = 1,
    /// <summary>
    /// Disassembles the property setter
    /// </summary>
    Setter = 2,
    /// <summary>
    /// Disassembles both the property getter and setter
    /// </summary>
    GetterAndSetter = Getter | Setter,
}