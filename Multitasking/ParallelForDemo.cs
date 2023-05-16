using System.Diagnostics;

namespace Multitasking;

internal class ParallelForDemo
{
	static void Main(string[] args)
	{
		int[] durchgänge = { 1000, 10_000, 50_000, 100_000, 250_000, 500_000, 1_000_000, 5_000_000, 10_000_000, 100_000_000 };
		for (int i = 0; i < durchgänge.Length; i++)
		{
			int d = durchgänge[i];

			Stopwatch sw = Stopwatch.StartNew();
			RegularFor(d);
			sw.Stop();
            Console.WriteLine($"For Durchgänge: {d}, {sw.ElapsedMilliseconds}ms");

			Stopwatch sw2 = Stopwatch.StartNew();
			ParallelFor(d);
			sw2.Stop();
			Console.WriteLine($"ParallelFor Durchgänge: {d}, {sw2.ElapsedMilliseconds}ms");

            Console.WriteLine($"Verhältnis: {sw.ElapsedTicks / (double) sw2.ElapsedTicks}");
			Console.WriteLine("----------------------------------------------------");
        }

		/*
		    For Durchgänge: 1000, 1ms
			ParallelFor Durchgänge: 1000, 55ms
			Verhältnis: 0,018477105519048402
			For Durchgänge: 10000, 3ms
			ParallelFor Durchgänge: 10000, 1ms
			Verhältnis: 1,9068610203568737
			For Durchgänge: 50000, 15ms
			ParallelFor Durchgänge: 50000, 24ms
			Verhältnis: 0,6277616001725611
			For Durchgänge: 100000, 48ms
			ParallelFor Durchgänge: 100000, 32ms
			Verhältnis: 1,5198375059281632
			For Durchgänge: 250000, 101ms
			ParallelFor Durchgänge: 250000, 106ms
			Verhältnis: 0,9562527730598472
			For Durchgänge: 500000, 202ms
			ParallelFor Durchgänge: 500000, 88ms
			Verhältnis: 2,2939564651095186
			For Durchgänge: 1000000, 537ms
			ParallelFor Durchgänge: 1000000, 417ms
			Verhältnis: 1,2868870259966607
			For Durchgänge: 5000000, 4872ms
			ParallelFor Durchgänge: 5000000, 572ms
			Verhältnis: 8,507517709939393
			For Durchgänge: 10000000, 3911ms
			ParallelFor Durchgänge: 10000000, 944ms
			Verhältnis: 4,141500620340891
			For Durchgänge: 100000000, 28338ms
			ParallelFor Durchgänge: 100000000, 10924ms
			Verhältnis: 2,594193264401645
		 */
	}

	static void RegularFor(int iterations)
	{
		double[] erg = new double[iterations];
		for (int i = 0; i < iterations; i++)
			erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100);
	}

	static void ParallelFor(int iterations)
	{
		double[] erg = new double[iterations];
		//int i = 0; i < iterations; i++
		Parallel.For(0, iterations, i => erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100));
	}
}
