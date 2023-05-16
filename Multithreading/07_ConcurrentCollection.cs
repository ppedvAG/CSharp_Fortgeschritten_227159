using System.Collections.Concurrent;

namespace Multithreading;

internal class _07_ConcurrentCollection
{
	static void Main(string[] args)
	{
		ConcurrentBag<int> ints = new ConcurrentBag<int>(); //Thread-/Task sichere List
		ints.Add(1);

		ConcurrentDictionary<int, string> dict = new(); //Thread-sicheres Dictionary
		dict.TryAdd(1, "a"); //Add -> TryAdd
	}
}
