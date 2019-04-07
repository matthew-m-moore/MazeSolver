using System;
using System.Collections.Generic;
using System.Linq;
using MazeSolver.Common;
using MazeSolver.Events;

namespace MazeSolver.Core.MazeElements
{
    public class Maze
    {
        public Dictionary<CartesianCoordinate, MazeGridpoint> MazeGridpoints { get; }

        private int? _mazeHeight;
        public int MazeHeight
        {
            get
            {
                if (_mazeHeight != null) return _mazeHeight.Value;

                _mazeHeight = MazeGridpoints.Keys.Max(position => position.Y) + 1;
                return _mazeHeight.Value;
            }
        }

        private int? _mazeWidth;
        public int MazeWidth
        {
            get
            {
                if (_mazeWidth != null) return _mazeWidth.Value;

                _mazeWidth = MazeGridpoints.Keys.Max(position => position.X) + 1;
                return _mazeWidth.Value;
            }
        }

        public Maze(Dictionary<CartesianCoordinate, MazeGridpoint> mazeGridpoints)
        {
            MazeGridpoints = mazeGridpoints;

            if (!MazeGridpoints.Values.Any(m => m.IsStartPoint))
            {
                throw new Exception("Invalid Maze: Sorry, this maze has no starting point marked.");
            }

            if (!MazeGridpoints.Values.Any(m => m.IsFinishPoint))
            {
                throw new Exception("Invalid Maze: Sorry, this maze has no finish point marked.");
            }
        }

        /// <summary>
        /// Checks if a maze gridpoint is a finish point of the maze.
        /// </summary>
        public bool CheckIfAtFinish(MazeGridpoint mazeGridpoint)
        {
            return mazeGridpoint.IsFinishPoint;
        }

        /// <summary>
        /// Checks if a maze gridpoint is a dead-end or wall.
        /// </summary>
        public bool CheckIfAtDeadEnd(MazeGridpoint mazeGridpoint)
        {
            if (mazeGridpoint.IsFinishPoint)
            {
                return false;
            }
            if (mazeGridpoint.IsStartPoint)
            {
                return false;
            }
            if (mazeGridpoint.IsWall)
            {
                return true;
            }
            if (mazeGridpoint.DirectionsAvailable.Count > 1)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// This method checks if the coordinate represents a valid point in the maze.
        /// </summary>
        public bool CheckIfCoordinateIsValid(CartesianCoordinate coordinate)
        {
            var isValid = coordinate.Y < MazeHeight
                       && coordinate.X < MazeWidth
                       && coordinate.Y >= 0
                       && coordinate.X >= 0;

            return isValid;
        }

        /// <summary>
        /// This method checks if moving to a new position in the maze will be inside
        /// the boundaries of the maze, and therfore valid.
        /// </summary>
        public bool IsValidPositionInMaze(
            MazeGridpoint currentMazeGridPoint,
            DirectionEnum directionToMove)
        {
            var nextPotentialCoordinate = currentMazeGridPoint.Position.MoveInDirection(directionToMove);

            var isValid = CheckIfCoordinateIsValid(nextPotentialCoordinate);

            return isValid;
        }

        /// <summary>
        /// This method checks two things: 1) If moving to a new position in the maze will be inside
        /// the boundaries of the maze, and therfore valid, 2) If moving to the specified position
        /// in the maze would be back-tracking based on the last position supplied.
        /// </summary>
        public bool IsValidPositionInMazeAndNotBacktracking(
            MazeGridpoint lastMazeGridPoint,
            MazeGridpoint currentMazeGridPoint,
            DirectionEnum directionToMove)
        {
            var nextPotentialCoordinate = currentMazeGridPoint.Position.MoveInDirection(directionToMove);

            var isBacktracking = (lastMazeGridPoint.Position == nextPotentialCoordinate);
            var isValid = CheckIfCoordinateIsValid(nextPotentialCoordinate);

            return (isValid && !isBacktracking);
        }

        /// <summary>
        /// This method moves to a new maze gridpoint indicated by the specified direction.
        /// If no maze gridpoint is found in the direction specified, an exception is thrown.
        /// </summary>
        public MazeGridpoint ProceedToNewGridpoint(MazeGridpoint mazeGridpoint, DirectionEnum direction)
        {
            var nextCoordinate = mazeGridpoint.Position.MoveInDirection(direction);

            var nextMazeGridpoint = MazeGridpoints[nextCoordinate];

            return nextMazeGridpoint;
        }

        /// <summary>
        /// An event that can be used to see if any maze gridpoints have been updated.
        /// </summary>
        public event EventHandler<MazeGridpointUpdatedEventArgs> MazeGridpointUpdated;

        /// <summary>
        /// A notification that can be used to call an event if any maze gridpoints have been updated.
        /// </summary>
        public void NotifyMazeHasBeenUpdated()
        {
            if (MazeGridpointUpdated != null)
            {
                var eventArgs = new MazeGridpointUpdatedEventArgs(this);

                MazeGridpointUpdated(new object(), eventArgs);
            }
        }

        /// <summary>
        /// An event that can be used to see if any maze gridpoints have been updated.
        /// </summary>
        public event EventHandler<MazeRedrawnEventArgs> MazeToBeRedrawn;

        /// <summary>
        /// A notification that can be used to call an event if any maze gridpoints have been updated.
        /// </summary>
        public void NotifyMazeToBeRedrawnUpdated()
        {
            if (MazeToBeRedrawn != null)
            {
                var eventArgs = new MazeRedrawnEventArgs(this);

                MazeToBeRedrawn(new object(), eventArgs);
            }
        }
    }
}
