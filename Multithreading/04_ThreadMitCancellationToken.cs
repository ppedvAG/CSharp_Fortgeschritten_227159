namespace Multithreading;

internal class _04_ThreadMitCancellationToken
{
	static void Main(string[] args)
	{
		CancellationTokenSource cts = new CancellationTokenSource(); //Sender, gibt die Tokens aus und steuert das abbrechen der entsprechenden Aufgaben
		CancellationToken ct = cts.Token; //Token aus der Source entnehmen

		ParameterizedThreadStart pt = new ParameterizedThreadStart(Run);
		Thread t = new Thread(pt);
		t.Start(ct);

		Thread.Sleep(500);

		cts.Cancel(); //Cancel alle Token die von der Source gekommen sind
	}

	static void Run(object o)
	{
		try
		{
			if (o is CancellationToken ct)
			{
				for (int i = 0; i < 100; i++)
				{
					ct.ThrowIfCancellationRequested();
					Console.WriteLine($"Side Thread: {i}");
					Thread.Sleep(25);
				}
			}
		}
		catch (OperationCanceledException)
		{
            Console.WriteLine("Thread abgebrochen");
        }
	}
}