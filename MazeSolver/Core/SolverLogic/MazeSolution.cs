using System.Collections.Generic;
using MazeSolver.Core.MazeElements;
using MazeSolver.Core.PreTreatments;
using MazeSolver.Interfaces;

namespace MazeSolver.Core.SolverLogic
{
    public class MazeSolution : IMazeSolverLogic
    {
        public virtual string Description => "Empty Solver Logic";
        public Maze MazeToSolve { get; }
        public List<MazeSolutionElement> PathToSolveMaze { get; set; }
        public IDirectionPickerLogic DirectionPickerLogic { get; protected set; }
        public List<PreTreatmentLogic> PreTreatmentLogics { get; protected set; }

        public MazeSolution(Maze mazeToSolve)
        {
            MazeToSolve = mazeToSolve;
            PathToSolveMaze = new List<MazeSolutionElement>();
        }

        /// <summary>
        /// Solves the maze using the given implementation of IMazeSolverLogic
        /// </summary>
        public virtual void SolveMaze() { }

        /// <summary>
        /// Marks the solution path along a maze using the PathToSolveMaze property populated
        /// by the SolveMaze function call.
        /// </summary>
        public void MarkSolutionPath()
        {
            foreach (var mazeSolutionElement in PathToSolveMaze)
            {
                mazeSolutionElement.MazeGridpoint.IsOnSolutionPath = true;
            }
        }
    }
}
