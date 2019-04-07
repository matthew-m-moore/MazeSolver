using System;
using System.Collections.Generic;
using System.Linq;
using MazeSolver.Common;
using MazeSolver.Core.MazeElements;

namespace MazeSolver.Core.DirectionPickerLogic
{
    /// <summary>
    /// Picks a direction that is wall-adjacent wherever possible.
    /// If this is not possible, tries to go in a direction consistent with the last direction chosen. 
    /// </summary>
    public class WallHuggingDirectionPicker : BaseDirectionPicker
    {
        public WallHuggingDirectionPicker(Maze mazeToSolve) : base(mazeToSolve) { }

        /// <summary>
        /// Picks a direction that is wall-adjacent wherever possible.
        /// If this is not possible, tries to go in a direction consistent with the last direction chosen. 
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

                // Find all paths that do not lead back to the last position in the maze on the path traced so far
                // AND, are valid positions inside the boundaries of the maze
                var potentialDirectionsToProceed = mazeGridpoint.DirectionsAvailable.OpenPaths
                    .Where(direction => 
                        MazeToSolve.IsValidPositionInMazeAndNotBacktracking(lastMazeGridpoint, mazeGridpoint, direction)).ToList();

                // Find the first direction available that is adjacent to a wall and pick it
                var directionToProceed = potentialDirectionsToProceed.FirstOrDefault(potentialDirection =>
                    IsWallAdjacent(mazeGridpoint, potentialDirection));

                // This conditional handles the case that no positions were wall-adjacent
                if (directionToProceed == DirectionEnum.None)
                {
                    // If it's possible to keep going in a straight line, continue to do so
                    var lastDirectionProceeded = lastMazeSolutionElement.DirectionProceeded;
                    if (potentialDirectionsToProceed.Contains(lastDirectionProceeded))
                    {
                        return lastDirectionProceeded;
                    }

                    return potentialDirectionsToProceed.FirstOrDefault();
                }

                return directionToProceed;
            }

            // Take the first path that is valid
            return mazeGridpoint.DirectionsAvailable.OpenPaths
                .FirstOrDefault(direction =>
                    MazeToSolve.IsValidPositionInMaze(mazeGridpoint, direction));
        }

        private bool IsWallAdjacent(MazeGridpoint mazeGridpoint, DirectionEnum direction)
        {
            // Start by proceeding in a direction given away from mazeGridpoint
            var isWallAdjacent = false;
            var nextMazeGridpoint = MazeToSolve.ProceedToNewGridpoint(mazeGridpoint, direction);

            // Now, check each direction around the nextMazeGridpoint
            foreach (var nextDirection in nextMazeGridpoint.DirectionsAvailable.OpenPaths)
            {
                if (!MazeToSolve.IsValidPositionInMazeAndNotBacktracking(mazeGridpoint, nextMazeGridpoint, nextDirection)) continue;

                var nextNextMazeGridpoint = MazeToSolve.ProceedToNewGridpoint(nextMazeGridpoint, nextDirection);

                if (nextNextMazeGridpoint.HasBeenVisited) continue;

                if (nextNextMazeGridpoint.IsWall)
                {
                    isWallAdjacent = true;
                    break;
                }
            }

            return isWallAdjacent;
        }
    }
}
