using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncAwaitWPF
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Progress.Value = 0;
			for (int i = 0; i < 100; i++)
			{
				Thread.Sleep(25);
				Progress.Value++;
			} //UI Thread wird blockiert
		}

		private void Button_Click1(object sender, RoutedEventArgs e)
		{
			Task.Run(() => //UI Updates sind nicht erlaubt von einem Side Thread/Task
			{
				this.Dispatcher.Invoke(() => Progress.Value = 0);
				for (int i = 0; i < 100; i++)
				{
					Thread.Sleep(25);
					this.Dispatcher.Invoke(() => Progress.Value++);
				}
			});
		}

		private async void Button_Click2(object sender, RoutedEventArgs e)
		{
			Progress.Value = 0;
			for (int i = 0; i < 100; i++)
			{
				await Task.Delay(25);
				Progress.Value++;
			}
		}

		private async void Button_Click3(object sender, RoutedEventArgs e)
		{
			Progress.Value = 0;
			Progress.Maximum = 3;
			using HttpClient client = new HttpClient();
			Task<HttpResponseMessage> resp = client.GetAsync(@"http://www.gutenberg.org/files/54700/54700-0.txt");
			//UI Update
			Button1.IsEnabled = false;
			TB.Text = "Text wird heruntergeladen";
			HttpResponseMessage message = await resp;
			Task<string> str = message.Content.ReadAsStringAsync();
			//UI Update
			Progress.Value++;
			TB.Text = "Text wird angezeigt";
			await Task.Delay(500);
			string text = await str;
			TB.Text = text;
			Button1.IsEnabled = true;
			Progress.Value++;
		}

		private async void Button_Click4(object sender, RoutedEventArgs e)
		{
			List<int> ints = Enumerable.Range(0, 100_000_000).ToList();
			Stopwatch sw = Stopwatch.StartNew();
			await Parallel.ForEachAsync(ints, (i, ct) =>
			{
                Console.WriteLine(i * 10);
				return ValueTask.CompletedTask; //Leeren Task zurückgeben
            });
			sw.Stop();
			TB.Text = sw.ElapsedMilliseconds.ToString();
		}
	}
}
