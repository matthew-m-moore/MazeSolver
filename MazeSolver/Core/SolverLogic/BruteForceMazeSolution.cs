using System;
using System.Collections.Generic;
using System.Linq;
using MazeSolver.Common;
using MazeSolver.Core.DirectionPickerLogic;
using MazeSolver.Core.MazeElements;
using MazeSolver.Core.PreTreatments;

namespace MazeSolver.Core.SolverLogic
{
    /// <summary>
    /// This logic simply picks a direction at each gridpoint to proceed until it runs
    /// into a dead-end or runs out of paths to take. In either case, the algorithm then
    /// backs up the last point where it had another direction to go and proceeds.
    /// 
    /// The direction-picking logic in this case always prefers to pick directions to proceed
    /// in the order: Up, Down, Left, Right.
    /// </summary>
    public class BruteForceMazeSolution : MazeSolution
    {
        public override string Description => "Brute-Force Solver Logic";

        public BruteForceMazeSolution(Maze mazeToSolve) : base(mazeToSolve)
        {
            DirectionPickerLogic = new BaseDirectionPicker(mazeToSolve);
            PreTreatmentLogics = new List<PreTreatmentLogic>
            {
                new DetermineAllOpenPathsAtStartPoint(mazeToSolve)
            };
        }

        /// <summary>
        /// Solves the maze using a brute force algorithm approach.
        /// </summary>
        public override void SolveMaze()
        {
            // Apply all pre-treament logics, if any
            PreTreatmentLogics.ForEach(p => p.PreTreatMazeToSolve());

            // Set all parent gridpoints to null at the start
            var currentMazeGridpoint = MazeToSolve.MazeGridpoints.Values.First(m => m.IsStartPoint && m.DirectionsAvailable.Count > 0);

            // Keep on looking so long as we haven't reached the finish
            while (!MazeToSolve.CheckIfAtFinish(currentMazeGridpoint))
            {
                currentMazeGridpoint.HasBeenVisited = true;

                // Picked a direction and go in that direction
                var directionToProceed = DirectionPickerLogic.PickDirectionToProceed(PathToSolveMaze, currentMazeGridpoint);

                if (directionToProceed == DirectionEnum.None)
                {
                    throw new Exception("Maze Solver Error: Sorry, maze could not be solved. The maze image provided may be invalid.");
                }

                currentMazeGridpoint = DetermineNextMazeGridPoint(currentMazeGridpoint, directionToProceed);

                Console.WriteLine("X : " + currentMazeGridpoint.Position.X + ", Y : " + currentMazeGridpoint.Position.Y + " Direction : " + directionToProceed);
                MazeToSolve.NotifyMazeHasBeenUpdated();
                MazeToSolve.NotifyMazeToBeRedrawnUpdated();
            }

            // Once out of the loop, the current gridpoint will be the final gridpoint
            var finalMazeSolutionElement = new MazeSolutionElement
            {
                StepNumber = PathToSolveMaze.Count,            
                MazeGridpoint = currentMazeGridpoint,
                DirectionProceeded = DirectionEnum.None
            };

            PathToSolveMaze.Add(finalMazeSolutionElement);
        }

        /// <summary>
        /// Given a direction to proceed, this logic determines what the next maze gridpoint will be. In cases
        /// where we have reached a dead-end, this may mean backtracking to the last gridpoint where another
        /// choice was available.
        /// </summary>
        private MazeGridpoint DetermineNextMazeGridPoint(MazeGridpoint currentMazeGridpoint, DirectionEnum directionToProceed)
        {
            var nextMazeGridpoint = MazeToSolve.ProceedToNewGridpoint(currentMazeGridpoint, directionToProceed);

            if (MazeToSolve.CheckIfAtDeadEnd(nextMazeGridpoint) || nextMazeGridpoint.HasBeenVisited)
            {
                // We have arrived at a dead-end, so this cannot have been the right direction to proceed
                currentMazeGridpoint.DirectionsAvailable[directionToProceed] = false;

                if (currentMazeGridpoint.DirectionsAvailable.Count == 1)
                {
                    // Go back to the last gridpoint where there were more than two choices about the direction to proceed
                    var lastValidMazeSolutionElement = PathToSolveMaze.LastOrDefault(p => p.MazeGridpoint.DirectionsAvailable.Count > 2);
                    if (lastValidMazeSolutionElement == null)
                    {
                        throw new Exception("Maze Solver Error: Sorry, maze could not be solved. The maze image provided may be invalid.");
                    }

                    var directionProceededAtLastValidMazeGridPoint = lastValidMazeSolutionElement.DirectionProceeded;

                    // Since the path resulted in a dead-end, it cannot have been the reight direction to proceed
                    lastValidMazeSolutionElement.MazeGridpoint.DirectionsAvailable[directionProceededAtLastValidMazeGridPoint] = false;

                    // Truncate the path back to the last valid point to take a new direction
                    PathToSolveMaze = PathToSolveMaze.Where(p => p.StepNumber < lastValidMazeSolutionElement.StepNumber).ToList();

                    return lastValidMazeSolutionElement.MazeGridpoint;
                }

                return currentMazeGridpoint;
            }

            // The step numbers will be indexed starting with zero
            var stepNumber = PathToSolveMaze.Count;
            var mazeSolutionElement = new MazeSolutionElement
            {
                StepNumber = stepNumber,
                MazeGridpoint = currentMazeGridpoint,
                DirectionProceeded = directionToProceed
            };

            PathToSolveMaze.Add(mazeSolutionElement);

            return nextMazeGridpoint;
        }
    }
}
