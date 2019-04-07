using System.Linq;
using System.Collections.Generic;
using MazeSolver.Common;
using MazeSolver.Core.MazeElements;
using MazeSolver.Core.SolverLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeSolver.Tests.Core.SolverLogic
{
    [TestClass]
    public class MazeSolutionTests
    {
        [TestMethod, Owner("Matthew Moore")]
        public void MarkSolutionPath_PathToSolutionHasSomeElements_MazeHasSameNumberOfGridpointsOnSolutionPath()
        {
            var listOfMazeGridpoints = new List<MazeGridpoint>
            {
                new MazeGridpoint(new CartesianCoordinate(  2 ,  0), new DirectionsAvailable(),  true, false, false),
                new MazeGridpoint(new CartesianCoordinate(  0,   2), new DirectionsAvailable(), false, false,  true),

                new MazeGridpoint(new CartesianCoordinate(  1 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  3 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  1 ,  2), new DirectionsAvailable(), false, false, false),

                new MazeGridpoint(new CartesianCoordinate(  2,   2), new DirectionsAvailable(), false, false, false),

                new MazeGridpoint(new CartesianCoordinate(  3 ,  2), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  1 ,  3), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  3), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  3 ,  3), new DirectionsAvailable(), false, false, false),

                new MazeGridpoint(new CartesianCoordinate(  4 ,  2), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  4), new DirectionsAvailable(), false,  true, false)
            };

            var maze = new Maze(listOfMazeGridpoints.ToDictionary(m => m.Position, m => m));

            var solutionPath = new List<MazeSolutionElement>
            {
                new MazeSolutionElement
                {
                    MazeGridpoint = maze.MazeGridpoints[new CartesianCoordinate(2, 0)]
                },
                new MazeSolutionElement
                {
                    MazeGridpoint = maze.MazeGridpoints[new CartesianCoordinate(2, 1)]
                },
                new MazeSolutionElement
                {
                    MazeGridpoint = maze.MazeGridpoints[new CartesianCoordinate(2, 2)]
                },
                new MazeSolutionElement
                {
                    MazeGridpoint = maze.MazeGridpoints[new CartesianCoordinate(2, 3)]
                },
                new MazeSolutionElement
                {
                    MazeGridpoint = maze.MazeGridpoints[new CartesianCoordinate(2, 4)]
                }
            };

            var mazeSolution = new MazeSolution(maze);

            mazeSolution.PathToSolveMaze = solutionPath;
            mazeSolution.MarkSolutionPath();

            Assert.AreEqual(
                mazeSolution.PathToSolveMaze.Count(),
                mazeSolution.MazeToSolve.MazeGridpoints.Values.Count(m => m.IsOnSolutionPath));
        }
    }
}
