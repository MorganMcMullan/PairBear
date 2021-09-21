using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairBear.Data
{
	public class PairRepository : IPairRepository
	{
		private ImmutableList<Pair> Pairs { get; set; }

		public PairRepository()
		{
			Pairs = ImmutableList.Create<Pair>();
		}

		public PairRepository(IEnumerable<Pair> pairs)
		{
			Pairs = ImmutableList.Create<Pair>().AddRange(pairs);
		}

		public IEnumerable<Pair> GetAllPairs()
		{
			return Pairs;
		}

		public Pair GetPairById(string id)
		{
			return Pairs.FirstOrDefault(p => p.Id.ToString() == id);
		}

		public Pair GetPairById(Guid id)
		{
			return Pairs.FirstOrDefault(p => p.Id == id);
		}

		public void AddPair(string key, string value)
		{
			Pairs = Pairs.Add(new Pair(key, value));
		}

		public void AddPair(Pair newPair)
		{
			Pairs = Pairs.Add(newPair);
		}

		public bool RemovePairById(string id)
		{
			try
			{
				Pairs = Pairs.Remove(Pairs.First(p => p.Id.ToString() == id));
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool RemovePairById(Guid id)
		{
			try
			{
				Pairs = Pairs.Remove(Pairs.First(p => p.Id == id));
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
