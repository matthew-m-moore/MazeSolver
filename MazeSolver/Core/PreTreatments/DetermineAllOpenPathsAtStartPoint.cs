using System.Linq;
using MazeSolver.Core.MazeElements;

namespace MazeSolver.Core.PreTreatments
{
    /// <summary>
    /// Determining the number of open paths at each gridpoint prior to attempting a solution
    /// can cut down the erroneous paths taken. In this case, a limited set of gridpoints can
    /// be investigated such that the solver logic can start at the edge of a large block of
    /// area considered to be the starging point.
    /// </summary>
    public class DetermineAllOpenPathsAtStartPoint : DetermineAllOpenPaths
    {
        private const int _maxiumGridpointsLimit = 2000;

        public DetermineAllOpenPathsAtStartPoint(Maze mazeToSolve) : base(mazeToSolve) { }

        /// <summary>
        /// Determines the correct number of open paths arouund all gridpoints labeled as starting points.
        /// </summary>
        public override void PreTreatMazeToSolve()
        {
            var startingMazeGridpoints = MazeToSolve.MazeGridpoints
                .Where(m => m.Value.IsStartPoint)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            if (startingMazeGridpoints.Count() > _maxiumGridpointsLimit) return;

            MarkClosedPaths(startingMazeGridpoints);
        }
    }
}
