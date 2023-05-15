namespace DelegatesEvents;

internal class Events
{
	//Event: Statischer Punkt, an den Methoden angehängt werden können
	//Im Code der Komponente können Events aufgerufen werden
	static event EventHandler TestEvent;

	static event EventHandler<TestEventArgs> ArgsEvent;

	static event EventHandler<int> IntEvent
	{
		add 
		{
            Console.WriteLine($"Event wurde angehängt: {value.Method.Name}");
        }
		remove
		{
            Console.WriteLine("Event wurde abgenommen");
        }
	}

	static void Main(string[] args)
	{
		TestEvent += Events_TestEvent; //Methode anhängen ohne new, Events kann nicht erstellt werden
		TestEvent(null, EventArgs.Empty); //Event ausführen
		TestEvent += (sender, args) => Console.WriteLine("Anonyme Methode"); //Anonyme Methode anhängen
		TestEvent(null, EventArgs.Empty);

		ArgsEvent += Events_ArgsEvent;
		ArgsEvent(null, new TestEventArgs("Erfolg"));

		IntEvent += Events_IntEvent;
	}

	private static void Events_TestEvent(object sender, EventArgs e)
	{
		Console.WriteLine("TestEvent wurde ausgeführt");
	}

	private static void Events_ArgsEvent(object sender, TestEventArgs e) //Hier sind jetzt TestEventArgs
	{
        Console.WriteLine($"Der Status ist {e.Status}");
	}

	private static void Events_IntEvent(object sender, int e) //Hier ist jetzt ein int
	{
		Console.WriteLine($"Die Zahl ist {e}");
	}
}

public class TestEventArgs : EventArgs
{
	public string Status { get; set; }

	public TestEventArgs(string Status)
	{
		this.Status = Status;
	}
}