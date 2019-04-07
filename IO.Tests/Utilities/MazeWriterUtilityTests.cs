using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using IO.Adapters;
using IO.Utilities;
using MazeSolver.Common;
using MazeSolver.Core.MazeElements;
using MazeSolver.Core.SolverLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IO.Tests.Utilities
{
    [TestClass]
    public class MazeWriterUtilityTests
    {
        [TestMethod, Owner("Matthew Moore")]
        public void ConvertMazeToImage_SuperSimpleMaze_MazeGridpointsMatchHeightTimesWidth()
        {
            const string mazeImageFilePath = @"..\..\..\TestingMazes\SuperSimpleMaze.png";
            const bool useEmbeddedColorManagement = true;
            var originalImage = Image.FromFile(mazeImageFilePath, useEmbeddedColorManagement);

            var maze = MazeConverter.ConvertImageToMaze(originalImage);

            const string mazeImageFileSavePath = @"..\..\..\TestingMazes\SuperSimpleMazeTest1.png";
            var mazeWriter = new MazeWriterUtility(mazeImageFileSavePath, maze);
            mazeWriter.SaveMazeImage();

            var newImage = Image.FromFile(mazeImageFileSavePath, useEmbeddedColorManagement);

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

            const string mazeImageFileSavePath = @"..\..\..\TestingMazes\SuperSimpleMazeTest2.png";
            var mazeWriter = new MazeWriterUtility(mazeImageFileSavePath, maze);
            mazeWriter.SaveMazeImage();

            var newImage = Image.FromFile(mazeImageFileSavePath, useEmbeddedColorManagement);

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

        // This test was used for tracking the progress of some solver algorithms running in release mode
        public void ConvertTextOutputToSolutionPathOnMazeImage()
        {
            const string mazeImageFilePath = @"..\..\..\SampleMazes\Maze2.png";
            const string mazeSolutionTextFilePath = @"..\..\..\SampleMazesSolutions\SampleMaze2\WallHugging.txt";
            const string mazeImageSolutionFilePath = @"..\..\..\SampleMazesSolutions\SampleMaze2\Maze2SolutionProgress.png";

            var mazeReader = new MazeReaderUtility(mazeImageFilePath);
            var maze = mazeReader.ReadInMazeImage();

            var fileLines = File.ReadAllLines(mazeSolutionTextFilePath);
            var count = fileLines.Count();

            var mazeSolutionPath = new List<MazeSolutionElement>();
            for (var i = 0; i < count; i++)
            {
                var line = fileLines[i];
                var splitLine = line.Split(' ', ',');
                var xPosition = Convert.ToInt32(splitLine[2]);
                var yPosition = Convert.ToInt32(splitLine[6]);

                var coordinate = new CartesianCoordinate(xPosition, yPosition);
                var mazeGridpoint = maze.MazeGridpoints[coordinate];

                var solutionElement = new MazeSolutionElement
                {
                    MazeGridpoint = mazeGridpoint,
                };

                mazeSolutionPath.Add(solutionElement);

                fileLines[i] = null;
            }

            var mazeSolution = new MazeSolution(maze)
            {
                PathToSolveMaze = mazeSolutionPath
            };
            
            var mazeWriter = new MazeWriterUtility(mazeImageSolutionFilePath, mazeSolution.MazeToSolve);
            mazeSolution.MarkSolutionPath();
            mazeWriter.SaveMazeImage();
        }
    }
}
