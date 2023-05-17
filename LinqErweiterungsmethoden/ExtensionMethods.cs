using System.Security.AccessControl;

namespace LinqErweiterungsmethoden;

internal static class ExtensionMethods
{
	public static int Quersumme(this int x) //mit this <Typ> sich auf einen bestimmten Typen beziehen
	{
		return x.ToString().Sum(e => (int) char.GetNumericValue(e));
	}

	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> x)
	{
		return x.OrderBy(e => Random.Shared.Next());
	}

	public static string GetString(this FahrzeugMarke m)
	{
		return m switch
		{
			FahrzeugMarke.Audi => "Audi",
			FahrzeugMarke.BMW => "BMW",
			FahrzeugMarke.VW => "VW",
			_ => ""
		};
	}
}
