using System.Diagnostics;
using System.Text;

namespace LinqErweiterungsmethoden;

internal class Program
{
	static void Main(string[] args)
	{
		#region Einfaches Linq
		List<int> ints = Enumerable.Range(1, 20).ToList();

		Console.WriteLine(ints.Average());
		Console.WriteLine(ints.Min());
		Console.WriteLine(ints.Max());
		Console.WriteLine(ints.Sum());

		Console.WriteLine(ints.First()); //Gibt das erste Element zurück, Exception wenn kein Element existiert oder wenn die Bedingung kein Element findet
		Console.WriteLine(ints.FirstOrDefault()); //Gibt das erste Element zurück, null wenn kein Element gefunden

		Console.WriteLine(ints.Last());
		Console.WriteLine(ints.LastOrDefault());

		Console.WriteLine(ints.First(e => e % 5 == 0)); //Die erste Zahl die durch 5 teilbar ist
		Console.WriteLine(ints.Last(e => e % 5 == 0)); //Finde die letzte Zahl in der Liste die durch 5 teilbar ist
		
		//Console.WriteLine(ints.First(e => e % 50 == 0)); //Exception
		Console.WriteLine(ints.FirstOrDefault(e => e % 50 == 0));
		#endregion

		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		#region Vergleich Linq Schreibweisen
		//Alle BMWs finden
		List<Fahrzeug> bmwsForEach = new();
		foreach (Fahrzeug f in fahrzeuge)
			if (f.Marke == FahrzeugMarke.BMW)
				bmwsForEach.Add(f);

		//Standard-Linq: SQL-ähnliche Schreibweise (alt)
		List<Fahrzeug> bmwsAlt = (from f in fahrzeuge
								  where f.Marke == FahrzeugMarke.BMW
								  select f).ToList();

		//Methodenkette
		List<Fahrzeug> bmwsNeu = fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW).ToList();
		#endregion

		#region Linq mit Lambda
		ints.Order(); //Sortiert die Liste nach dem Standardsortierverhalten, vor C# 11: ints.OrderBy(e => e)
		ints.OrderDescending();

		fahrzeuge.Min(e => e.MaxV); //Die kleinste Geschwindigkeit
		fahrzeuge.MinBy(e => e.MaxV); //Das Fahrzeug mit der kleinsten Geschwindigkeit

		fahrzeuge.Chunk(5);

		string s = fahrzeuge.Aggregate(string.Empty, (agg, fzg) => agg + $"Das Fahrzeug hat die Marke {fzg.Marke} und kann maximal {fzg.MaxV}km/h fahren.\n");
        Console.WriteLine(s);

		string sb2 = fahrzeuge.Aggregate(new StringBuilder(), (agg, fzg) => agg.AppendLine($"Das Fahrzeug hat die Marke {fzg.Marke} und kann maximal {fzg.MaxV}km/h fahren.")).ToString();

		fahrzeuge.GroupBy(e => e.Marke); //Erzeugt Gruppen anhand eines Kriteriums (BMW-Gruppe, VW-Gruppe, Audi-Gruppe)

		Dictionary<FahrzeugMarke, List<Fahrzeug>> dict = fahrzeuge.GroupBy(e => e.Marke).ToDictionary(e => e.Key, e => e.ToList());
		//dict[FahrzeugMarke.Audi]



		//Ohne Linq (23 Zeilen)
		HttpClient client = new();
		string str = Task.Run(() => client.GetStringAsync("http://www.gutenberg.org/files/54700/54700-0.txt")).Result;
		string[] words = str.Split(new char[] { ' ', '\n', ',', '.', ';', ':', '-', '_', '/' }, StringSplitOptions.RemoveEmptyEntries);

		Dictionary<string, int> anzahlen = new();
		foreach (string wort in words)
		{
			if (wort.Length > 6)
			{
				if (!anzahlen.ContainsKey(wort.ToLower()))
					anzahlen.Add(wort.ToLower(), 0);
				anzahlen[wort.ToLower()]++;
			}
		}

		IEnumerable<KeyValuePair<string, int>> top10 = anzahlen.OrderByDescending(e => e.Value).Take(10);

		StringBuilder sb = new StringBuilder();
		sb.AppendLine("Die häufigsten Wörter sind:");
		foreach (var v in top10)
		{
			sb.AppendLine("  " + v);
		}
		Console.WriteLine(sb.ToString());

		//Mit Linq
		IEnumerable<string> a = words
			.Where(e => e.Length > 6)
			.GroupBy(e => e.ToLower())
			.ToDictionary(e => e.Key, e => e.Count())
			.OrderByDescending(e => e.Value)
			.Take(10)
			.Select(e => e.Key + ": " + e.Value);
        Console.WriteLine(string.Join('\n', a));

		//Mit Aggregate
		Console.WriteLine
		(
			words
				.Where(e => e.Length > 6)
				.GroupBy(e => e.ToLower())
				.ToDictionary(e => e.Key, e => e.Count())
				.OrderByDescending(e => e.Value)
				.Take(10)
				.Aggregate(string.Empty, (agg, e) => $"{agg}{e} \n")
		);
		#endregion

		#region Erweiterungsmethoden
		int x = 58347;
		x.Quersumme();
        Console.WriteLine(427398479.Quersumme());

		fahrzeuge.Shuffle();
		dict.Shuffle();

		fahrzeuge.Select(e => e.Marke.GetString()); //Enum Funktionen geben
        #endregion
    }
}

[DebuggerDisplay("Marke: {Marke}, Geschwindigkeit: {MaxV} - {typeof(Fahrzeug).FullName}")]
public class Fahrzeug
{
	public int MaxV { get; set; }

	public FahrzeugMarke Marke { get; set; }

	public Fahrzeug(int maxV, FahrzeugMarke marke)
	{
		MaxV = maxV;
		Marke = marke;
	}
}

public enum FahrzeugMarke { Audi, BMW, VW }