namespace Multitasking;

internal class _04_TaskExceptions
{
	static void Main(string[] args)
	{
		try
		{
			Task t1 = new Task(Exception1);
			Task t2 = new Task(Exception2);
			Task t3 = new Task(Exception3);

			t1.Start();
			t2.Start();
			t3.Start();

			Task.WaitAll(t1, t2, t3);

			Console.ReadKey();
		}
		catch (AggregateException ex)
		{
			foreach (Exception e in ex.InnerExceptions)
			{
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
            }
		}
	}

	static void Exception1()
	{
		Thread.Sleep(1000);
		throw new DivideByZeroException();
	}

	static void Exception2()
	{
		Thread.Sleep(2000);
		throw new StackOverflowException();
	}

	static void Exception3()
	{
		Thread.Sleep(3000);
		throw new OutOfMemoryException();
	}
}