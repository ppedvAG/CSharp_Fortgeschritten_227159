namespace Multitasking;

internal class _06_WhenAllAny
{
	static void Main(string[] args)
	{
		List<Task<int>> tasks = new();
		for (int i = 0; i < 100; i++)
			tasks.Add(Task.Run(() => i * i));

		Task<int[]> ints = Task.WhenAll(tasks); //WhenAll: Alle Ergebnisse von mehreren Tasks sammeln
		//Console.WriteLine(ints.Result); //Bei großen Datenmengen ContinueWith bevorzugen
		ints.ContinueWith(t => Console.WriteLine(t.Result));

		Task.WhenAny(tasks).ContinueWith(t => Console.WriteLine($"Erstes Ergebnis: {t.Result.Result}")); //WhenAny: Das Ergebnis vom schnellsten Task holen
	}
}
