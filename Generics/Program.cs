namespace Generics;

internal class Program
{
	static void Main(string[] args)
	{
		List<int> list = new List<int>(); //Generic: T wird nach unten übernommen (hier T = int)
		list.Add(1); //T wird durch int ersetzt

		List<string> listStr = new();
		listStr.Add("123");

		Dictionary<string, int> dict = new(); //Klasse mit 2 Generics: TKey -> string, TValue -> int
		dict.Add("123", 123); //Add(TKey, TValue) -> Add(string, int)
	}
}

public class DataStore<T> :
	IProgress<T>, //T bei Vererbung weitergeben
	IEquatable<int> //Fixen Typ bei Vererbung übergeben
{
	private T[] data { get; set; } //T bei einem Feld/Property

	public List<T> Data => data.ToList(); //Generic wird nach unten gegeben

	public void Add(int index, T item) //T bei Parameter
	{
		data[index] = item;
	}

	public T Get(int index)
	{
		if (index < 0 || index > data.Length)
			return default(T); //default(T): Standardwert von T (int: 0, string: null, bool: false, ...)
		return data[index];
	}

	public void Report(T value) //T kommt von Interface
	{

	}

	public bool Equals(int other) //Fixer Typ kommt von oben
	{
		return true;
	}

	public void PrintType<MyType>()
	{
        Console.WriteLine(default(MyType)); //default(T): Standardwert von T (int: 0, string: null, bool: false, ...)
        Console.WriteLine(typeof(MyType)); //Typ aus Generic holen
        Console.WriteLine(nameof(MyType)); //Typ als string (z.B. "int", "string", "Func", ...)

        //if (MyType is int) { }
        if (typeof(MyType) == typeof(int)) { } //Typvergleich mit Generic
	}
}

public class DataStoreVererbung<T> : DataStore<T> { }