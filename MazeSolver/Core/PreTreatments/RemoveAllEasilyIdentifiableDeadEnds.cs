using System.Linq;
using MazeSolver.Core.MazeElements;

namespace MazeSolver.Core.PreTreatments
{
    /// <summary>
    /// Given that this program is omnicient toward the maze and its gridpoints, we can easily
    /// find any gridpoints where the number of open paths is equal to one. These gridpoints
    /// will not be necessary to explore, since they are obviously dead-ends.
    /// </summary>
    public class RemoveAllEasilyIdentifiableDeadEnds : PreTreatmentLogic
    {
        public RemoveAllEasilyIdentifiableDeadEnds(Maze mazeToSolve) : base(mazeToSolve) { }

        /// <summary>
        /// Sets all points in a maze that have only one open path to visited, effectively removing
        /// them from needing to be explored for a solution.
        /// </summary>
        public override void PreTreatMazeToSolve()
        {
            var deadEndMazeGridpoints = MazeToSolve.MazeGridpoints
                .Where(m => !m.Value.IsWall && !m.Value.HasBeenVisited && m.Value.DirectionsAvailable.Count == 1);

            foreach (var mazeGridpoint in deadEndMazeGridpoints)
            {
                if (mazeGridpoint.Value.IsStartPoint || mazeGridpoint.Value.IsFinishPoint) continue;

                mazeGridpoint.Value.HasBeenVisited = true;
                MazeToSolve.NotifyMazeHasBeenUpdated();
                MazeToSolve.NotifyMazeToBeRedrawnUpdated();
            }
        }
    }
}
