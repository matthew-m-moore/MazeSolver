using System;
using System.Linq;
using IO.Utilities;
using MazeSolver.Core.PreTreatments;
using MazeSolver.Core.SolverLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maze.IntegrationTests
{
    [TestClass]
    public class SuperSimpleMazeTests
    {
        [TestMethod, Owner("Matthew Moore")]
        public void PreTreatmentMakesMazeEasierToSolve()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SuperSimpleMaze.png";
            var mazeReader = new MazeReaderUtility(mazeImageFilePath);
            var mazeToTest = mazeReader.ReadInMazeImage();
            var mazeSolution = new BruteForceMazeSolution(mazeToTest);

            mazeSolution.SolveMaze();

            var stepsToSolveMazeWithoutPreTreatment = mazeSolution.PathToSolveMaze.Count();

            mazeToTest = mazeReader.ReadInMazeImage();
            mazeSolution = new BruteForceMazeSolution(mazeToTest);
            mazeSolution.PreTreatmentLogics.Add(new RemoveRedundantWhiteSpace(mazeToTest));

            mazeSolution.SolveMaze();

            var stepsToSolveMazeWithPreTreatment = mazeSolution.PathToSolveMaze.Count();

            Assert.IsTrue(stepsToSolveMazeWithPreTreatment < stepsToSolveMazeWithoutPreTreatment);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void MazeIsSolvedAndSolutionIsComplete()
        {
            // Test that start of solution path IsStartPoint and end IsFinishPoint
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SuperSimpleMaze.png";
            var mazeReader = new MazeReaderUtility(mazeImageFilePath);
            var mazeToTest = mazeReader.ReadInMazeImage();
            var mazeSolution = new BruteForceMazeSolution(mazeToTest);

            mazeSolution.SolveMaze();

            Assert.IsTrue(mazeSolution.PathToSolveMaze.First().MazeGridpoint.IsStartPoint);
            Assert.IsTrue(mazeSolution.PathToSolveMaze.Last().MazeGridpoint.IsFinishPoint);

            // Test that solution path is connected all the way through, distance between each point is unity
            var lastSolutionElement = mazeSolution.PathToSolveMaze.First();
            foreach (var solutionElement in mazeSolution.PathToSolveMaze.Skip(1))
            {
                var lastCoordinate = lastSolutionElement.MazeGridpoint.Position;
                var thisCoordinate = solutionElement.MazeGridpoint.Position;

                Assert.AreEqual(lastCoordinate.Distance(thisCoordinate), 1.0, 0.000001);

                lastSolutionElement = solutionElement;
            }
        }
    }
}
