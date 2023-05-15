using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Sprachfeatures;

internal class Program
{
	static void Main(string[] args)
	{
		Test("", "", "", DateTime.Now, 13);
		Test(adresse: "Eine Adresse", gebDat: DateTime.MinValue);
		Test(nachname: "Ein Nachname", alter: 34);

		int x = 5;
		int y = x;
		x = 10;
		/*static*/ void XPlusPlus() => x++;

		string name = "mAx";

		name = char.ToUpper(name[0]) + name[1..];

		bool b = name == "Max" ? true : false;

		//Verbatim-String: String der Escape-Sequenzen ignoriert
		string pfad = @"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_05_15";

		//Interpolated-String: Erlaubt es, Code in einen String einzubauen
		string s = $$"""Der Pfad zum "Kurs" {ist}: {{pfad}}, der Name ist: {{name}}, die Zahl mal zwei ist: {{x * 2}}, das Ergebnis von Test2() ist {{Test2()}}""" +
			$"{x switch 
			{
				1 => "Eins",
				2 => "Zwei",
				3 => "Drei",
				_ => "Andere Zahl"
			}}";

		string s2 = $"{{}}";

		//init == readonly

		switch (x)
		{
			case >= 0 and <= 5:
				break;
			case > 5 and <= 10:
				break;
			case 11 or 12:
				break;
		}

		switch (DateTime.Now.DayOfWeek)
		{
			case >= DayOfWeek.Monday and <= DayOfWeek.Friday:
                    Console.WriteLine("Wochentag");
                    break;
			case DayOfWeek.Saturday or DayOfWeek.Sunday:
                    Console.WriteLine("Wochenende");
                    break;
		}

		Person p = new(1, "", null);
		string str = $"{p switch 
			{
				{ Vorgesetzter.Vorgesetzter.Vorgesetzter: null } => "X"
			}
		}";

		Fahrzeug f = new(1);
	}

	static void Test(string vorname = default, string nachname = default, string adresse = default, DateTime gebDat = default, int alter = default) { }

	static int Test2() => 5;

	void Switch()
	{
		Person p = new Person(1, "", null);
		Person p1 = p;
		switch (p)
		{
			case Person p2 when p2.ID == 5:
				break;
		}
	}
}

//public class Person
//{
//	public int ID { get; set; }

//	public Person(int iD)
//	{
//		ID = iD;
//	}
//}

public record Person([field: JsonIgnore] int ID, string Vorname, Person Vorgesetzter)
{
	public void Test()
	{

	}
}

public class Fahrzeug
{
	public required int ID { get; set; }

	[SetsRequiredMembers]
	public Fahrzeug(int ID)
	{
		this.ID = ID;
	}
}