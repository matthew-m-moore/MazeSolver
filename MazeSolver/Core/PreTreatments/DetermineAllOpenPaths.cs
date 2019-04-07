using System.Collections.Generic;
using System.Linq;
using MazeSolver.Common;
using MazeSolver.Core.MazeElements;

namespace MazeSolver.Core.PreTreatments
{
    /// <summary>
    /// Determining the number of open paths at each gridpoint prior to attempting a solution
    /// can cut down the erroneous paths taken.
    /// </summary>
    public class DetermineAllOpenPaths : PreTreatmentLogic
    {
        private const int _maxiumGridpointsLimit = 2000;

        public DetermineAllOpenPaths(Maze mazeToSolve) : base(mazeToSolve) { }

        /// <summary>
        /// Determines the correct number of open paths around all gridpoints in a maze.
        /// </summary>
        public override void PreTreatMazeToSolve()
        {
            // One round of the outer-loop below takes about 40 ms in debug mode.
            // For 2000 gridpoints, that's about 2 minutes.
            if (MazeToSolve.MazeGridpoints.Count() > _maxiumGridpointsLimit) return;

            MarkClosedPaths(MazeToSolve.MazeGridpoints);
        }

        protected void MarkClosedPaths(Dictionary<CartesianCoordinate, MazeGridpoint> mazeGridpoints)
        {
            foreach (var mazeGridpoint in mazeGridpoints.Values)
            {
                // For each available direction, check what paths are closed and mark them
                foreach (var direction in mazeGridpoint.DirectionsAvailable.OpenPaths)
                {
                    if (MazeToSolve.IsValidPositionInMaze(mazeGridpoint, direction))
                    {
                        var nextMazeGridpoint = MazeToSolve.ProceedToNewGridpoint(mazeGridpoint, direction);

                        if (MazeToSolve.CheckIfAtDeadEnd(nextMazeGridpoint)
                            || nextMazeGridpoint.HasBeenVisited)
                        {
                            mazeGridpoint.DirectionsAvailable[direction] = false;
                        }
                    }
                }
            }
        }
    }
}
