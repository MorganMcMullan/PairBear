using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

namespace PairBear.Core
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		public ObservableCollection<Pair> BearPairs { get; set; }
		public Pair NewPair { get; set; }

		public MainWindow()
		{
			BearPairs = new ObservableCollection<Pair>();
			InitializeComponent();
			BearPairsListView.ItemsSource = BearPairs;

			BearPairs.Add(new Pair("Pair", "1"));
			BearPairs.Add(new Pair("Pair", "2"));
			BearPairs.Add(new Pair("Pair", "3"));
			BearPairs.Add(new Pair("Pair", "4"));
		}
	}
}
