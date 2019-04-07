using System.Drawing;
using IO.Adapters;
using MazeSolver.Core.MazeElements;

namespace IO.Utilities
{
    /// <summary>
    /// This is a utility class that can be used to read in a maze image to create a Maze object,
    /// where a file path to the image is specified.
    /// </summary>
    public class MazeReaderUtility
    {
        public string MazeImageFilePath { get; }

        public MazeReaderUtility(string filePath)
        {
            MazeImageFilePath = filePath;
        }

        public Maze ReadInMazeImage()
        {
            var useEmbeddedColorManagement = true;
            var mazeImage = Image.FromFile(MazeImageFilePath, useEmbeddedColorManagement);

            var maze = MazeConverter.ConvertImageToMaze(mazeImage);

            return maze;
        }
    }
}
