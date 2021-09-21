using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

using PairBear.Data;

namespace PairBear.Data.Test
{
	[TestClass]
	public class PairRepositoryTest
	{
		public PairRepositoryTest()
		{

		}

		[TestMethod]
		public void Getting_a_pair_with_a_string_should_return_a_pair()
		{
			//Arrange
			var createdPair = new Pair("Test", "Pair");
			var mockList = new List<Pair>();
			mockList.Add(createdPair);
			var Repository = new PairRepository(mockList);
			//Act
			var retrievedPair = Repository.GetPairById(createdPair.Id.ToString());

			//Assert
			Assert.AreEqual(retrievedPair, createdPair);
		}

		[TestMethod]
		public void Getting_a_pair_with_a_guid_should_return_a_pair()
		{
			//Arrange
			var createdPair = new Pair("Test", "Pair");
			var mockList = new List<Pair>();
			mockList.Add(createdPair);
			var Repository = new PairRepository(mockList);
			//Act
			var retrievedPair = Repository.GetPairById(createdPair.Id);

			//Assert
			Assert.AreEqual(retrievedPair, createdPair);
		}

		[TestMethod]
		public void Getting_a_pair_with_an_invalid_id_should_return_null()
		{
			//Arrange
			var createdPair = new Pair("Test", "Pair");
			var mockList = new List<Pair>();
			mockList.Add(createdPair);
			var Repository = new PairRepository(mockList);
			//Act
			var retrievedPair = Repository.GetPairById("Blah");

			//Assert
			Assert.IsNull(retrievedPair);
		}

		[TestMethod]
		public void Removing_a_pair_with_an_invalid_id_should_return_false()
		{
			//Arrange
			var createdPair = new Pair("Test", "Pair");
			var mockList = new List<Pair>();
			mockList.Add(createdPair);
			var Repository = new PairRepository(mockList);
			//Act
			var isSuccessful = Repository.RemovePairById("Blah");

			//Assert
			Assert.IsFalse(isSuccessful);
		}
	}
}
