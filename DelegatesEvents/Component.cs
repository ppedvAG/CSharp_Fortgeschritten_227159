namespace DelegatesEvents;

public class Component
{
	public event Action ProcessCompleted; //Action als Delegate statt EventHandler

	public event Action<int> Progress; //Action mit einem Parameter (der Fortschritt)

	/// <summary>
	/// Stellt einen längeren Prozess dar, kommuniziert über die Events mit dem Benutzer
	/// </summary>
	public void StartProcess()
	{
		for (int i = 0; i < 10; i++)
		{
			Thread.Sleep(200);
			Progress?.Invoke(i); //? hier essentiell, da der Programmierer möglicherweise keine Methode an das Event hängt
		}
		ProcessCompleted?.Invoke(); //? hier essentiell, da der Programmierer möglicherweise keine Methode an das Event hängt
	}
}
