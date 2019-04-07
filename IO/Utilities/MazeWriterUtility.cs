using System.IO;
using System.Drawing;
using IO.Adapters;
using MazeSolver.Core.MazeElements;

namespace IO.Utilities
{
    public class MazeWriterUtility
    {
        public string MazeImageFilePath { get; }
        public Image MazeImage =>
            MazeConverter.ConvertMazeToImage(Maze);
        public Maze Maze { get; set; }

        public MazeWriterUtility(string filePath, Maze maze)
        {
            MazeImageFilePath = filePath;
            Maze = maze;
        }

        public void SaveMazeImage(bool overwriteExistingFile = true)
        {
            if (File.Exists(MazeImageFilePath) && overwriteExistingFile)
            {
                File.Delete(MazeImageFilePath);
            }

            MazeImage.Save(MazeImageFilePath);
        }
    }
}
