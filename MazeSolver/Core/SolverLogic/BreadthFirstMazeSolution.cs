using System;
using System.Collections.Generic;
using System.Linq;
using MazeSolver.Core.MazeElements;
using MazeSolver.Core.PreTreatments;

namespace MazeSolver.Core.SolverLogic
{
    /// <summary>
    /// This is a non-recursive implementation of classical breadth-first search using a
    /// Queue data structure. The first published proposal of using this algorithm for
    /// solving a maze was in the 1950s.
    /// </summary>
    public class BreadthFirstMazeSolution : MazeSolution
    {
        public override string Description => "Breadth-First Solver Logic";
        private bool _foundMazeFinishPoint;

        public BreadthFirstMazeSolution(Maze mazeToSolve) : base(mazeToSolve)
        {
            PreTreatmentLogics = new List<PreTreatmentLogic>
            {
                new DetermineAllOpenPathsAtStartPoint(mazeToSolve)
            };
        }

        /// <summary>
        /// Solves a maze using a non-recursive breath-first approach.
        /// </summary>
        public override void SolveMaze()
        {
            // Apply all pre-treament logics, if any
            PreTreatmentLogics.ForEach(p => p.PreTreatMazeToSolve());

            // Set all parent gridpoints to null at the start
            PathToSolveMaze = MazeToSolve.MazeGridpoints.Select(mazeGridpoint =>
                new MazeSolutionElement
                {
                    ParentMazeGridpoint = null,
                    MazeGridpoint = mazeGridpoint.Value
                }).ToList();

            var queueToSolveMaze = new Queue<MazeGridpoint>();

            // Pick the first satisfactory gridpoint as the starting point
            var startMazeGridpoint = MazeToSolve.MazeGridpoints.Values.First(m => m.IsStartPoint && m.DirectionsAvailable.Count > 0);

            queueToSolveMaze.Enqueue(startMazeGridpoint);

            var finalMazeGridpoint = SearchForSolution(queueToSolveMaze);
            var finalMazeSolutionElement = PathToSolveMaze.Single(m => m.MazeGridpoint == finalMazeGridpoint);

            ConstructPathToSolveMaze(finalMazeSolutionElement);
        }

        private MazeGridpoint SearchForSolution(Queue<MazeGridpoint> queueToSolveMaze)
        {
            // As long as there are still elements in the queue to check, keep going
            while (queueToSolveMaze.Any())
            {
                var currentMazeGridpoint = queueToSolveMaze.Dequeue();
                currentMazeGridpoint.HasBeenVisited = true;
                MazeToSolve.NotifyMazeHasBeenUpdated();
                MazeToSolve.NotifyMazeToBeRedrawnUpdated();

                _foundMazeFinishPoint = MazeToSolve.CheckIfAtFinish(currentMazeGridpoint);

                // Quit now if the end of the maze was found
                if (_foundMazeFinishPoint) return currentMazeGridpoint;

                // Investigate each direction available at the current gridpoint
                foreach (var direction in currentMazeGridpoint.DirectionsAvailable.OpenPaths)
                {
                    if (!MazeToSolve.IsValidPositionInMaze(currentMazeGridpoint, direction)) continue;

                    var nextMazeGridpoint = MazeToSolve.ProceedToNewGridpoint(currentMazeGridpoint, direction);

                    if (MazeToSolve.CheckIfAtDeadEnd(nextMazeGridpoint) || nextMazeGridpoint.HasBeenVisited) continue;

                    // Set up the parent gridpoint for each direction that is valid
                    var nextMazeSolutionElement = PathToSolveMaze.Single(m => m.MazeGridpoint == nextMazeGridpoint);
                    nextMazeSolutionElement.ParentMazeGridpoint = currentMazeGridpoint;

                    nextMazeGridpoint.HasBeenVisited = true;

                    // Make sure we haven't found the end of the maze for each direction
                    _foundMazeFinishPoint = MazeToSolve.CheckIfAtFinish(nextMazeGridpoint);

                    if (_foundMazeFinishPoint) return nextMazeGridpoint;

                    // If its not the end of the maze, queue it up for another round down the line
                    queueToSolveMaze.Enqueue(nextMazeGridpoint);

                    Console.WriteLine("X : " + nextMazeGridpoint.Position.X + ", Y : " + nextMazeGridpoint.Position.Y + " Direction : " + direction);
                }
            }

            throw new Exception("Maze Solver Error: Sorry, maze could not be solved. The maze image provided may be invalid.");
        }

        private void ConstructPathToSolveMaze(MazeSolutionElement mazeSolutionElement)
        {
            var pathToSolveMaze = new List<MazeSolutionElement>();
            pathToSolveMaze.Add(mazeSolutionElement);

            // Go back from parent to parent to find the tree that results in a solution
            // The starting solution element will have a parent that is null
            while (pathToSolveMaze.Last().ParentMazeGridpoint != null)
            {
                var previousMazeGridpoint = mazeSolutionElement.ParentMazeGridpoint;
                var previousMazeSolutionElement = PathToSolveMaze.Single(m => m.MazeGridpoint == previousMazeGridpoint);

                pathToSolveMaze.Add(previousMazeSolutionElement);
                mazeSolutionElement = previousMazeSolutionElement;
            }

            PathToSolveMaze = pathToSolveMaze;
        }
    }
}
