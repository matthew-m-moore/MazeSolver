using System.Collections.Generic;
using MazeSolver.Core.MazeElements;

namespace MazeSolver.Interfaces
{
    /// <summary>
    /// This interface must have a property of type Maze, a list of solution elements, 
    /// and a function that solves the maze.
    /// </summary>
    public interface IMazeSolverLogic
    {
        string Description { get; }
        Maze MazeToSolve { get; }
        List<MazeSolutionElement> PathToSolveMaze { get; }
        void SolveMaze();
    }
}
