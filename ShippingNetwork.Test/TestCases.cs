using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShippingNetworkLibrary;

namespace ShippingNetwork.Test
{

    [TestClass]
    public class TestCases
    {
        Graph g;
        [TestInitialize]
        public void TestInitialize()
        {
            g = Graph.LoadGraph("Data/network.txt");
        }

        [TestMethod]
        public void Given_Route_When_Executed_Return_TotalJourneyTime()
        {
            //Arrange
            string[] path = { "Buenos Aires", "New York", "Liverpool" };
            //Act
            int result = g.GetJourneyTime(path);
            //Assert
            Assert.AreEqual(result, 10);
        }

        [TestMethod]
        public void Given_InvalidRoute_When_Executed_Return_Minus()
        {
            //Arrange
            string[] path = { "Cape Town", "Buenos Aires" };
            //Act
            int result = g.GetJourneyTime(path);
            //Assert
            Assert.AreEqual(result, -1);
        }
        [TestMethod]
        public void Given_UpperAndLowerCaseRoute_When_Executed_Return_TotalJourneyTime()
        {
            //Arrange
            string[] path = { "buenos Aires", "New york", "liverpool" };
            //Act
            int result = g.GetJourneyTime(path);
            //Assert
            Assert.AreEqual(result, 10);
        }
        [TestMethod]
        public void Given_Route_When_Executed_Return_ShortesPathTime()
        {
            //Arrange
            string from = "Buenos Aires";
            string to = "Liverpool";
            //Act
            List<string> result = g.GetShortestPath(from, to);
            //Assert
            Assert.AreEqual(g.GetJourneyTime(result.ToArray()), 8);
        }

        [TestMethod]
        public void Given_RouteAndMaxStops_When_Executed_Return_TotalRoutesHaveMaxStops()
        {
            //Arrange
            string from = "Liverpool";
            string to = "Liverpool";
            //Act
            int result = g.CountRoutesMaxStops(from, to, 3, false);
            //Assert
            Assert.AreEqual(result, 2);
        }
        [TestMethod]
        public void Given_RouteAndExactStops_When_Executed_Return_TotalRoutesHaveExactStops()
        {
            //Arrange
            string from = "Buenos Aires";
            string to = "Liverpool";
            //Act
            int result = g.CountRoutesMaxStops(from, to, 4, true);
            //Assert
            Assert.AreEqual(result, 1);
        }
        [TestMethod]
        public void Given_RouteAndDays_When_Executed_Return_TotalRoutesHaveLessThanOrEqDays()
        {
            //Arrange
            string from = "Liverpool";
            string to = "Liverpool";
            //Act
            int result = g.CountRoutesJourneyTime(from, to, 25);
            //Assert
            Assert.AreEqual(result, 4);
        }
        [TestMethod]
        public void GetJourneyTimeMethod_Given_WrongNonExistantNode_When_Executed_Return_MinusOne()
        {
            //Arrange
            string[] path = { "Buenos Aires", "Johannesburg", "Liverpool" };
            //Act
            int result = g.GetJourneyTime(path);
            //Assert
            Assert.AreEqual(result, -2);
        }
        [TestMethod]
        public void GetShortestPath_Given_WrongNonExistantNode_When_Executed_Return_Null()
        {
            //Arrange
            string from = "Johannesburg";
            string to = "Liverpool";
            //Act
            List<string> result = g.GetShortestPath(from, to);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetShortestPath_Given_SameFromToNodes_When_Executed_Return_Zero()
        {
            //Arrange
            string from = "New York";
            string to = "New York";
            //Act
            List<string> result = g.GetShortestPath(from, to);
            //Assert
            Assert.AreEqual(g.GetJourneyTime(result.ToArray()), 0);
        }
    }
}
