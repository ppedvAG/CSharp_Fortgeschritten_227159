using PluginBase;
using System.Reflection;

namespace PluginClient;

internal class Program
{
	static void Main(string[] args)
	{
		//Pfade sollten aus einer Config kommen (z.B. Json oder Xml)
		Assembly loaded = Assembly.LoadFrom(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_05_15\PluginCalculator\bin\Debug\net7.0\PluginCalculator.dll");

		Type pt = loaded.GetTypes().First(e => e.GetInterface(typeof(IPlugin).Name) != null); //Finde den ersten Typen der das Interface hat

		IPlugin plugin = Activator.CreateInstance(pt) as IPlugin; //Objekt vom Typ IPlugin erstellen

        Console.WriteLine($"Name: {plugin.Name}");
        Console.WriteLine($"Desc: {plugin.Description}");
        Console.WriteLine($"Version: {plugin.Version}");
        Console.WriteLine($"Autor: {plugin.Author}");

		Console.WriteLine();

		List<MethodInfo> methoden = pt.GetMethods().Where(e => e.GetCustomAttribute<ReflectionVisible>() != null).ToList();
        Console.WriteLine("Wähle eine Methode aus: ");
        for (int i = 0; i < methoden.Count; i++)
		{
			Console.WriteLine($"{i}: {methoden[i].Name}");
		}

		int auswahl = int.Parse(Console.ReadLine());

		List<object> inputValues = new();
		foreach (ParameterInfo par in methoden[auswahl].GetParameters())
		{
			Console.WriteLine($"{par.Name} ({par.ParameterType}) eingeben: ");
			object input = Convert.ChangeType(Console.ReadLine(), par.ParameterType);
			inputValues.Add(input);
		}

		Console.WriteLine(methoden[auswahl].Invoke(plugin, inputValues.ToArray()));
	}
}