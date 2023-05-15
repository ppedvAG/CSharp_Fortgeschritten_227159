namespace DelegatesEvents;

internal class ComponentWithEvent
{
	static void Main(string[] args)
	{
		Component comp = new();
		comp.Progress += (i) => Console.WriteLine($"Progress: {i}");
		comp.ProcessCompleted += () => Console.WriteLine("Prozess fertig"); //Entwickler auf der Anwenderseite kann selber festlegen was die Komponente tut
		comp.StartProcess();
	}
}
