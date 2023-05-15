namespace DelegatesEvents;

internal class Delegates
{
	public delegate void Vorstellungen(string name); //Definition von Delegate, speichert Referenzen auf Methoden, können zur Laufzeit hinzugefügt oder weggenommen werden

	public static void Main(string[] args)
	{
		Vorstellungen v = new Vorstellungen(VorstellungDE); //Erstellung von Delegate + Initialmethode
		v("Max"); //Delegate ausführen

		v += VorstellungEN; //Methode anhängen
		v += VorstellungEN; //Selbe Methode kann mehrmals angehängt werden
		v("Lukas");

		v -= VorstellungDE; //Methode abnehmen
		v -= VorstellungDE;
		v -= VorstellungDE; //Methode die nicht dran ist, gibt keinen Fehler wenn sie abgenommen wird
		v("Stefan");

		v -= VorstellungEN;
		v -= VorstellungEN; //Delegate ist null wenn die letzte Methode abgenommen wird
		//v("Max");

		if (v is not null) //Vorher auf null überprüfen
			v("Max");

		//Null Propagation: Schneller Null-Check, mit ? vor einem Funktionsaufruf
		v?.Invoke("Max"); //Wenn v nicht null ist, führe den Code danach aus, wenn v null ist, überspringe den Code danach

		List<int> ints = null;
		ints?.Add(1); //Wenn die Liste nicht null ist, füge das Element hinzu

		foreach (Delegate dg in v.GetInvocationList()) //Delegate anschauen
		{
            Console.WriteLine(dg.Method.Name);
        }
	}

	public static void VorstellungDE(string name) => Console.WriteLine($"Hallo mein Name ist {name}");

	public static void VorstellungEN(string name) => Console.WriteLine($"Hello my name is {name}");
}