namespace Multitasking;

internal class _03_TaskBeenden
{
	static void Main(string[] args)
	{
		CancellationTokenSource cts = new CancellationTokenSource(); //Sender
		CancellationToken ct = cts.Token; //Empfänger

		Task t = new Task(Run, ct); //Hier Token übergeben
		t.Start();

		Thread.Sleep(500);

		cts.Cancel();

		Console.ReadKey();
	}

	static void Run(object o) //Object o muss gegeben sein für CancellationToken
	{
		if (o is CancellationToken ct)
		{
			for (int i = 0; i < 100; i++)
			{
				ct.ThrowIfCancellationRequested(); //Task wirft Exception ist aber nicht sichtbar
                Console.WriteLine($"Task: {i}");
				Thread.Sleep(10);
            }
		}
	}
}
