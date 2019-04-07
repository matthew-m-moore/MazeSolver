using System;
using IO.Utilities;
using Maze.Listeners;
using MazeSolver.Core.SolverLogic;

namespace Maze
{
    public class Program
    {
        /// <summary>
        /// Solves an input maze by outputting an image with the solution traced in green
        /// and any spaces visited colored gray.
        /// 
        /// Arguments Usage:
        /// [0] - Required - Path to the maze image (string)
        /// [1] - Required - Path to write the solution image (string)
        /// [2] - Optional - Specify to have the solution image be updated in real time (bool)
        /// [3] - Optional - Specify which solver logic to use (int)
        /// </summary>
        static void Main(string[] args)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var watchMazeGetSolved = false;

            var pathToMazeImageFile = args[0];
            var mazeReader = new MazeReaderUtility(pathToMazeImageFile);

            var mazeSolutionType = -1;
            if (args.Length > 3)
            {
                mazeSolutionType = Convert.ToInt32(args[3]);
            }

            var mazeToSolve = mazeReader.ReadInMazeImage();
            var mazeSolution = InstantiateMazeSolution(mazeToSolve, mazeSolutionType);

            var pathToMazeSolutionImageFile = args[1];
            var mazeWriter = new MazeWriterUtility(pathToMazeSolutionImageFile, mazeSolution.MazeToSolve);

            if (args.Length > 2)
            {
                watchMazeGetSolved = bool.Parse(args[2]);
            }
            var mazeUpdatedListener = new MazeUpdatedListener();
            if (watchMazeGetSolved)
            {         
                mazeUpdatedListener.Subscribe(mazeSolution, mazeWriter);
            }

            mazeSolution.SolveMaze();
            mazeSolution.MarkSolutionPath();

            mazeUpdatedListener.UnSubscribe(mazeSolution);
            mazeWriter.SaveMazeImage();

            stopwatch.Stop();
            var elapsedTimeInMilliseconds = stopwatch.ElapsedMilliseconds;

            Console.WriteLine("Elapsed Time in Seconds: " + elapsedTimeInMilliseconds / 1000d);
            Console.WriteLine("Number of Steps: " + mazeSolution.PathToSolveMaze.Count);
        }

        private static MazeSolution InstantiateMazeSolution(MazeSolver.Core.MazeElements.Maze mazeToSolve, int type)
        {
            switch (type)
            {
                case 0:
                    return new BruteForceWallHuggingMazeSolution(mazeToSolve);
                case 1:
                    return new BreadthFirstMazeSolution(mazeToSolve);
                case 2:
                    return new BruteForceMazeSolution(mazeToSolve);
                case 3:
                    return new BruteForceConstantDirectionMazeSolution(mazeToSolve);
                default:
                    return new BruteForceWallHuggingMazeSolution(mazeToSolve);
            }
        }
    }
}
