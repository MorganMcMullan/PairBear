using PairBear.WindowsClient.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace PairBear.WindowsClient
{
	/// <summary>
	/// Interaction logic for PairList.xaml
	/// </summary>
	public partial class PairList : UserControl
	{
		public PairListViewModel PairListViewModel { get; set; }

		public PairList()
		{
			InitializeComponent();
			PairListViewModel = App.ServiceProvider.GetService<PairListViewModel>();
			DataContext = PairListViewModel;
		}

		//Update sorting state on the key flow when key header button is clicked 
		private void KeyHeader_Click(object sender, RoutedEventArgs e)
		{
			PairListViewModel.SortByKey();
		}

		//Update sorting state on the value flow when value header button is clicked
		private void ValueHeader_Click(object sender, RoutedEventArgs e)
		{
			PairListViewModel.SortByValue();
		}

		//Remove currently selected pair
		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			var button = sender as Button;
			PairListViewModel.SelectedPair = button.DataContext as Pair;
			PairListViewModel.RemovePair();
		}

		private void PairListListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
}
