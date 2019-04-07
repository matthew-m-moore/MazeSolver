using IO.Utilities;
using MazeSolver.Core.SolverLogic;
using MazeSolver.Events;

namespace Maze.Listeners
{
    public class MazeUpdatedListener
    {
        private MazeWriterUtility _mazeWriter;

        public void Subscribe(MazeSolution mazeSolution, MazeWriterUtility mazeWriter)
        {
            _mazeWriter = mazeWriter;
            mazeSolution.MazeToSolve.MazeGridpointUpdated += OnMazeUpdated;
        }

        public void UnSubscribe(MazeSolution mazeSolution)
        {
            mazeSolution.MazeToSolve.MazeGridpointUpdated -= OnMazeUpdated;
        }

        public void OnMazeUpdated(object sender, MazeGridpointUpdatedEventArgs e)
        {
            _mazeWriter.Maze = e.UpdatedMaze;
            _mazeWriter.SaveMazeImage();
        }
    }
}
