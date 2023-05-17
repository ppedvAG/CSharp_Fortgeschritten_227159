namespace PluginBase;

/// <summary>
/// Attribut: Klasse in eckiger Klammer über Typ oder Member
/// Attribute können keinen Code haben, sie können nur über Reflection erkannt werden und damit den Code des Benutzers beeinflussen
/// Über AttributeUsage festlegen aus was das Attribut angehängt werden kann
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
public class ReflectionVisible : Attribute { }
