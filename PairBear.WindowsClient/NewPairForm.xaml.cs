using PairBear.Service;
using PairBear.WindowsClient.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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

namespace PairBear.WindowsClient
{
	/// <summary>
	/// Interaction logic for NewPairForm.xaml
	/// </summary>
	public partial class NewPairForm : UserControl
	{
		public NewPairFormViewModel NewPairFormViewModel { get; set; }

		public NewPairForm()
		{
			InitializeComponent();
			NewPairFormViewModel = App.ServiceProvider.GetService<NewPairFormViewModel>();
			DataContext = NewPairFormViewModel;
		}

		private void AddPair_Click(object sender, RoutedEventArgs e)
		{
			NewPairFormViewModel.AddNewPair();
		}
	}
}
