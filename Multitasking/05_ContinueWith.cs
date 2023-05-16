namespace Multitasking;

internal class _05_ContinueWith
{
	static void Main(string[] args)
	{
		//Mit ContinueWith kann ein Task an einen anderen Task angehängt werden (Tasks verketten)
		//Verhindern blockieren des Main Threads
		//Auf den vorherigen Task kann zugegriffen werden (auch auf das Ergebnis)
		Task<double> t = Task.Run(() =>
		{
			Thread.Sleep(1000);
			//throw new Exception();
			return Math.Pow(4, 30);
		});
		t.ContinueWith(vorherigerTask => Console.WriteLine(vorherigerTask.Result)); //Dieser Task wird immer ausgeführt (auch bei einer Exception)
		t.ContinueWith(t => Console.WriteLine("Exception"), TaskContinuationOptions.OnlyOnFaulted);
		t.ContinueWith(t => Console.WriteLine(t.Result * 2)); //Hier werden jetzt 2 Tasks ausgeführt

		Task t2 = new Task(() => { }).ContinueWith(t => { }); //Wenn zwischen Task Start und ContinueWith ein langes Stück Code ist und der Task vorher fertig ist, kann diese Schreibweise gewählt werden
		t2.Start();

        Console.WriteLine("Main Thread");

        Console.ReadKey();
	}
}
