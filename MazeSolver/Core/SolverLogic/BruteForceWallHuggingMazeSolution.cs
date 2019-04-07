using MazeSolver.Core.DirectionPickerLogic;
using MazeSolver.Core.MazeElements;

namespace MazeSolver.Core.SolverLogic
{
    /// <summary>
    /// This logic simply picks a direction at each gridpoint to proceed until it runs
    /// into a dead-end or runs out of paths to take. In either case, the algorithm then
    /// backs up the last point where it had another direction to go and proceeds.
    /// 
    /// The direction-picking logic in this case prefers to pick directions to proceed
    /// that are wall-adjacent. As the name implies, it "hugs the wall" which will always
    /// lead to a solution. Apparently this can be proved use topology arugments.
    /// 
    /// This logic works really well for sparse mazes with lots of redundant interior
    /// space, but fails catastrophically for dense mazes.
    /// </summary>
    public class BruteForceWallHuggingMazeSolution : BruteForceMazeSolution
    {
        public override string Description => "Wall-Hugging Solver Logic";

        public BruteForceWallHuggingMazeSolution(Maze mazeToSolve) : base(mazeToSolve)
        {
            DirectionPickerLogic = new WallHuggingDirectionPicker(mazeToSolve);
        }
    }
}
