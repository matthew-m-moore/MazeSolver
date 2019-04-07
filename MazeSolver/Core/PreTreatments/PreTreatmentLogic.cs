using MazeSolver.Core.MazeElements;

namespace MazeSolver.Core.PreTreatments
{
    /// <summary>
    /// The idea behind this abstraction is that various types of logic can be used to
    /// alter a maze before any work is done in attempt to solve it. For example, all
    /// easily identifiable dead-ends could be filled in to avoid wasting time on exploring
    /// these routes when solving the maze.
    /// </summary>
    public abstract class PreTreatmentLogic
    {
        public Maze MazeToSolve { get; }

        public PreTreatmentLogic(Maze mazeToSolve)
        {
            MazeToSolve = mazeToSolve;
        }

        /// <summary>
        /// Apply work to alter a maze prior to solving it that will make it easier to solve.
        /// </summary>
        public abstract void PreTreatMazeToSolve();
    }
}
