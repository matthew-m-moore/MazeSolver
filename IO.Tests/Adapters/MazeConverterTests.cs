using System;
using System.Drawing;
using IO.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IO.Tests.Adapters
{
    [TestClass]
    public class MazeConverterTests
    {
        [TestMethod, Owner("Matthew Moore")]
        [ExpectedException(typeof(Exception))]
        public void ConvertImageToMaze_ColorsAreInvalid_ThrowsException()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SmileyFace.png";
            const bool useEmbeddedColorManagement = true;
            var mazeImage = Image.FromFile(mazeImageFilePath, useEmbeddedColorManagement);

            MazeConverter.ConvertImageToMaze(mazeImage);
        }

        [TestMethod, Owner("Matthew Moore")]
        [ExpectedException(typeof(Exception))]
        public void ConvertImageToMaze_ColorsAreUnsupportedShadeOfRed_ThrowsException()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SuperSimpleMazePinkStart.png";
            const bool useEmbeddedColorManagement = true;
            var mazeImage = Image.FromFile(mazeImageFilePath, useEmbeddedColorManagement);

            MazeConverter.ConvertImageToMaze(mazeImage);
        }

        [TestMethod, Owner("Matthew Moore")]
        [ExpectedException(typeof(Exception))]
        public void ConvertImageToMaze_ColorsAreUnsupportedShadeOfBlue_ThrowsException()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SuperSimpleMazePurpleEnd.png";
            const bool useEmbeddedColorManagement = true;
            var mazeImage = Image.FromFile(mazeImageFilePath, useEmbeddedColorManagement);

            MazeConverter.ConvertImageToMaze(mazeImage);
        }

        [TestMethod, Owner("Matthew Moore")]
        [ExpectedException(typeof(Exception))]
        public void ConvertImageToMaze_MazeHasNoStartingPoint_ThrowsException()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SuperSimpleMazeNoStart.png";
            const bool useEmbeddedColorManagement = true;
            var mazeImage = Image.FromFile(mazeImageFilePath, useEmbeddedColorManagement);

            MazeConverter.ConvertImageToMaze(mazeImage);
        }

        [TestMethod, Owner("Matthew Moore")]
        [ExpectedException(typeof(Exception))]
        public void ConvertImageToMaze_MazeHasNoFinishPoint_ThrowsException()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SuperSimpleMazeNoEnd.png";
            const bool useEmbeddedColorManagement = true;
            var mazeImage = Image.FromFile(mazeImageFilePath, useEmbeddedColorManagement);

            MazeConverter.ConvertImageToMaze(mazeImage);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void ConvertImageToMaze_SuperSimpleMaze_MazeGridpointsMatchHeightTimesWidth()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SuperSimpleMaze.png";
            const bool useEmbeddedColorManagement = true;
            var mazeImage = Image.FromFile(mazeImageFilePath, useEmbeddedColorManagement);

            var maze = MazeConverter.ConvertImageToMaze(mazeImage);

            var mazeHeight = mazeImage.Height;
            var mazeWidth = mazeImage.Width;

            Assert.AreEqual(maze.MazeGridpoints.Count, mazeHeight * mazeWidth);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void ConvertMazeToImage_SuperSimpleMaze_MazeGridpointsMatchHeightTimesWidth()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SuperSimpleMaze.png";
            const bool useEmbeddedColorManagement = true;
            var originalImage = Image.FromFile(mazeImageFilePath, useEmbeddedColorManagement);

            var maze = MazeConverter.ConvertImageToMaze(originalImage);
            var newImage = MazeConverter.ConvertMazeToImage(maze);

            var mazeHeight = newImage.Height;
            var mazeWidth = newImage.Width;

            Assert.AreEqual(maze.MazeGridpoints.Count, mazeHeight * mazeWidth);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void ConvertMazeToImage_SuperSimpleMazeRoundTrip_AllPixelColorsAndPositionsMatch()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SuperSimpleMaze.png";
            const bool useEmbeddedColorManagement = true;
            var originalImage = Image.FromFile(mazeImageFilePath, useEmbeddedColorManagement);

            var maze = MazeConverter.ConvertImageToMaze(originalImage);
            var newImage = MazeConverter.ConvertMazeToImage(maze);

            var mazeHeight = originalImage.Height;
            var mazeWidth = originalImage.Width;

            var originalBitmap = new Bitmap(originalImage);
            var newBitmap = new Bitmap(newImage);

            for (var x = 0; x < mazeWidth; x++)
            {
                for (var y = 0; y < mazeHeight; y++)
                {
                    var originalPixelColor = originalBitmap.GetPixel(x, y);
                    var newPixelColor = newBitmap.GetPixel(x, y);

                    Assert.AreEqual(originalPixelColor.ToArgb(), newPixelColor.ToArgb());
                }
            } 
        }
    }
}
