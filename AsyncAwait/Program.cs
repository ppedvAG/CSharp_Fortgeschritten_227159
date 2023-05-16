using System.Diagnostics;

namespace AsyncAwait;

internal class Program
{
	static async Task Main(string[] args)
	{
		Stopwatch sw = Stopwatch.StartNew();
		//Toast();
		//Geschirr();
		//Kaffee();
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds); //7s, sequentiell

		//sw.Restart();
		//Task.Run(() => 
		//{
		//	Toast();
		//	Geschirr();
		//	Kaffee();
		//});
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds); //0s, Main Thread läuft weiter

		//sw.Restart();
		//Task t = Task.Run(Toast);
		//Task t2 = Task.Run(Geschirr).ContinueWith(x => Kaffee());
		//t.ContinueWith(x =>
		//{
		//	sw.Stop();
		//	Console.WriteLine(sw.ElapsedMilliseconds); //4s
		//});
		//Task.WaitAll(t, t2);

		//sw.Restart();
		//Task t = ToastAsync();
		//Task g = GeschirrAsync();
		//Task k = KaffeeAsync();
		//await t;
		//await g; //Wenn der Task g hier noch läuft, dann warte hier
		//await k;
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//sw.Restart();
		//Task<Toast> toast = ToastObjectAsync();
		//Task<Tasse> tasse = GeschirrObjectAsync();
		//Tasse t = await tasse; //Nimm den Rückgabewert aus dem Task heraus wenn dieser fertig ist
		//Task<Kaffee> kaffee = KaffeeObjectAsync(t);
		//Kaffee k = await kaffee;
		//Toast brot = await toast; //await sollte sortiert sein nach der Länge der Tasks
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		Task<Toast> toast = ToastObjectAsync();
		Task<Tasse> t = GeschirrObjectAsync();
		Kaffee k = await KaffeeObjectAsync(await t);
		Toast brot = await toast;

		//WhenAll: Warte auf mehrere Tasks, funktioniert nur wenn alle Tasks ähnlich lange dauern, oder wenn ich keine Ahnung habe wie lange die einzelnen Tasks brauchen
		await Task.WhenAll(toast, t); //Mehrere await Statements in eines konsolidieren mit WhenAll
		await Task.WhenAny(toast, t); //Wie WhenAll nur mit einem Ergebnis
	}

	static void Toast()
	{
		Thread.Sleep(4000);
        Console.WriteLine("Toast fertig");
    }

	static void Geschirr()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Geschirr hergerichtet");
	}

	static void Kaffee()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Kaffee fertig");
	}

	/// <summary>
	/// 1. Await ist jetzt verwendbar
	/// 2. Methoden die async sind und einfach aufgerufen werden, werden als Tasks gestartet
	/// 3. Diese Methoden können awaited werden (wenn sie nicht void sind)
	/// 4. Methode die async ist und nur Task (ohne Generic) als Rückgabewert hat, braucht kein return
	/// </summary>
	static async Task ToastAsync()
	{
		await Task.Delay(4000); //await: Warte hier, das dieses Stück Code fertig ist (Warte auf das warten von 4 Sekunden)
		Console.WriteLine("Toast fertig");
	}

	static async Task GeschirrAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Geschirr hergerichtet");
	}

	static async Task KaffeeAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
	}

	static async Task<Toast> ToastObjectAsync()
	{
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
		return new Toast();
	}

	static async Task<Tasse> GeschirrObjectAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Geschirr hergerichtet");
		return new Tasse();
	}

	static async Task<Kaffee> KaffeeObjectAsync(Tasse t)
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
		return new Kaffee();
	}
}

public class Toast { }

public class Tasse { }

public class Kaffee { }