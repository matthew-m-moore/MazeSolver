using System;
using System.Linq;
using IO.Utilities;
using MazeSolver.Core.SolverLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeSolver.Tests.Core.SolverLogic
{
    [TestClass]
    public class BreadthFirstMazeSolutionTests
    {
        [TestMethod, Owner("Matthew Moore")]
        [ExpectedException(typeof(Exception))]
        public void SolveMaze_SuperSimpleMazeCannotBeSolved_ThrowsException()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SuperSimpleMazeCannotBeSolved.png";
            var mazeReader = new MazeReaderUtility(mazeImageFilePath);
            var mazeToTest = mazeReader.ReadInMazeImage();
            var mazeSolution = new BreadthFirstMazeSolution(mazeToTest);

            mazeSolution.SolveMaze();
        }

        [TestMethod, Owner("Matthew Moore")]
        public void SolveMaze_SuperSimpleMaze_NumberOfStepsToSolveAsExpected()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SuperSimpleMaze.png";
            var mazeReader = new MazeReaderUtility(mazeImageFilePath);
            var mazeToTest = mazeReader.ReadInMazeImage();
            var mazeSolution = new BreadthFirstMazeSolution(mazeToTest);

            mazeSolution.SolveMaze();

            var stepsToSolveMaze = mazeSolution.PathToSolveMaze.Count();
            Assert.AreEqual(stepsToSolveMaze, 253);

            var gridpointsVisited = mazeSolution.MazeToSolve.MazeGridpoints.Values.Count(m => m.HasBeenVisited);
            Assert.AreEqual(gridpointsVisited, 1166);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void SolveMaze_UltraTinyMaze_NumberOfStepsToSolveAsExpected()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\UltraTinyMaze.png";
            var mazeReader = new MazeReaderUtility(mazeImageFilePath);
            var mazeToTest = mazeReader.ReadInMazeImage();
            var mazeSolution = new BreadthFirstMazeSolution(mazeToTest);

            mazeSolution.SolveMaze();

            var stepsToSolveMaze = mazeSolution.PathToSolveMaze.Count();
            Assert.AreEqual(stepsToSolveMaze, 95);

            var gridpointsVisited = mazeSolution.MazeToSolve.MazeGridpoints.Values.Count(m => m.HasBeenVisited);
            Assert.AreEqual(gridpointsVisited, 180);
        }
    }
}
