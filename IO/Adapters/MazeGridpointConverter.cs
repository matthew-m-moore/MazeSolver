using System;
using System.Drawing;
using MazeSolver.Common;
using MazeSolver.Core.MazeElements;

namespace IO.Adapters
{
    /// <summary>
    /// An adapter that can be used to convert pixels to and from MazeGridpoint objects.
    /// </summary>
    public class MazeGridpointConverter
    {
        // The rule for the problem state that start will be Red, end will be Blue, all walls will be Black.
        // Futhermore, this code is assuming that the interior of the maze will be white only.
        // If any of those rules are violated, we can catch them here.
        // Alternatively, if the rules are changed it then this the only function that needs to be altered.

        /// <summary>
        /// Converts a pixel position of an image and it's color into  MazeGridpoint. Shades of red are interpreted
        /// as starting points. Shades of blue are interpreted as ending points.
        /// </summary>
        public static MazeGridpoint ConvertPixelInformationToMazeGridpoint(int x, int y, Color mazeImagePixelColor)
        {
            var position = new CartesianCoordinate(x, y);
            var directionsAvailable = new DirectionsAvailable();

            // One could also envision an interchanged Start/End/Wall determination logic here as an interface,
            // but since the rules are clearly stated, that might be overkill. Also, the class would need to
            // be non-static in order to pull that off.
            bool isRed; bool isBlue; bool isBlack;
            ValidatePixelColor(mazeImagePixelColor, out isRed, out isBlue, out isBlack);
            var isStartPoint = IsColorRed(mazeImagePixelColor);
            var isEndPoint = IsColorBlue(mazeImagePixelColor);
            var isWall = IsColorBlack(mazeImagePixelColor);

            return new MazeGridpoint(position, directionsAvailable, isStartPoint, isEndPoint, isWall);
        }

        private static void ValidatePixelColor(Color pixelColor, out bool isRed, out bool isBlue, out bool isBlack)
        {
            isRed = IsColorRed(pixelColor);
            isBlue = IsColorBlue(pixelColor);
            isBlack = IsColorBlack(pixelColor);

            var isWhite = IsColorWhite(pixelColor);

            if (!isRed && !isBlue && !isBlack && !isWhite)
            {
                throw new Exception(
                    "Maze Image Pixel Color Violation: Sorry, maze image contains a color that is not Red (Start), Blue (End), Black (Walls), or White (Interior).");
            }
        }

        /// <summary>
        /// A method that converts a MazeGridpoint object into a pixel, with colors corresponding as follows:
        /// Red (Start), Blue (Finish), Gray (Interior, Explored During Solution), Green (Solution), 
        /// White (Interior, Never Visited), and Black (Walls).
        /// 
        /// This method may not preserve the original colors of the input matrix, for example 
        /// if the shades of Red/Blue differ from cannoical Red/Blue.
        /// </summary>
        public static Color ConvertMazeGridpointToPixelColor(MazeGridpoint mazeGridpoint)
        {
            if (mazeGridpoint.IsStartPoint)
            {
                return Color.Red;
            }
            if (mazeGridpoint.IsFinishPoint)
            {
                return Color.Blue;
            }
            if (mazeGridpoint.IsWall)
            {
                return Color.Black;
            }
            if (mazeGridpoint.IsOnSolutionPath)
            {
                return Color.Green;
            }
            if (mazeGridpoint.HasBeenVisited)
            {
                return Color.Gray;
            }

            return Color.White;
        }

        // Admittedly everything after "DarkRed" is a bit of a stretch.
        private static bool IsColorRed(Color pixelColor)
        {
            var argbColor = pixelColor.ToArgb();

            if (argbColor == Color.Red.ToArgb()) return true;
            if (argbColor == Color.DarkRed.ToArgb()) return true;

            if (argbColor == Color.IndianRed.ToArgb()) return true;
            if (argbColor == Color.OrangeRed.ToArgb()) return true;
            if (argbColor == Color.PaleVioletRed.ToArgb()) return true;
            if (argbColor == Color.MediumVioletRed.ToArgb()) return true;

            return false;
        }

        // So many shades of blue... Good thing this is my son's favorite color : )
        private static bool IsColorBlue(Color pixelColor)
        {
            var argbColor = pixelColor.ToArgb();

            if (argbColor == Color.Blue.ToArgb()) return true;
            if (argbColor == Color.DarkBlue.ToArgb()) return true;
            if (argbColor == Color.DarkSlateBlue.ToArgb()) return true;
            if (argbColor == Color.SlateBlue.ToArgb()) return true;

            if (argbColor == Color.SkyBlue.ToArgb()) return true;
            if (argbColor == Color.LightBlue.ToArgb()) return true;
            if (argbColor == Color.LightSkyBlue.ToArgb()) return true;
            if (argbColor == Color.LightSteelBlue.ToArgb()) return true;
            if (argbColor == Color.DeepSkyBlue.ToArgb()) return true;

            if (argbColor == Color.CadetBlue.ToArgb()) return true;
            if (argbColor == Color.CornflowerBlue.ToArgb()) return true;
            if (argbColor == Color.DodgerBlue.ToArgb()) return true;
            if (argbColor == Color.PowderBlue.ToArgb()) return true;
            if (argbColor == Color.RoyalBlue.ToArgb()) return true;

            if (argbColor == Color.MediumBlue.ToArgb()) return true;
            if (argbColor == Color.MediumSlateBlue.ToArgb()) return true;
            if (argbColor == Color.MidnightBlue.ToArgb()) return true;
            if (argbColor == Color.SteelBlue.ToArgb()) return true;

            return false;
        }

        // Let's not get into shades of gray here. Pun definitely intended. ; )
        private static bool IsColorBlack(Color pixelColor)
        {
            var argbColor = pixelColor.ToArgb();

            if (argbColor == Color.Black.ToArgb()) return true;

            return false;
        }

        // Again, the assumption here is that the interior of the maze is always white.
        private static bool IsColorWhite(Color pixelColor)
        {
            var argbColor = pixelColor.ToArgb();

            if (argbColor == Color.White.ToArgb()) return true;

            return false;
        }

        // Another option for above would be to store the list of acceptable shades of each color in a database table.
    }
}
