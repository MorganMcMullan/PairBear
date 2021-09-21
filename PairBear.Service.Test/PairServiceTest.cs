using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PairBear.Data;
using System.Collections.Generic;

namespace PairBear.Service.Test
{
	[TestClass]
	public class PairServiceTest
	{
		[TestMethod]
		public void Get_pairs_should_return_an_unsorted_list()
		{
			//Arrange
			var returnedList = new List<Pair>();
			returnedList.Add(new Pair("B", "B"));
			returnedList.Add(new Pair("A", "A"));
			returnedList.Add(new Pair("C", "C"));
			returnedList.Add(new Pair("A", "A"));
			var mockRepository = new Mock<IPairRepository>();
			mockRepository.Setup(r => r.GetAllPairs()).Returns(returnedList);
			var pairService = new PairService(mockRepository.Object);

			//Act
			var result = new List<Pair>(pairService.GetPairs());

			//Assert
			Assert.AreEqual(result[0], returnedList[0]);
			Assert.AreEqual(result[1], returnedList[1]);
			Assert.AreEqual(result[2], returnedList[2]);
			Assert.AreEqual(result[3], returnedList[3]);
		}

		[TestMethod]
		public void Get_pairs_sorted_by_kay_ascending_should_return_a_list_sorted_by_key_ascending()
		{
			//Arrange
			var returnedList = new List<Pair>();
			returnedList.Add(new Pair("B", "B"));
			returnedList.Add(new Pair("A", "A"));
			returnedList.Add(new Pair("C", "C"));
			returnedList.Add(new Pair("A", "A"));
			var mockRepository = new Mock<IPairRepository>();
			mockRepository.Setup(r => r.GetAllPairs()).Returns(returnedList);
			var pairService = new PairService(mockRepository.Object);

			//Act
			var result = new List<Pair>(pairService.GetPairsSortedByKeyAscending());

			//Assert
			Assert.AreEqual(result[0], returnedList[1]);
			Assert.AreEqual(result[1], returnedList[3]);
			Assert.AreEqual(result[2], returnedList[0]);
			Assert.AreEqual(result[3], returnedList[2]);
		}

		[TestMethod]
		public void Get_pairs_sorted_by_kay_descending_should_return_a_list_sorted_by_key_descending()
		{
			//Arrange
			var returnedList = new List<Pair>();
			returnedList.Add(new Pair("B", "B"));
			returnedList.Add(new Pair("A", "A"));
			returnedList.Add(new Pair("C", "C"));
			returnedList.Add(new Pair("A", "A"));
			var mockRepository = new Mock<IPairRepository>();
			mockRepository.Setup(r => r.GetAllPairs()).Returns(returnedList);
			var pairService = new PairService(mockRepository.Object);
			//Act
			var result = new List<Pair>(pairService.GetPairsSortedByKeyDescending());

			//Assert
			Assert.AreEqual(result[0], returnedList[2]);
			Assert.AreEqual(result[1], returnedList[0]);
			Assert.AreEqual(result[2], returnedList[1]);
			Assert.AreEqual(result[3], returnedList[3]);
		}

		[TestMethod]
		public void Get_pairs_sorted_by_value_ascending_should_return_a_list_sorted_by_value_ascending()
		{
			//Arrange
			var returnedList = new List<Pair>();
			returnedList.Add(new Pair("B", "B"));
			returnedList.Add(new Pair("A", "A"));
			returnedList.Add(new Pair("C", "C"));
			returnedList.Add(new Pair("A", "A"));
			var mockRepository = new Mock<IPairRepository>();
			mockRepository.Setup(r => r.GetAllPairs()).Returns(returnedList);
			var pairService = new PairService(mockRepository.Object);

			//Act
			var result = new List<Pair>(pairService.GetPairsSortedByValueAscending());

			//Assert
			Assert.AreEqual(result[0], returnedList[1]);
			Assert.AreEqual(result[1], returnedList[3]);
			Assert.AreEqual(result[2], returnedList[0]);
			Assert.AreEqual(result[3], returnedList[2]);
		}

		[TestMethod]
		public void Get_pairs_sorted_by_value_descending_should_return_a_list_sorted_by_value_descending()
		{
			//Arrange
			var returnedList = new List<Pair>();
			returnedList.Add(new Pair("B", "B"));
			returnedList.Add(new Pair("A", "A"));
			returnedList.Add(new Pair("C", "C"));
			returnedList.Add(new Pair("A", "A"));
			var mockRepository = new Mock<IPairRepository>();
			mockRepository.Setup(r => r.GetAllPairs()).Returns(returnedList);
			var pairService = new PairService(mockRepository.Object);

			//Act
			var result = new List<Pair>(pairService.GetPairsSortedByValueDescending());

			//Assert
			Assert.AreEqual(result[0], returnedList[2]);
			Assert.AreEqual(result[1], returnedList[0]);
			Assert.AreEqual(result[2], returnedList[1]);
			Assert.AreEqual(result[3], returnedList[3]);
		}

		[TestMethod]
		public void Add_pair_should_add_a_pair_to_the_repository_and_fire_repository_updated_event()
		{
			//Arrange
			var newPair = new Pair("A", "A");
			var repository = new PairRepository();
			var pairService = new PairService(repository);
			var eventfired = false;
			pairService.RepositoryUpdated += (sender, args) => eventfired = true;
			//Act
			pairService.AddPair(newPair);
			var result = repository.GetPairById(newPair.Id);
			//Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(result, newPair);
			Assert.IsTrue(eventfired);
		}

		[TestMethod]
		public void Remove_pair_should_remove_a_pair_from_the_repository_and_fire_repository_updated_event()
		{
			//Arrange
			var newPair = new Pair("A", "A");
			var pairList = new List<Pair>();
			pairList.Add(newPair);
			var repository = new PairRepository(pairList);
			var pairService = new PairService(repository);
			var eventfired = false;
			pairService.RepositoryUpdated += (sender, args) => eventfired = true;
			//Act
			pairService.RemovePair(newPair);
			var result = repository.GetPairById(newPair.Id);
			//Assert
			Assert.IsNull(result);
			Assert.IsTrue(eventfired);
		}
	}
}
