using PairBear.Service;
using Stateless;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PairBear.WindowsClient.ViewModels
{
	public class PairListViewModel : INotifyPropertyChanged
	{
		//Binds to the button reprisenting the header of the Key column
		private string keyButtonTextValue;
		public string KeyButtonText
		{
			get => keyButtonTextValue;
			set
			{
				if (value != keyButtonTextValue)
				{
					keyButtonTextValue = value;
					NotifyPropertyChanged();
				}

			}
		}

		//Binds to the button reprisenting the header of the Value column
		private string valueButtonTextValue;
		public string ValueButtonText
		{
			get => valueButtonTextValue;
			set
			{
				if (value != valueButtonTextValue)
				{
					valueButtonTextValue = value;
					NotifyPropertyChanged();
				}
			}
		}

		//Binds to the listview to display the Pairs
		private IEnumerable<Pair> pairsValue;
		public IEnumerable<Pair> Pairs
		{
			get => pairsValue;
			set
			{
				if (value != pairsValue)
				{
					pairsValue = value;
					NotifyPropertyChanged();
				}
			}
		}

		//Binds one way to source to the currently selected pair in the listview
		public Pair SelectedPair { get; set; }

		private IPairService PairService;
		private StateMachine<State, Trigger> ListSortStatusStateMachine;

		//Gets the initial list and subscribes to the pair added event and sets the initial sort state
		public PairListViewModel(IPairService pairService)
		{
			PairService = pairService;
			Pairs = PairService.GetPairs();
			PairService.RepositoryUpdated += OnPairAdded;

			ListSortStatusStateMachine = new StateMachine<State, Trigger>(State.Unsorted);
			ConfigureListSortStatusStateMachine();
		}

		private void ConfigureListSortStatusStateMachine()
		{
			ListSortStatusStateMachine.Configure(State.Unsorted)
			.OnEntry(GetPairsUnsorted)
			.Permit(Trigger.SortByKeyAscending, State.SortedByKeyAscending)
			.Permit(Trigger.SortByValueAscending, State.SortedByValueAscending);

			ListSortStatusStateMachine.Configure(State.SortedByKeyAscending)
			.OnEntry(GetPairsSortedByKeyAscending)
			.Permit(Trigger.SortByKeyDescending, State.SortedByKeyDescending)
			.Permit(Trigger.SortByValueAscending, State.SortedByValueAscending);

			ListSortStatusStateMachine.Configure(State.SortedByKeyDescending)
			.OnEntry(GetPairsSortedByKeyDescending)
			.Permit(Trigger.Unsort, State.Unsorted)
			.Permit(Trigger.SortByValueAscending, State.SortedByValueAscending);

			ListSortStatusStateMachine.Configure(State.SortedByValueAscending)
			.OnEntry(GetPairsSortedByValueAscending)
			.Permit(Trigger.SortByKeyAscending, State.SortedByKeyAscending)
			.Permit(Trigger.SortByValueDescending, State.SortedByValueDescending);

			ListSortStatusStateMachine.Configure(State.SortedByValueDescending)
			.OnEntry(GetPairsSortedByValueDescending)
			.Permit(Trigger.SortByKeyAscending, State.SortedByKeyAscending)
			.Permit(Trigger.Unsort, State.Unsorted);
		}

		//If a new pair is added by the service get the updated list from the service
		public void OnPairAdded(object? sender, EventArgs e)
		{
			Pairs = PairService.GetPairs();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		//FIres the Propety Changed event when any property is changed
		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void RemovePair()
		{
			PairService.RemovePair(SelectedPair);
		}

		//Check the current sort state and move to the next one in the order of
		//Unsorted or Value Sorted -> KeyAscending -> KeyDescending -> Unsorted
		public void SortByKey()
		{
			switch (ListSortStatusStateMachine.State)
			{
				case State.Unsorted:
					ListSortStatusStateMachine.Fire(Trigger.SortByKeyAscending);
					break;
				case State.SortedByKeyAscending:
					ListSortStatusStateMachine.Fire(Trigger.SortByKeyDescending);
					break;
				case State.SortedByKeyDescending:
					ListSortStatusStateMachine.Fire(Trigger.Unsort);
					break;
				case State.SortedByValueAscending:
					ListSortStatusStateMachine.Fire(Trigger.SortByKeyAscending);
					break;
				case State.SortedByValueDescending:
					ListSortStatusStateMachine.Fire(Trigger.SortByKeyAscending);
					break;
				default:
					break;
			}
		}

		//Check the current sort state and move to the next one in the order of 
		//Unsorted or Key Sorted-> ValueAscending -> ValueDescending -> Unsorted
		public void SortByValue()
		{
			switch (ListSortStatusStateMachine.State)
			{
				case State.Unsorted:
					ListSortStatusStateMachine.Fire(Trigger.SortByValueAscending);
					break;
				case State.SortedByKeyAscending:
					ListSortStatusStateMachine.Fire(Trigger.SortByValueAscending);
					break;
				case State.SortedByKeyDescending:
					ListSortStatusStateMachine.Fire(Trigger.SortByValueAscending);
					break;
				case State.SortedByValueAscending:
					ListSortStatusStateMachine.Fire(Trigger.SortByValueDescending);
					break;
				case State.SortedByValueDescending:
					ListSortStatusStateMachine.Fire(Trigger.Unsort);
					break;
				default:
					break;
			}
		}

		private void GetPairsUnsorted()
		{
			Pairs = PairService.GetPairs();
		}

		private void GetPairsSortedByKeyDescending()
		{
			Pairs = PairService.GetPairsSortedByKeyDescending();
		}

		private void GetPairsSortedByKeyAscending()
		{
			Pairs = PairService.GetPairsSortedByKeyAscending();
		}

		private void GetPairsSortedByValueDescending()
		{
			Pairs = PairService.GetPairsSortedByValueDescending();
		}

		private void GetPairsSortedByValueAscending()
		{
			Pairs = PairService.GetPairsSortedByValueAscending();
		}

		//The states for the List Sort Status state machine
		private enum State 
		{
			Unsorted,
			SortedByKeyAscending,
			SortedByKeyDescending,
			SortedByValueAscending,
			SortedByValueDescending
		}

		//The triggers for the List Sort Status state machine
		private enum Trigger
		{
			Unsort,
			SortByKeyAscending,
			SortByKeyDescending,
			SortByValueAscending,
			SortByValueDescending
		}
	}
}
