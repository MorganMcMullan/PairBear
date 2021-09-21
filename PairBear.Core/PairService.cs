using PairBear.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PairBear.Service
{
	public class PairService : IPairService
	{
		public IPairRepository PairRepository { get; set; }

		public PairService(IPairRepository pairRepository)
		{
			PairRepository = pairRepository;
		}

		public event EventHandler RepositoryUpdated;

		public IEnumerable<Pair> GetPairs()
		{
			return PairRepository.GetAllPairs();
		}

		public IEnumerable<Pair> GetPairsSortedByKeyAscending()
		{
			return PairRepository.GetAllPairs().OrderBy(p => p.Key);
		}

		public IEnumerable<Pair> GetPairsSortedByKeyDescending()
		{
			return PairRepository.GetAllPairs().OrderByDescending(p => p.Key);
		}

		public IEnumerable<Pair> GetPairsSortedByValueAscending()
		{
			return PairRepository.GetAllPairs().OrderBy(p => p.Value);
		}

		public IEnumerable<Pair> GetPairsSortedByValueDescending()
		{
			return PairRepository.GetAllPairs().OrderByDescending(p => p.Value);
		}

		public void AddPair(Pair newPair)
		{
			PairRepository.AddPair(newPair);
			RepositoryUpdated?.Invoke(this, EventArgs.Empty);
		}

		public void RemovePair(Pair pair)
		{
			PairRepository.RemovePairById(pair.Id);
			RepositoryUpdated.Invoke(this, EventArgs.Empty);
		}
	}
}
