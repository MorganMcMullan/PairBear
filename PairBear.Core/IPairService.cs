using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PairBear.Service
{
	public interface IPairService
	{
		public IEnumerable<Pair> GetPairs();
		public IEnumerable<Pair> GetPairsSortedByKeyDescending();
		public IEnumerable<Pair> GetPairsSortedByKeyAscending();
		public IEnumerable<Pair> GetPairsSortedByValueAscending();
		public IEnumerable<Pair> GetPairsSortedByValueDescending();
		public void AddPair(Pair newPair);
		public void RemovePair(Pair pair);

		public event EventHandler RepositoryUpdated;
	}
}