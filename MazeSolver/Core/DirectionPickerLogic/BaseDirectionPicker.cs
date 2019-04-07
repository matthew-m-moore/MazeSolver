using System;
using System.Collections.Generic;
using System.Linq;
using MazeSolver.Common;
using MazeSolver.Core.MazeElements;
using MazeSolver.Interfaces;

namespace MazeSolver.Core.DirectionPickerLogic
{
    /// <summary>
    /// Picks a direction in this order of preference: Up, Down, Left, Right.
    /// </summary>
    public class BaseDirectionPicker : IDirectionPickerLogic
    {
        public Maze MazeToSolve { get; }

        public BaseDirectionPicker(Maze mazeToSolve)
        {
            MazeToSolve = mazeToSolve;
        }

        /// <summary>
        /// Picks a direction in this order of preference: Up, Down, Left, Right.
        /// </summary>
        public virtual DirectionEnum PickDirectionToProceed(object mazeSolutionElements, MazeGridpoint mazeGridpoint)
        {
            var pathToSolveMaze = mazeSolutionElements as List<MazeSolutionElement>;

            if (pathToSolveMaze == null)
            {
                throw new Exception("Maze solution elements used are not supported by BaseDirectionPicker");
            }

            // There cannot be any back-tracking if there is nothing in the path to solve the maze
            if (pathToSolveMaze.Any())
            {
                var lastMazeGridpoint = pathToSolveMaze.Last().MazeGridpoint;

                // Take the first path that does not lead back to the last position in the maze on the path traced so far
                // AND, is a valid position inside the boundaries of the maze
                var directionToProceed = mazeGridpoint.DirectionsAvailable.OpenPaths
                    .FirstOrDefault(direction => MazeToSolve.IsValidPositionInMazeAndNotBacktracking(lastMazeGridpoint, mazeGridpoint, direction));

                return directionToProceed;
            }

            // Take the first path that is valid
            return mazeGridpoint.DirectionsAvailable.OpenPaths
                .FirstOrDefault(direction =>
                    MazeToSolve.IsValidPositionInMaze(mazeGridpoint, direction));
        }
    }
}
