using System.Reflection;
using System.Threading.Channels;

namespace Reflection
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Program p = new();
			Type pt = p.GetType(); //Typ holen mit GetType() über Objekt
			Type t = typeof(Program); //Typ holen durch typeof(<Klassenname>)

			object o = Activator.CreateInstance(pt); //Objekt über Typ-Objekt erstellen

			/////////////////////////////////////////////////////////////////

			pt.GetMethods(); //alle Methoden anschauen
			pt.GetMethod("Test").Invoke(o, null);
			pt.GetMethod("Test2").Invoke(o, new object[] { "Zwei Test" });

			pt.GetProperty("Text").SetValue(o, "Drei Text");
            Console.WriteLine(pt.GetProperty("Text").GetValue(o));

			/////////////////////////////////////////////////////////////////
			
			Assembly assembly = Assembly.GetExecutingAssembly(); //Das derzeitige Projekt anschauen

			object o2 = Activator.CreateInstance("Reflection", "Reflection.Program"); //Objekt nur über strings erstellen

			assembly.GetTypes(); //Alle Typen aus dem Projekt auslesen

			Type loadedType = assembly.GetType("Program"); //Typ über Name holen

			/////////////////////////////////////////////////////////////////

			Assembly a = Assembly.LoadFrom(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_05_15\DelegatesEvents\bin\Debug\net7.0\DelegatesEvents.dll");

			object comp = Activator.CreateInstance(a.GetType("DelegatesEvents.Component"));

			comp.GetType().GetEvent("ProcessCompleted").AddEventHandler(comp, () => Console.WriteLine("Prozess fertig"));
			comp.GetType().GetEvent("Progress").AddEventHandler(comp, (int i) => Console.WriteLine($"Fortschritt: {i}"));
			comp.GetType().GetMethod("StartProcess").Invoke(comp, null);
		}

		public string Text { get; set; }

		public void Test()
		{
            Console.WriteLine("Ein Test");
        }

		public void Test2(string text)
		{
            Console.WriteLine(text);
        }
	}
}