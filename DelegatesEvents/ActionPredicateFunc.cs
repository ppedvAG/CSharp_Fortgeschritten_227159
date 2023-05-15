using System.Threading.Channels;

namespace DelegatesEvents;

internal class ActionPredicateFunc
{
	static List<int> TestList = new();

	static void Main(string[] args)
	{
		//Action, Predicate, Func: Vordefinierte Delegates die in C# an einigen Stellen eingebaut sind
		//Essentiell für die Fortgeschrittene Programmierung
		//Können alles was in dem Delegate Kapitel vorher gekommen ist

		//Action: Methode mit void und bis zu 16 Parametern
		Action<int, int> action = Addiere;
		action(4, 5);
		action?.Invoke(5, 6);

		DoAction(6, 7, Addiere); //Das Verhalten der Methode über den Action Parameter festlegen
		DoAction(6, 7, Subtrahiere);
		DoAction(6, 7, action);

		TestList.ForEach(Quadiere); //Hier bei ForEach über den Parameter das Verhalten der Funktion bestimmen
		void Quadiere(int x) => Console.WriteLine(x * x);


		//Predicate: Funktion mit bool als Rückgabewert und genau einem Parameter
		Predicate<int> pred = CheckForZero;
		bool b = pred(3);
		bool? b2 = pred?.Invoke(0); //Invoke könnte null zurück geben, wenn das Predicate null ist
		bool b3 = pred?.Invoke(0) == true; //Drei mögliche Ergebnisse: true, false oder null -> true: true, false oder null: false

		DoPredicate(0, CheckForZero); //Das Verhalten der Methode über den Predicate Parameter festlegen
		DoPredicate(0, CheckForOne);
		DoPredicate(0, pred);

		TestList.FindAll(CheckForZero);


		//Func: Methode mit Rückgabewert (letztes Generic ist der Rückgabetyp) und bis zu 16 Parametern
		Func<int, int, double> func = Multipliziere;
		double d = func(4, 5);
		double? d2 = func?.Invoke(2, 5);
		double d3 = d2 == null ? double.NaN : d2.Value; //Möglicher Weg um double? zu double zu konvertieren

		DoFunc(4, 5, Multipliziere); //Das Verhalten der Methode über den Func Parameter festlegen
		DoFunc(4, 5, Dividiere);
		DoFunc(4, 5, func);

		TestList.Where(TeilbarDurch2);
		bool TeilbarDurch2(int x) => x % 2 == 0;

		func += delegate (int x, int y) { return x + y; }; //Anonyme Methode

		func += (int x, int y) => { return x + y; }; //Kürzere Form

		func += (x, y) => { return x - y; };

		func += (x, y) => (double) x / y; //Kürzeste, häufigste Form

		//Anwendung von anonymen Funktionen
		DoAction(4, 5, (x, y) => Console.WriteLine(x + y)); //Hier kein Rückgabewert möglich -> CW
		DoPredicate(4, (x) => x % 2 == 0); //Ist die Zahl durch 2 teilbar? -> bool als Ergebnis
		DoFunc(4, 6, (x, y) => x % y); //Anonyme Methode muss hier einen double ergeben

		TestList.ForEach(x => Console.WriteLine(x));
		TestList.Find(x => x % 2 == 0);
		TestList.Where(x => x % 5 == 0);
	}

	#region Action
	private static void Addiere(int arg1, int arg2) => Console.WriteLine($"{arg1} + {arg2} = {arg1 + arg2}");

	private static void Subtrahiere(int arg1, int arg2) => Console.WriteLine($"{arg1} - {arg2} = {arg1 - arg2}");

	private static void DoAction(int x, int y, Action<int, int> action) => action?.Invoke(x, y);
	#endregion

	#region Predicate
	public static bool CheckForZero(int x) => x == 0;

	public static bool CheckForOne(int x) => x == 1;

	public static bool DoPredicate(int x, Predicate<int> pred) => pred?.Invoke(0) == true;
	#endregion

	#region Func
	private static double Multipliziere(int arg1, int arg2) => arg1 * arg2;

	private static double Dividiere(int x, int y) => (double) x / y; //double Cast um Kommazahlen als Ergebnis zu bekommen, ansonsten wird hier eine int-Division durchgeführt

	private static double DoFunc(int x, int y, Func<int, int, double> func) => func(x, y);
	#endregion
}
