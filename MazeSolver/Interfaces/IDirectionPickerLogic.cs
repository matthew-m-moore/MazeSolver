using MazeSolver.Common;
using MazeSolver.Core.MazeElements;

namespace MazeSolver.Interfaces
{
    /// <summary>
    /// This interface has to have a function that picks a new direction in which to proceed.
    /// Depenending on the solver logic used, the object representing the maze solution 
    /// elements could be different.
    /// </summary>
    public interface IDirectionPickerLogic
    {
        Maze MazeToSolve { get; }
        DirectionEnum PickDirectionToProceed(object mazeSolutionElements, MazeGridpoint mazeGridpoint);
    }
}
