using System;
using System.Collections.Generic;
using System.Linq;
using MazeSolver.Common;
using MazeSolver.Core.MazeElements;

namespace MazeSolver.Core.DirectionPickerLogic
{
    /// <summary>
    /// Picks a direction that is consitent with the last direction proceeded, if possible.
    /// </summary>
    public class StraightLineDirectionPicker : BaseDirectionPicker
    {
        public StraightLineDirectionPicker(Maze mazeToSolve) : base(mazeToSolve) { }

        /// <summary>
        /// Picks a direction that is consitent with the last direction proceeded, if possible.
        /// </summary>
        public override DirectionEnum PickDirectionToProceed(object mazeSolutionElements, MazeGridpoint mazeGridpoint)
        {
            var pathToSolveMaze = mazeSolutionElements as List<MazeSolutionElement>;

            if (pathToSolveMaze == null)
            {
                throw new Exception("Maze solution elements used are not supported by BaseDirectionPicker");
            }

            if (pathToSolveMaze.Any())
            {
                var lastMazeSolutionElement = pathToSolveMaze.Last();
                var lastMazeGridpoint = lastMazeSolutionElement.MazeGridpoint;
                var lastDirectionProceeded = lastMazeSolutionElement.DirectionProceeded;

                // Find all paths that do not lead back to the last position in the maze on the path traced so far
                // AND, are valid positions inside the boundaries of the maze
                var potentialDirectionsToProceed = mazeGridpoint.DirectionsAvailable.OpenPaths
                    .Where(direction => 
                        MazeToSolve.IsValidPositionInMazeAndNotBacktracking(lastMazeGridpoint, mazeGridpoint, direction)).ToList();

                // If it's possible to keep going in a straight line, continue to do so
                if (potentialDirectionsToProceed.Contains(lastDirectionProceeded))
                {
                    return lastDirectionProceeded;
                }

                return potentialDirectionsToProceed.FirstOrDefault();
            }

            // Take the first path that is valid
            return mazeGridpoint.DirectionsAvailable.OpenPaths
                .FirstOrDefault(direction =>
                    MazeToSolve.IsValidPositionInMaze(mazeGridpoint, direction));
        }
    }
}
