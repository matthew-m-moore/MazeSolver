using System.Linq;
using MazeSolver.Core.MazeElements;

namespace MazeSolver.Core.PreTreatments
{
    /// <summary>
    /// This logic finds any interior space in the maze that is adjacent to walls
    /// or other visited gridpoints, then determines if that space is surrounded by
    /// enough open space to make visiting the subject gridpoint unecessary.
    /// 
    /// For example, if the interior space of a maze is bounded by walls separated
    /// by five gridpoints, four of those five gridpoints may be unnecessary to
    /// explore while finding a solution.
    /// 
    /// Unfortunately this logic is very slow. So, if the maze is small or very dense 
    /// without a lot of excess interior space, it could make sense to run this logic.
    /// </summary>
    public class RemoveRedundantWhiteSpace : PreTreatmentLogic
    {
        private const int _maxiumGridpointsLimit = 5000;

        public RemoveRedundantWhiteSpace(Maze mazeToSolve) : base(mazeToSolve) { }

        /// <summary>
        /// Removes redundant interior space in a maze.
        /// </summary>
        public override void PreTreatMazeToSolve()
        {
            var wallMazeGridpoints = MazeToSolve.MazeGridpoints
                .Where(m => m.Value.IsWall)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            // One round of the outer-loop below takes about 170 ms in debug mode.
            // For 5,500 gridpoints, that's about 15 minutes.
            if (wallMazeGridpoints.Count() > _maxiumGridpointsLimit) return;

            // First, focus only on the gridpoints that are walls or have been visited
            foreach (var mazeGridpoint in wallMazeGridpoints)
            {
                // Now for each direction, find the wall-adjacent gridpoints that are not walls themselves
                foreach (var direction in mazeGridpoint.Value.DirectionsAvailable.OpenPaths)
                {
                    if (!MazeToSolve.IsValidPositionInMaze(mazeGridpoint.Value, direction)) continue;

                    var canAvoidThisGridpoint = true;
                    var wallAdjacentMazeGridpoint = MazeToSolve.ProceedToNewGridpoint(mazeGridpoint.Value, direction);

                    if (MazeToSolve.CheckIfAtDeadEnd(wallAdjacentMazeGridpoint)) continue;

                    // For each gridpoint that is wall-adjacent and not a wall itself, check the gridpoints
                    // at each of its four directions.
                    foreach (var nextDirection in wallAdjacentMazeGridpoint.DirectionsAvailable.OpenPaths)
                    {
                        if (!MazeToSolve.IsValidPositionInMaze(wallAdjacentMazeGridpoint, nextDirection)) continue;

                        var nextMazeGridpoint = MazeToSolve.ProceedToNewGridpoint(wallAdjacentMazeGridpoint, nextDirection);

                        if (MazeToSolve.CheckIfAtDeadEnd(nextMazeGridpoint))
                        {
                            wallAdjacentMazeGridpoint.DirectionsAvailable[nextDirection] = false;
                        }
                        else
                        {
                            // If not at a dead-end go out a second layer of gridpoints from the gridpoints that
                            // are around the wall-adjacent gridpoint
                            foreach (var nextNextDirection in nextMazeGridpoint.DirectionsAvailable.OpenPaths)
                            {
                                if (!MazeToSolve.IsValidPositionInMaze(nextMazeGridpoint, nextNextDirection)) continue;

                                var nextNextMazeGridpoint = MazeToSolve.ProceedToNewGridpoint(nextMazeGridpoint, nextNextDirection);

                                if (MazeToSolve.CheckIfAtDeadEnd(nextNextMazeGridpoint))
                                {
                                    nextMazeGridpoint.DirectionsAvailable[nextNextDirection] = false;
                                }
                            }

                            // If there's not at least two paths avaiable this far out, the wall-adjacent gridpoint
                            // likely cannot be avoided without closing off a potential solution path
                            if (nextMazeGridpoint.DirectionsAvailable.Count < 2)
                            {
                                canAvoidThisGridpoint = false;
                                break;
                            }
                        }
                    }

                    // Assuming each of the second layer gridpoints had at least two open paths, make sure
                    // that the wall-adjacent gridpoint has at least tree open paths
                    if (!canAvoidThisGridpoint ||
                        wallAdjacentMazeGridpoint.DirectionsAvailable.Count < 3) break;

                    // If everything above was satisfied, we can safely ignore the wall-adjacent gridpoint
                    // without fear of closing off a solution path
                    wallAdjacentMazeGridpoint.HasBeenVisited = true;
                    MazeToSolve.NotifyMazeHasBeenUpdated();
                    MazeToSolve.NotifyMazeToBeRedrawnUpdated();
                }
            }
        }
    }
}
