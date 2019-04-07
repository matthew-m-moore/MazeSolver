using System;
using System.Collections.Generic;
using System.Linq;
using MazeSolver.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeSolver.Tests.Common
{
    [TestClass]
    public class CartesianCoordinateTests
    {
        [TestMethod, Owner("Matthew Moore")]
        public void MoveInDirection_MoveUp_ResultsAsExpected()
        {
            var coordinate = new CartesianCoordinate(1, 1);
            var newCoordinate = coordinate.MoveInDirection(DirectionEnum.Up);
            var expectedCoordinate = new CartesianCoordinate(1, 0);

            Assert.AreEqual(newCoordinate, expectedCoordinate);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void MoveInDirection_MoveDown_ResultsAsExpected()
        {
            var coordinate = new CartesianCoordinate(1, 1);
            var newCoordinate = coordinate.MoveInDirection(DirectionEnum.Down);
            var expectedCoordinate = new CartesianCoordinate(1, 2);

            Assert.AreEqual(newCoordinate, expectedCoordinate);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void MoveInDirection_MoveLeft_ResultsAsExpected()
        {
            var coordinate = new CartesianCoordinate(1, 1);
            var newCoordinate = coordinate.MoveInDirection(DirectionEnum.Left);
            var expectedCoordinate = new CartesianCoordinate(0, 1);

            Assert.AreEqual(newCoordinate, expectedCoordinate);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void MoveInDirection_MoveRight_ResultsAsExpected()
        {
            var coordinate = new CartesianCoordinate(1, 1);
            var newCoordinate = coordinate.MoveInDirection(DirectionEnum.Right);
            var expectedCoordinate = new CartesianCoordinate(2, 1);

            Assert.AreEqual(newCoordinate, expectedCoordinate);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void MoveInDirection_MoveUpThenLeft_DistanceToStartingPointIsRootOfTwo()
        {
            var coordinate = new CartesianCoordinate(1, 1);
            var newCoordinate = coordinate.MoveInDirection(DirectionEnum.Up);
            newCoordinate = newCoordinate.MoveInDirection(DirectionEnum.Left);

            var expectedCoordinate = new CartesianCoordinate(0, 0);
            Assert.AreEqual(newCoordinate, expectedCoordinate);

            var distance = coordinate.Distance(newCoordinate);
            Assert.AreEqual(distance, Math.Sqrt(2), 0.00000001);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void MoveInDirection_MoveDownThenRight_DistanceToStartingPointIsRootOfTwo()
        {
            var coordinate = new CartesianCoordinate(1, 1);
            var newCoordinate = coordinate.MoveInDirection(DirectionEnum.Down);
            newCoordinate = newCoordinate.MoveInDirection(DirectionEnum.Right);

            var expectedCoordinate = new CartesianCoordinate(2, 2);
            Assert.AreEqual(newCoordinate, expectedCoordinate);

            var distance = coordinate.Distance(newCoordinate);
            Assert.AreEqual(distance, Math.Sqrt(2), 0.00000001);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void Distance_ProvideTwoCoordinates_DistanceMeasuredMatchesExpectations()
        {
            var coordinateA = new CartesianCoordinate(0, 0);
            var coordinateB = new CartesianCoordinate(1, 0);
            Assert.AreEqual(coordinateA.Distance(coordinateB), 1.0, 0.00000001);

            var coordinateC = new CartesianCoordinate(1, 2);
            var coordinateD = new CartesianCoordinate(1, 1);
            Assert.AreEqual(coordinateC.Distance(coordinateD), 1.0, 0.00000001);

            var coordinateE = new CartesianCoordinate(1, 1);
            var coordinateF = new CartesianCoordinate(3, 3);
            Assert.AreEqual(coordinateE.Distance(coordinateF), 2.0 * Math.Sqrt(2), 0.00000001);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void Equals_CoordinatesAreTheSame_ReturnsTrue()
        {
            var coordinateA = new CartesianCoordinate(1, 1);
            var coordinateB = new CartesianCoordinate(1, 1);

            Assert.IsTrue(coordinateA == coordinateB);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void Equals_OneCoordinateIsNull_ReturnsFalse()
        {
            var coordinateA = new CartesianCoordinate(1, 1);
            CartesianCoordinate coordinateB = null;

            Assert.IsFalse(coordinateA == coordinateB);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void NotEquals_CoordinatesAreDifferent_ReturnsTrue()
        {
            var coordinateA = new CartesianCoordinate(1, 1);
            var coordinateB = new CartesianCoordinate(2, 2);

            Assert.IsTrue(coordinateA != coordinateB);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void GetHashCode_ListOfCoordinatesProvide_LinqDistinctReturnsCorrectNumberOfCoordinates()
        {
            var listOfCoordinates = new List<CartesianCoordinate>
            {
                new CartesianCoordinate(0, 0),
                new CartesianCoordinate(0, 0),

                new CartesianCoordinate(1, 1),
                new CartesianCoordinate(1, 1),
                new CartesianCoordinate(1, 1),

                new CartesianCoordinate(2, 2),
                new CartesianCoordinate(2, 2),
                new CartesianCoordinate(2, 2),
                new CartesianCoordinate(2, 2)
            };

            var distinctCoordinates = listOfCoordinates.Distinct();

            Assert.AreEqual(distinctCoordinates.Count(), 3);
        }
    }
}
