using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairBear
{
	public class Pair
	{
		public Guid Id { get; }
		public string Key { get; }
		public string Value { get; }

		public Pair(string key, string value)
		{
			Id = Guid.NewGuid();
			Key = key;
			Value = value;
		}
	}
}
