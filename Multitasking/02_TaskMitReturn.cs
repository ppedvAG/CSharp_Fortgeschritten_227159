namespace Multitasking;

internal class _02_TaskMitReturn
{
	static void Main(string[] args)
	{
		Task<int> intTask = new Task<int>(Run);
		intTask.Start();
        Console.WriteLine(intTask.Result); //intTask.Result blockiert den Main Thread, 2 Lösungen: ContinueWith, await

		Task t2 = Task.Run(() => Console.WriteLine("Etwas")); //Task mit anonymer Methode

		Task t3 = Task.Run(() => 
		{
            Console.WriteLine("Mehrzeilige");
            Console.WriteLine("anonyme");
            Console.WriteLine("Methode");
        });

		t3.Wait(); //Ab hier gehts erst mit dem Main Thread weiter, wenn t3 fertig ist
		Task.WaitAll(intTask, t2, t3); //Warte bis alle Tasks fertig sind
		Task.WaitAny(intTask, t2, t3); //Warte bis ein Task fertig ist, gibt den Index des Tasks zurück der zuerst fertig geworden ist
    }

	static int Run()
	{
		int summe = 0;
		for (int i = 0; i < 1000; i++)
		{
			Thread.Sleep(1); //Bezieht sich auf den Task
			summe += i;
		}
		return summe;
	}
}
