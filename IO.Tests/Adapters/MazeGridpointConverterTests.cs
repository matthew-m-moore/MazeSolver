using System;
using System.Drawing;
using IO.Adapters;
using MazeSolver.Common;
using MazeSolver.Core.MazeElements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IO.Tests.Adapters
{
    [TestClass]
    public class MazeGridpointConverterTests
    {
        [TestMethod, Owner("Matthew Moore")]
        [ExpectedException(typeof(Exception))]
        public void ConvertPixelInformationToMazeGridpoint_InvalidColor_ThrowsException()
        {
            var pixelColor = Color.Yellow;
            MazeGridpointConverter.ConvertPixelInformationToMazeGridpoint(0, 0, pixelColor);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void ConvertPixelInformationToMazeGridpoint_ColorIsRed_GridpointMarkedAsStart()
        {
            var pixelColor = Color.Red;
            var mazeGridpoint = MazeGridpointConverter.ConvertPixelInformationToMazeGridpoint(0, 0, pixelColor);
            Assert.IsTrue(mazeGridpoint.IsStartPoint);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void ConvertPixelInformationToMazeGridpoint_ColorIsDarkRed_GridpointMarkedAsStart()
        {
            var pixelColor = Color.DarkRed;
            var mazeGridpoint = MazeGridpointConverter.ConvertPixelInformationToMazeGridpoint(0, 0, pixelColor);
            Assert.IsTrue(mazeGridpoint.IsStartPoint);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void ConvertPixelInformationToMazeGridpoint_ColorIsBlue_GridpointMarkedAsFinish()
        {
            var pixelColor = Color.Blue;
            var mazeGridpoint = MazeGridpointConverter.ConvertPixelInformationToMazeGridpoint(0, 0, pixelColor);
            Assert.IsTrue(mazeGridpoint.IsFinishPoint);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void ConvertPixelInformationToMazeGridpoint_ColorIsVariousShadesOfBlue_GridpointMarkedAsFinish()
        {
            var pixelColor = Color.DarkBlue;
            var mazeGridpoint = MazeGridpointConverter.ConvertPixelInformationToMazeGridpoint(0, 0, pixelColor);
            Assert.IsTrue(mazeGridpoint.IsFinishPoint);

            pixelColor = Color.LightBlue;
            mazeGridpoint = MazeGridpointConverter.ConvertPixelInformationToMazeGridpoint(0, 0, pixelColor);
            Assert.IsTrue(mazeGridpoint.IsFinishPoint);

            pixelColor = Color.MidnightBlue;
            mazeGridpoint = MazeGridpointConverter.ConvertPixelInformationToMazeGridpoint(0, 0, pixelColor);
            Assert.IsTrue(mazeGridpoint.IsFinishPoint);

            pixelColor = Color.CornflowerBlue;
            mazeGridpoint = MazeGridpointConverter.ConvertPixelInformationToMazeGridpoint(0, 0, pixelColor);
            Assert.IsTrue(mazeGridpoint.IsFinishPoint);

            pixelColor = Color.DodgerBlue;
            mazeGridpoint = MazeGridpointConverter.ConvertPixelInformationToMazeGridpoint(0, 0, pixelColor);
            Assert.IsTrue(mazeGridpoint.IsFinishPoint);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void ConvertPixelInformationToMazeGridpoint_ColorIsBlack_GridpointMarkedAsWall()
        {
            var pixelColor = Color.Black;
            var mazeGridpoint = MazeGridpointConverter.ConvertPixelInformationToMazeGridpoint(0, 0, pixelColor);
            Assert.IsTrue(mazeGridpoint.IsWall);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void ConvertMazeGridpointToPixelColor_GridpointIsWall_ColorIsBlack()
        {
            var coordinate = new CartesianCoordinate(0, 0);
            var directions = new DirectionsAvailable();
            var mazeGridpoint = new MazeGridpoint(coordinate, directions, false, false, true);

            var pixelColor = MazeGridpointConverter.ConvertMazeGridpointToPixelColor(mazeGridpoint);
            Assert.AreEqual(pixelColor.ToArgb(), Color.Black.ToArgb());
        }

        [TestMethod, Owner("Matthew Moore")]
        public void ConvertMazeGridpointToPixelColor_GridpointIsStart_ColorIsRed()
        {
            var coordinate = new CartesianCoordinate(0, 0);
            var directions = new DirectionsAvailable();
            var mazeGridpoint = new MazeGridpoint(coordinate, directions, true, false, false);

            mazeGridpoint.HasBeenVisited = false;
            var pixelColor = MazeGridpointConverter.ConvertMazeGridpointToPixelColor(mazeGridpoint);
            Assert.AreEqual(pixelColor.ToArgb(), Color.Red.ToArgb());

            mazeGridpoint.HasBeenVisited = true;
            pixelColor = MazeGridpointConverter.ConvertMazeGridpointToPixelColor(mazeGridpoint);
            Assert.AreEqual(pixelColor.ToArgb(), Color.Red.ToArgb());
        }

        [TestMethod, Owner("Matthew Moore")]
        public void ConvertMazeGridpointToPixelColor_GridpointIsFinish_ColorIsBlue()
        {
            var coordinate = new CartesianCoordinate(0, 0);
            var directions = new DirectionsAvailable();
            var mazeGridpoint = new MazeGridpoint(coordinate, directions, false, true, false);

            mazeGridpoint.HasBeenVisited = false;
            var pixelColor = MazeGridpointConverter.ConvertMazeGridpointToPixelColor(mazeGridpoint);
            Assert.AreEqual(pixelColor.ToArgb(), Color.Blue.ToArgb());

            mazeGridpoint.HasBeenVisited = true;
            pixelColor = MazeGridpointConverter.ConvertMazeGridpointToPixelColor(mazeGridpoint);
            Assert.AreEqual(pixelColor.ToArgb(), Color.Blue.ToArgb());
        }

        [TestMethod, Owner("Matthew Moore")]
        public void ConvertMazeGridpointToPixelColor_GridpointOnSolutionPath_ColorIsGreen()
        {
            var coordinate = new CartesianCoordinate(0, 0);
            var directions = new DirectionsAvailable();
            var mazeGridpoint = new MazeGridpoint(coordinate, directions, false, false, false);

            mazeGridpoint.HasBeenVisited = true;
            mazeGridpoint.IsOnSolutionPath = true;
            var pixelColor = MazeGridpointConverter.ConvertMazeGridpointToPixelColor(mazeGridpoint);
            Assert.AreEqual(pixelColor.ToArgb(), Color.Green.ToArgb());
        }

        [TestMethod, Owner("Matthew Moore")]
        public void ConvertMazeGridpointToPixelColor_GridpointOHasBeenVisited_ColorIsGray()
        {
            var coordinate = new CartesianCoordinate(0, 0);
            var directions = new DirectionsAvailable();
            var mazeGridpoint = new MazeGridpoint(coordinate, directions, false, false, false);

            mazeGridpoint.HasBeenVisited = true;
            mazeGridpoint.IsOnSolutionPath = false;
            var pixelColor = MazeGridpointConverter.ConvertMazeGridpointToPixelColor(mazeGridpoint);
            Assert.AreEqual(pixelColor.ToArgb(), Color.Gray.ToArgb());
        }
    }
}
