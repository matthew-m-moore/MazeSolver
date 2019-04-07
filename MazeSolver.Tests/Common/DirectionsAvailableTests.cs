using MazeSolver.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeSolver.Tests.Common
{
    [TestClass]
    public class DirectionsAvailableTests
    {
        [TestMethod, Owner("Matthew Moore")]
        public void OpenPaths_ObjectInitialized_AllDirectionsAreOpen()
        {
            var directions = new DirectionsAvailable();

            Assert.IsTrue(directions.OpenPaths.Contains(DirectionEnum.Up));
            Assert.IsTrue(directions.OpenPaths.Contains(DirectionEnum.Down));
            Assert.IsTrue(directions.OpenPaths.Contains(DirectionEnum.Left));
            Assert.IsTrue(directions.OpenPaths.Contains(DirectionEnum.Right));
        }

        [TestMethod, Owner("Matthew Moore")]
        public void OpenPaths_VariousDirectionsAreSetToFalse_OpenPathsAsExpected()
        {
            var directions = new DirectionsAvailable();

            directions[DirectionEnum.Right] = false;

            Assert.IsTrue(directions.OpenPaths.Contains(DirectionEnum.Up));
            Assert.IsTrue(directions.OpenPaths.Contains(DirectionEnum.Down));
            Assert.IsTrue(directions.OpenPaths.Contains(DirectionEnum.Left));
            Assert.IsFalse(directions.OpenPaths.Contains(DirectionEnum.Right));

            directions[DirectionEnum.Up] = false;

            Assert.IsFalse(directions.OpenPaths.Contains(DirectionEnum.Up));
            Assert.IsTrue(directions.OpenPaths.Contains(DirectionEnum.Down));
            Assert.IsTrue(directions.OpenPaths.Contains(DirectionEnum.Left));
            Assert.IsFalse(directions.OpenPaths.Contains(DirectionEnum.Right));

            directions[DirectionEnum.Left] = false;

            Assert.IsFalse(directions.OpenPaths.Contains(DirectionEnum.Up));
            Assert.IsTrue(directions.OpenPaths.Contains(DirectionEnum.Down));
            Assert.IsFalse(directions.OpenPaths.Contains(DirectionEnum.Left));
            Assert.IsFalse(directions.OpenPaths.Contains(DirectionEnum.Right));
        }

        [TestMethod, Owner("Matthew Moore")]
        public void Count_VariousDirectionsAreSetToFalse_CountAsExpected()
        {
            var directions = new DirectionsAvailable();
            Assert.AreEqual(directions.Count, 4);

            directions[DirectionEnum.Right] = false;
            Assert.AreEqual(directions.Count, 3);

            directions[DirectionEnum.Up] = false;
            Assert.AreEqual(directions.Count, 2);

            directions[DirectionEnum.Left] = false;
            Assert.AreEqual(directions.Count, 1);
        }
    }
}
