using System;
using System.Collections.Generic;

namespace PairBear.Data
{
	public interface IPairRepository
	{
		public IEnumerable<Pair> GetAllPairs();
		public Pair GetPairById(string id);
		public Pair GetPairById(Guid id);
		public void AddPair(string key, string value);
		public void AddPair(Pair newPair);
		public bool RemovePairById(string id);
		public bool RemovePairById(Guid id);
	}
}