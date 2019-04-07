using System;
using System.Collections.Generic;
using System.Linq;
using MazeSolver.Common;
using MazeSolver.Core.MazeElements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeSolver.Tests.Core.MazeElements
{
    [TestClass]
    public class MazeTests
    {
        private Maze GetTestMaze()
        {
            var listOfMazeGridpoints = new List<MazeGridpoint>
            {
                new MazeGridpoint(new CartesianCoordinate(0, 0), new DirectionsAvailable(), true, false, false),
                new MazeGridpoint(new CartesianCoordinate(0, 1), new DirectionsAvailable(), false, false, true),
                new MazeGridpoint(new CartesianCoordinate(1, 0), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(1, 1), new DirectionsAvailable(), false, true, false)
            };

            return new Maze(listOfMazeGridpoints.ToDictionary(m => m.Position, m => m));
        }

        [TestMethod, Owner("Matthew Moore")]
        [ExpectedException(typeof(Exception))]
        public void Maze_NoStartPoint_ThrowsException()
        {
            var listOfMazeGridpoints = new List<MazeGridpoint>
            {
                new MazeGridpoint(new CartesianCoordinate(0, 0), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(0, 1), new DirectionsAvailable(), false, false, true),
                new MazeGridpoint(new CartesianCoordinate(1, 0), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(1, 1), new DirectionsAvailable(), false, true, false)
            };

            new Maze(listOfMazeGridpoints.ToDictionary(m => m.Position, m => m));
        }

        [TestMethod, Owner("Matthew Moore")]
        [ExpectedException(typeof(Exception))]
        public void Maze_NoFinishPoint_ThrowsException()
        {
            var listOfMazeGridpoints = new List<MazeGridpoint>
            {
                new MazeGridpoint(new CartesianCoordinate(0, 0), new DirectionsAvailable(), true, false, false),
                new MazeGridpoint(new CartesianCoordinate(0, 1), new DirectionsAvailable(), false, false, true),
                new MazeGridpoint(new CartesianCoordinate(1, 0), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(1, 1), new DirectionsAvailable(), false, false, false)
            };

            new Maze(listOfMazeGridpoints.ToDictionary(m => m.Position, m => m));
        }

        [TestMethod, Owner("Matthew Moore")]
        public void MazeHeight_HeightIsTwo_ReturnsTwo()
        {
            var mazeToTest = GetTestMaze();
            Assert.AreEqual(mazeToTest.MazeHeight, 2);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void MazeWidth_WidthIsTwo_ReturnsTwo()
        {
            var mazeToTest = GetTestMaze();
            Assert.AreEqual(mazeToTest.MazeWidth, 2);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void CheckIfAtFinish_GridpointIsAtFinish_ReturnsTrue()
        {
            var mazeToTest = GetTestMaze();
            var mazeGridpoint = new MazeGridpoint(new CartesianCoordinate(1, 1), new DirectionsAvailable(), false, true, false);
            Assert.IsTrue(mazeToTest.CheckIfAtFinish(mazeGridpoint));
        }

        [TestMethod, Owner("Matthew Moore")]
        public void CheckIfAtFinish_GridpointIsNotAtFinish_ReturnsFalse()
        {
            var mazeToTest = GetTestMaze();
            var mazeGridpoint = new MazeGridpoint(new CartesianCoordinate(0, 0), new DirectionsAvailable(), true, false, false);
            Assert.IsFalse(mazeToTest.CheckIfAtFinish(mazeGridpoint));
        }

        [TestMethod, Owner("Matthew Moore")]
        public void CheckIfAtDeadEnd_GridpointIsAtStart_ReturnsFalse()
        {
            var mazeToTest = GetTestMaze();
            var mazeGridpoint = new MazeGridpoint(new CartesianCoordinate(0, 0), new DirectionsAvailable(), true, false, false);
            Assert.IsFalse(mazeToTest.CheckIfAtDeadEnd(mazeGridpoint));
        }

        [TestMethod, Owner("Matthew Moore")]
        public void CheckIfAtDeadEnd_GridpointIsAtFinish_ReturnsFalse()
        {
            var mazeToTest = GetTestMaze();
            var mazeGridpoint = new MazeGridpoint(new CartesianCoordinate(1, 1), new DirectionsAvailable(), false, true, false);
            Assert.IsFalse(mazeToTest.CheckIfAtDeadEnd(mazeGridpoint));
        }

        [TestMethod, Owner("Matthew Moore")]
        public void CheckIfAtDeadEnd_GridpointHasMoreThanOneOpenPath_ReturnsFalse()
        {
            var mazeToTest = GetTestMaze();
            var mazeGridpoint = new MazeGridpoint(new CartesianCoordinate(1, 0), new DirectionsAvailable(), false, false, false);
            Assert.IsFalse(mazeToTest.CheckIfAtDeadEnd(mazeGridpoint));
        }

        [TestMethod, Owner("Matthew Moore")]
        public void CheckIfAtDeadEnd_GridpointHasOnlyOneOpenPath_ReturnsTrue()
        {
            var mazeToTest = GetTestMaze();
            var mazeGridpoint = new MazeGridpoint(new CartesianCoordinate(1, 0), new DirectionsAvailable(), false, false, false);

            mazeGridpoint.DirectionsAvailable[DirectionEnum.Up] = false;
            mazeGridpoint.DirectionsAvailable[DirectionEnum.Left] = false;
            mazeGridpoint.DirectionsAvailable[DirectionEnum.Right] = false;

            Assert.IsTrue(mazeToTest.CheckIfAtDeadEnd(mazeGridpoint));
        }

        [TestMethod, Owner("Matthew Moore")]
        public void CheckIfAtDeadEnd_GridpointIsWall_ReturnsTrue()
        {
            var mazeToTest = GetTestMaze();
            var mazeGridpoint = new MazeGridpoint(new CartesianCoordinate(0, 1), new DirectionsAvailable(), false, false, true);
            Assert.IsTrue(mazeToTest.CheckIfAtDeadEnd(mazeGridpoint));
        }

        [TestMethod, Owner("Matthew Moore")]
        public void CheckIfCoordinateIsValid_CoordinateIsGreaterThanMazeHeight_ReturnsFalse()
        {
            var mazeToTest = GetTestMaze();
            var coordinate = new CartesianCoordinate(0, 2);
            Assert.IsFalse(mazeToTest.CheckIfCoordinateIsValid(coordinate));
        }

        [TestMethod, Owner("Matthew Moore")]
        public void CheckIfCoordinateIsValid_CoordinateIsGreaterThanMazeWidth_ReturnsFalse()
        {
            var mazeToTest = GetTestMaze();
            var coordinate = new CartesianCoordinate(2, 0);
            Assert.IsFalse(mazeToTest.CheckIfCoordinateIsValid(coordinate));
        }

        [TestMethod, Owner("Matthew Moore")]
        public void CheckIfCoordinateIsValid_CoordinateXPostionIsNegative_ReturnsFalse()
        {
            var mazeToTest = GetTestMaze();
            var coordinate = new CartesianCoordinate(-1, 0);
            Assert.IsFalse(mazeToTest.CheckIfCoordinateIsValid(coordinate));
        }

        [TestMethod, Owner("Matthew Moore")]
        public void CheckIfCoordinateIsValid_CoordinateYPostionIsNegative_ReturnsFalse()
        {
            var mazeToTest = GetTestMaze();
            var coordinate = new CartesianCoordinate(0, -1);
            Assert.IsFalse(mazeToTest.CheckIfCoordinateIsValid(coordinate));
        }

        [TestMethod, Owner("Matthew Moore")]
        public void CheckIfCoordinateIsValid_CoordinateIsInMaze_ReturnsTrue()
        {
            var mazeToTest = GetTestMaze();
            var coordinate = new CartesianCoordinate(1, 1);
            Assert.IsTrue(mazeToTest.CheckIfCoordinateIsValid(coordinate));
        }

        [TestMethod, Owner("Matthew Moore")]
        public void IsValidPositionInMaze_DirectionMovesOutOfMaze_ReturnsFalse()
        {
            var mazeToTest = GetTestMaze();
            var mazeGridpoint = new MazeGridpoint(new CartesianCoordinate(1, 0), new DirectionsAvailable(), false, false, false);
            Assert.IsFalse(mazeToTest.IsValidPositionInMaze(mazeGridpoint, DirectionEnum.Up));
        }

        [TestMethod, Owner("Matthew Moore")]
        public void IsValidPositionInMaze_DirectionStaysInsideOfMaze_ReturnsTrue()
        {
            var mazeToTest = GetTestMaze();
            var mazeGridpoint = new MazeGridpoint(new CartesianCoordinate(1, 0), new DirectionsAvailable(), false, false, false);
            Assert.IsTrue(mazeToTest.IsValidPositionInMaze(mazeGridpoint, DirectionEnum.Down));
        }

        [TestMethod, Owner("Matthew Moore")]
        public void IsValidPositionInMazeAndNotBacktracking_DirectionMovesOutOfMaze_ReturnsFalse()
        {
            var mazeToTest = GetTestMaze();
            var firstMazeGridpoint = new MazeGridpoint(new CartesianCoordinate(0, 0), new DirectionsAvailable(), true, false, false);
            var secondMazeGridpoint = new MazeGridpoint(new CartesianCoordinate(1, 0), new DirectionsAvailable(), false, false, false);
            Assert.IsFalse(mazeToTest.IsValidPositionInMazeAndNotBacktracking(firstMazeGridpoint, secondMazeGridpoint, DirectionEnum.Up));
        }

        [TestMethod, Owner("Matthew Moore")]
        public void IsValidPositionInMazeAndNotBacktracking_DirectionCountsAsBacktracking_ReturnsFalse()
        {
            var mazeToTest = GetTestMaze();
            var firstMazeGridpoint = new MazeGridpoint(new CartesianCoordinate(0, 0), new DirectionsAvailable(), true, false, false);
            var secondMazeGridpoint = new MazeGridpoint(new CartesianCoordinate(1, 0), new DirectionsAvailable(), false, false, false);
            Assert.IsFalse(mazeToTest.IsValidPositionInMazeAndNotBacktracking(firstMazeGridpoint, secondMazeGridpoint, DirectionEnum.Left));
        }

        [TestMethod, Owner("Matthew Moore")]
        public void IsValidPositionInMazeAndNotBacktracking_DirectionStaysInsideOfMazeAndIsNotBacktracking_ReturnsTrue()
        {
            var mazeToTest = GetTestMaze();
            var firstMazeGridpoint = new MazeGridpoint(new CartesianCoordinate(0, 0), new DirectionsAvailable(), true, false, false);
            var secondMazeGridpoint = new MazeGridpoint(new CartesianCoordinate(1, 0), new DirectionsAvailable(), false, false, false);
            Assert.IsTrue(mazeToTest.IsValidPositionInMazeAndNotBacktracking(firstMazeGridpoint, secondMazeGridpoint, DirectionEnum.Down));
        }

        [TestMethod, Owner("Matthew Moore")]
        public void ProceedToNewGridpoint_DirectionIsRight_ExpectedGridpointIsReturned()
        {
            var mazeToTest = GetTestMaze();
            var mazeGridpoint = new MazeGridpoint(new CartesianCoordinate(1, 0), new DirectionsAvailable(), false, false, false);
            var newGridpoint = mazeToTest.ProceedToNewGridpoint(mazeGridpoint, DirectionEnum.Down);

            var expectedGridpoint = new MazeGridpoint(new CartesianCoordinate(1, 1), new DirectionsAvailable(), false, true, false);
            Assert.AreEqual(newGridpoint.Position, expectedGridpoint.Position);
        }

        [TestMethod, Owner("Matthew Moore")]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void ProceedToNewGridpoint_DirectionMovesOutsideOfMaze_ThrowsException()
        {
            var mazeToTest = GetTestMaze();
            var mazeGridpoint = new MazeGridpoint(new CartesianCoordinate(1, 0), new DirectionsAvailable(), false, false, false);
            mazeToTest.ProceedToNewGridpoint(mazeGridpoint, DirectionEnum.Up);
        }
    }
}
