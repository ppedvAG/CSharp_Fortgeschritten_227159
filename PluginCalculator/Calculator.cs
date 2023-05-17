using PluginBase;

namespace PluginCalculator;

public class Calculator : IPlugin
{
	public string Name => "Rechner Plugin";

	public string Description => "Ein einfacher Rechner der die vier Grundrechenarten beherrscht";

	public string Version => "1.0";

	public string Author => "Lukas Kern";

	[ReflectionVisible]
	public double Addiere(double z1, double z2) => z1 + z2;

	[ReflectionVisible]
	public double Subtrahiere(double z1, double z2) => z1 - z2;

	[ReflectionVisible]
	public double Multipliziere(double z1, double z2) => z1 * z2;

	[ReflectionVisible]
	public double Dividiere(double z1, double z2) => z1 / z2;
}