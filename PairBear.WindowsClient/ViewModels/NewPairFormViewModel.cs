using PairBear.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PairBear.WindowsClient.ViewModels
{
	public class NewPairFormViewModel : INotifyPropertyChanged
	{
		//Binds to the contents of the Key textbox
		private string keyValue;
		public string Key
		{
			get => keyValue;
			set
			{
				if (value != keyValue)
				{
					keyValue = value;
					NotifyPropertyChanged();
				}

			}
		}

		//Binds to the contents of the Value textbox
		private string valueValue;
		public string Value
		{
			get => valueValue;
			set
			{
				if (value != valueValue)
				{
					valueValue = value;
					NotifyPropertyChanged();
				}

			}
		}

		private IPairService PairService { get; set; }

		public NewPairFormViewModel(IPairService pairService)
		{
			PairService = pairService;
		}

		//Create a new pair from the values in the key and value properties and reset their values to empty
		public void AddNewPair()
		{
			if (!string.IsNullOrWhiteSpace(Key) && !string.IsNullOrWhiteSpace(Value))
			{
				PairService.AddPair(new Pair(Key, Value));
				Key = "";
				Value = "";
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		//Fire the property changed event when any property changes values
		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
