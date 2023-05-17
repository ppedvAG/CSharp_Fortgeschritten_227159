using System.Collections;
using System.Text;
using System.Threading.Channels;

namespace Sonstiges;

internal class Program
{
	static void Main(string[] args)
	{
		Wagon w1 = new();
		Wagon w2 = new();
        Console.WriteLine(w1 == w2);

		Zug z = new();
		z += w1;
		z += w2;

		z++;
		z++;
		z++;
		z++;
		z++;

		foreach (Wagon w in z)
		{
			
		}

		z[3] = new();
		Console.WriteLine(z["Rot"]);
		Console.WriteLine(z[10, "Rot"]);

		var x = z.Wagons.Select(e => new { e.AnzSitze, HC = e.GetHashCode() });
		Console.WriteLine(x.First().HC); //Mit Select mehrere Werte bewegen über anonymes Objekt (muss keine neue Klasse erstellen)

		System.Timers.Timer timer = new();
		timer.Interval = 1000;
		timer.Elapsed += (sender, args) => Console.WriteLine("Intervall vergangen");
		timer.Start();

		Console.ReadLine();
	}
}

public class Zug : IEnumerable
{
	public List<Wagon> Wagons = new();

	public IEnumerator GetEnumerator()
	{
		//return Wagons.GetEnumerator();
		foreach (Wagon w in Wagons)
			yield return w; //yield return: Schreibe das Element in eine temporäre Liste, und gib diese am Ende der Methode zurück
		//IEnumerable<Wagon> temp = new();
		//foreach ... temp.Add(w);
		//return temp;
	}

	public static Zug operator +(Zug z, Wagon w)
	{
		z.Wagons.Add(w);
		return z;
	}

	public static Zug operator ++(Zug z)
	{
		z.Wagons.Add(new Wagon());
		return z;
	}

	public Wagon this[int i]
	{
		get => Wagons[i];
		set => Wagons[i] = value;
	}

	public Wagon this[string s]
	{
		get => Wagons.First(e => e.Farbe == s);
	}

	public Wagon this[int anz, string s]
	{
		get => Wagons.First(e => e.Farbe == s && e.AnzSitze == anz);
	}
}

public class Wagon
{
	public int AnzSitze { get; set; }

	public string Farbe { get; set; }

	public static bool operator ==(Wagon a, Wagon b)
	{
		return a.AnzSitze == b.AnzSitze && a.Farbe == b.Farbe;
	}

	public static bool operator !=(Wagon a, Wagon b)
	{
		return !(a == b);
	}
}