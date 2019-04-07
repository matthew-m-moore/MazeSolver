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
    /// that are consistent with the direction it was already going. Kind of like the
    /// game "Snake", this algorithm only picks a new direction if it has to.
    /// 
    /// Try the picking this logic for a comically slow solution
    /// </summary>
    public class BruteForceConstantDirectionMazeSolution : BruteForceMazeSolution
    {
        public override string Description => "Constant Direction Solver Logic";

        public BruteForceConstantDirectionMazeSolution(Maze mazeToSolve) : base(mazeToSolve)
        {
            DirectionPickerLogic = new StraightLineDirectionPicker(mazeToSolve);
        }
    }
}
