using System;
using System.Collections.Generic;
using System.Linq;
using MazeSolver.Core.MazeElements;
using MazeSolver.Core.PreTreatments;

namespace MazeSolver.Core.SolverLogic
{
    /// <summary>
    /// A recursive breadth-firse maze solution. Unfortunately it gives a stack-overflow for mazes
    /// larger than about 20,000 pixels.
    /// </summary>
    public class RecursiveBreadthFirstMazeSolution : MazeSolution
    {
        public override string Description => "Breadth-First Solver Logic (Recursive Implementation)";
        private MazeSolutionElementTree _mazeSolutionElementTree { get; set; }
        private bool _foundMazeFinishPoint;

        public RecursiveBreadthFirstMazeSolution(Maze mazeToSolve) : base(mazeToSolve)
        {
            _mazeSolutionElementTree = new MazeSolutionElementTree();
            PreTreatmentLogics = new List<PreTreatmentLogic>
            {
                new DetermineAllOpenPathsAtStartPoint(mazeToSolve)
            };
        }

        /// <summary>
        /// Solves a maze using a recursive breath-first approach.
        /// </summary>
        public override void SolveMaze()
        {
            // Apply all pre-treament logics, if any
            PreTreatmentLogics.ForEach(p => p.PreTreatMazeToSolve());

            // Pick the first satisfactory gridpoint as the starting point
            var currentMazeGridpoint = MazeToSolve.MazeGridpoints.Values.First(m => m.IsStartPoint && m.DirectionsAvailable.Count > 0);

            _mazeSolutionElementTree.MazeGridpoint = currentMazeGridpoint;

            try
            {
                RecursivelySearchForSolution(currentMazeGridpoint, _mazeSolutionElementTree);
            }
            catch (StackOverflowException)
            {
                throw new Exception(
                    "Maze Solver Error: Sorry, this maze is too large to solve using a recursive search algorithm.");
            }

            if (!_foundMazeFinishPoint)
            {
                throw new Exception("Maze Solver Error: Sorry, maze could not be solved. The maze image provided may be invalid.");
            }

            ConstructPathToSolveMaze(_mazeSolutionElementTree);
        }

        private void RecursivelySearchForSolution(MazeGridpoint currentMazeGridpoint, MazeSolutionElementTree parentMazeSolutionElementTree)
        {
            // No need to keep going on this call if the finish has already been found
            if (_foundMazeFinishPoint) return;

            _mazeSolutionElementTree = parentMazeSolutionElementTree;
            _foundMazeFinishPoint = MazeToSolve.CheckIfAtFinish(currentMazeGridpoint);

            currentMazeGridpoint.HasBeenVisited = true;
            MazeToSolve.NotifyMazeHasBeenUpdated();
            MazeToSolve.NotifyMazeToBeRedrawnUpdated();

            // Now that we've checked the current gridpoint, let's make sure we need to do more work
            if (_foundMazeFinishPoint) return;

            // For each direction available that is valid, add an element to the tree and recursively
            // call the search function again.
            foreach (var direction in currentMazeGridpoint.DirectionsAvailable.OpenPaths)
            {
                if (!MazeToSolve.IsValidPositionInMaze(currentMazeGridpoint, direction)) continue;

                var nextMazeGridpoint = MazeToSolve.ProceedToNewGridpoint(currentMazeGridpoint, direction);

                if (MazeToSolve.CheckIfAtDeadEnd(nextMazeGridpoint) || nextMazeGridpoint.HasBeenVisited) continue;

                var nextMazeSolutionElement = new MazeSolutionElementTree
                {
                    ParentSolutionElement = parentMazeSolutionElementTree,
                    MazeGridpoint = nextMazeGridpoint
                };

                Console.WriteLine("X : " + nextMazeGridpoint.Position.X + ", Y : " + nextMazeGridpoint.Position.Y + " Direction : " + direction);
                RecursivelySearchForSolution(nextMazeGridpoint, nextMazeSolutionElement);
            }
        }

        // To find the path to the solution, simply unwind the solution element tree
        // from the finsh point to the start using recursion.
        private void ConstructPathToSolveMaze(MazeSolutionElementTree mazeSolutionElementTree)
        {
            if (mazeSolutionElementTree.ParentSolutionElement != null)
            {
                ConstructPathToSolveMaze(mazeSolutionElementTree.ParentSolutionElement);
            }

            var mazeSolutionElement = new MazeSolutionElement
            {
                MazeGridpoint = mazeSolutionElementTree.MazeGridpoint
            };

            PathToSolveMaze.Add(mazeSolutionElement);
            mazeSolutionElementTree.ParentSolutionElement = null;
        }
    }
}
