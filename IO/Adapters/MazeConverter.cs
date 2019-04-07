using System.Collections.Generic;
using System.Drawing;
using MazeSolver.Common;
using MazeSolver.Core.MazeElements;

namespace IO.Adapters
{
    /// <summary>
    /// An adapter that can be used to and image (*.bmp, *.jpg, *.png) to and from a Maze object.
    /// </summary>
    public class MazeConverter
    {
        /// <summary>
        /// Converts an Image object into a Maze object. The Image object is used to create a Bitmap representation.
        /// </summary>
        public static Maze ConvertImageToMaze(Image mazeImage)
        {
            var mazeBitmapImage = new Bitmap(mazeImage);
            var mazeGridpoints = new Dictionary<CartesianCoordinate, MazeGridpoint>();

            for (var x = 0; x < mazeBitmapImage.Width; x++)
            {
                for (var y = 0; y < mazeBitmapImage.Height; y++)
                {
                    var mazeBitmapPixelColor = mazeBitmapImage.GetPixel(x, y);
                    var mazeGridpoint = MazeGridpointConverter.ConvertPixelInformationToMazeGridpoint(x, y, mazeBitmapPixelColor);

                    mazeGridpoints.Add(mazeGridpoint.Position, mazeGridpoint);
                }
            }

            return new Maze(mazeGridpoints);
        }

        /// <summary>
        /// Converts a Maze object into an Image object. A Bitmap representation is used to create the Image object.
        /// </summary>
        public static Image ConvertMazeToImage(Maze maze)
        {
            var imageWidth = maze.MazeWidth;
            var imageHeight = maze.MazeHeight;
            var mazeBitmapImage = new Bitmap(imageWidth, imageHeight);

            foreach (var mazeGridpoint in maze.MazeGridpoints)
            {
                var x = mazeGridpoint.Key.X;
                var y = mazeGridpoint.Key.Y;

                var pixelColor = MazeGridpointConverter.ConvertMazeGridpointToPixelColor(mazeGridpoint.Value);

                mazeBitmapImage.SetPixel(x, y, pixelColor);
            }

            var mazeImage = mazeBitmapImage as Image;
            return mazeImage;
        }
    }
}
