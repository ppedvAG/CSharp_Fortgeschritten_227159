namespace Multithreading;

internal class _06_Lock
{
	static int Counter = 0;

	static object LockObject = new();

	static void Main(string[] args)
	{
		for (int i = 0; i < 1000; i++)
			new Thread(ZahlPlusPlus).Start();
	}

	static void ZahlPlusPlus()
	{
		lock (LockObject) //Zahl sperren damit nicht mehrere Threads gleichzeitig zugreifen können
		{
			Counter++;
		} //Lock wird geöffnet für den nächsten Thread

		Monitor.Enter(LockObject); //Monitor und Lock haben 1:1 den selben Code
		Counter++;
		Monitor.Exit(LockObject);
	}
}
