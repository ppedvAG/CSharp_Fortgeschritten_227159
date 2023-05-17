namespace PluginBase;

/// <summary>
/// PluginBase als Dependency zu dem Plugin und dem Client hinzufügen (optional)
/// </summary>
public interface IPlugin
{
	string Name { get; }

	string Description { get; }

	string Version { get; }

	string Author { get; }
}