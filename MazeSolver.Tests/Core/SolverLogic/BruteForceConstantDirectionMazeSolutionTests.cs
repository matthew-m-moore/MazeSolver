using System;
using System.Linq;
using IO.Utilities;
using MazeSolver.Core.SolverLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeSolver.Tests.Core.SolverLogic
{
    [TestClass]
    public class BruteForceConstantDirectionMazeSolutionTests
    {
        [TestMethod, Owner("Matthew Moore")]
        public void SolveMaze_SuperSimpleMaze_NumberOfStepsToSolveAsExpected()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SuperSimpleMaze.png";
            var mazeReader = new MazeReaderUtility(mazeImageFilePath);
            var mazeToTest = mazeReader.ReadInMazeImage();
            var mazeSolution = new BruteForceConstantDirectionMazeSolution(mazeToTest);

            mazeSolution.SolveMaze();

            var stepsToSolveMaze = mazeSolution.PathToSolveMaze.Count();
            Assert.AreEqual(stepsToSolveMaze, 307);

            var gridpointsVisited = mazeSolution.MazeToSolve.MazeGridpoints.Values.Count(m => m.HasBeenVisited);
            Assert.AreEqual(gridpointsVisited, 390);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void SolveMaze_UltraTinyMaze_NumberOfStepsToSolveAsExpected()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\UltraTinyMaze.png";
            var mazeReader = new MazeReaderUtility(mazeImageFilePath);
            var mazeToTest = mazeReader.ReadInMazeImage();
            var mazeSolution = new BruteForceConstantDirectionMazeSolution(mazeToTest);

            mazeSolution.SolveMaze();

            var stepsToSolveMaze = mazeSolution.PathToSolveMaze.Count();
            Assert.AreEqual(stepsToSolveMaze, 115);

            var gridpointsVisited = mazeSolution.MazeToSolve.MazeGridpoints.Values.Count(m => m.HasBeenVisited);
            Assert.AreEqual(gridpointsVisited, 125);
        }
    }
}
