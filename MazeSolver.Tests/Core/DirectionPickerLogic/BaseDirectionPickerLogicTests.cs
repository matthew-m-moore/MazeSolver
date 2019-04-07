using System;
using System.Collections.Generic;
using System.Linq;
using MazeSolver.Core.MazeElements;
using MazeSolver.Common;
using MazeSolver.Core.DirectionPickerLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeSolver.Tests.Core.DirectionPickerLogic
{
    [TestClass]
    public class BaseDirectionPickerLogicTests
    {
        [TestMethod, Owner("Matthew Moore")]
        [ExpectedException(typeof(Exception))]
        public void PickDirectionToProceed_PassIncorrectTypeOfSolutionElements_ThrowsException()
        {
            var currentMazeGridpoint =
                new MazeGridpoint(new CartesianCoordinate(  2,   2), new DirectionsAvailable(), false, false, false);

            var listOfMazeGridpoints = new List<MazeGridpoint>
            {
                currentMazeGridpoint,
                new MazeGridpoint(new CartesianCoordinate(  2 ,  0), new DirectionsAvailable(),  true, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  4), new DirectionsAvailable(), false,  true, false)
            };

            var mazeToTest = new Maze(listOfMazeGridpoints.ToDictionary(m => m.Position, m => m));
            var directionPicker = new BaseDirectionPicker(mazeToTest);
            var mazeSolution = new MazeSolutionElementTree();

            directionPicker.PickDirectionToProceed(mazeSolution, currentMazeGridpoint);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void PickDirectionToProceed_NoElementsInSolutionPath_PicksDirectionUp()
        {
            var currentMazeGridpoint =
                new MazeGridpoint(new CartesianCoordinate(  2,   2), new DirectionsAvailable(), false, false, false);

            var listOfMazeGridpoints = new List<MazeGridpoint>
            {
                new MazeGridpoint(new CartesianCoordinate(  2 ,  0), new DirectionsAvailable(),  true, false, false),
                new MazeGridpoint(new CartesianCoordinate(  0,   2), new DirectionsAvailable(), false, false,  true),

                new MazeGridpoint(new CartesianCoordinate(  1 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  3 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  1 ,  2), new DirectionsAvailable(), false, false, false),

                currentMazeGridpoint,

                new MazeGridpoint(new CartesianCoordinate(  3 ,  2), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  1 ,  3), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  3), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  3 ,  3), new DirectionsAvailable(), false, false, false),

                new MazeGridpoint(new CartesianCoordinate(  4 ,  2), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  4), new DirectionsAvailable(), false,  true, false)
            };

            var mazeToTest = new Maze(listOfMazeGridpoints.ToDictionary(m => m.Position, m => m));
            var directionPicker = new BaseDirectionPicker(mazeToTest);
            var mazeSolution = new List<MazeSolutionElement>();

            var directionToProceed = directionPicker.PickDirectionToProceed(mazeSolution, currentMazeGridpoint);

            Assert.AreEqual(directionToProceed, DirectionEnum.Up);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void PickDirectionToProceed_NoElementsInSolutionPathAndDirectionUpIsOutsideBoundariesOfMaze_PicksDirectionDown()
        {
            var currentMazeGridpoint =
                new MazeGridpoint(new CartesianCoordinate(  2,  0), new DirectionsAvailable(), false, false, false);

            var listOfMazeGridpoints = new List<MazeGridpoint>
            {
                new MazeGridpoint(new CartesianCoordinate(  2 ,  0), new DirectionsAvailable(),  true, false, false),
                new MazeGridpoint(new CartesianCoordinate(  0,   2), new DirectionsAvailable(), false, false,  true),

                new MazeGridpoint(new CartesianCoordinate(  1 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  3 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  1 ,  2), new DirectionsAvailable(), false, false, false),

                new MazeGridpoint(new CartesianCoordinate(  2,   2), new DirectionsAvailable(), false, false, false),

                new MazeGridpoint(new CartesianCoordinate(  3 ,  2), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  1 ,  3), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  3), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  3 ,  3), new DirectionsAvailable(), false, false, false),

                new MazeGridpoint(new CartesianCoordinate(  4 ,  2), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  4), new DirectionsAvailable(), false,  true, false)
            };

            var mazeToTest = new Maze(listOfMazeGridpoints.ToDictionary(m => m.Position, m => m));
            var directionPicker = new BaseDirectionPicker(mazeToTest);
            var mazeSolution = new List<MazeSolutionElement>();

            var directionToProceed = directionPicker.PickDirectionToProceed(mazeSolution, currentMazeGridpoint);

            Assert.AreEqual(directionToProceed, DirectionEnum.Down);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void PickDirectionToProceed_CanProceedInAnyDirection_PicksDirectionUp()
        {
            var currentMazeGridpoint =
                new MazeGridpoint(new CartesianCoordinate(2, 2), new DirectionsAvailable(), false, false, false);

            var solutionPath = new List<MazeSolutionElement>
            {
                new MazeSolutionElement
                {
                    MazeGridpoint = new MazeGridpoint(new CartesianCoordinate(2, 0), new DirectionsAvailable(), true, false, false)
                }
            };

            var listOfMazeGridpoints = new List<MazeGridpoint>
            {
                new MazeGridpoint(new CartesianCoordinate(  2 ,  0), new DirectionsAvailable(),  true, false, false),
                new MazeGridpoint(new CartesianCoordinate(  0,   2), new DirectionsAvailable(), false, false,  true),

                new MazeGridpoint(new CartesianCoordinate(  1 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  3 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  1 ,  2), new DirectionsAvailable(), false, false, false),

                currentMazeGridpoint,

                new MazeGridpoint(new CartesianCoordinate(  3 ,  2), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  1 ,  3), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  3), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  3 ,  3), new DirectionsAvailable(), false, false, false),

                new MazeGridpoint(new CartesianCoordinate(  4 ,  2), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  4), new DirectionsAvailable(), false,  true, false)
            };

            var mazeToTest = new Maze(listOfMazeGridpoints.ToDictionary(m => m.Position, m => m));
            var directionPicker = new BaseDirectionPicker(mazeToTest);

            var directionToProceed = directionPicker.PickDirectionToProceed(solutionPath, currentMazeGridpoint);

            Assert.AreEqual(directionToProceed, DirectionEnum.Up);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void PickDirectionToProceed_CannotProceedInDirectionUpSinceItIsConsideredBacktracking_PicksDirectionDown()
        {
            var currentMazeGridpoint =
                new MazeGridpoint(new CartesianCoordinate(2, 2), new DirectionsAvailable(), false, false, false);

            var solutionPath = new List<MazeSolutionElement>
            {
                new MazeSolutionElement
                {
                    MazeGridpoint = new MazeGridpoint(new CartesianCoordinate(2, 0), new DirectionsAvailable(), true, false, false)
                },
                new MazeSolutionElement
                {
                    MazeGridpoint = new MazeGridpoint(new CartesianCoordinate(2, 1), new DirectionsAvailable(), true, false, false)
                }
            };

            var listOfMazeGridpoints = new List<MazeGridpoint>
            {
                new MazeGridpoint(new CartesianCoordinate(  2 ,  0), new DirectionsAvailable(),  true, false, false),
                new MazeGridpoint(new CartesianCoordinate(  0,   2), new DirectionsAvailable(), false, false,  true),

                new MazeGridpoint(new CartesianCoordinate(  1 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  3 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  1 ,  2), new DirectionsAvailable(), false, false, false),

                currentMazeGridpoint,

                new MazeGridpoint(new CartesianCoordinate(  3 ,  2), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  1 ,  3), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  3), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  3 ,  3), new DirectionsAvailable(), false, false, false),

                new MazeGridpoint(new CartesianCoordinate(  4 ,  2), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  4), new DirectionsAvailable(), false,  true, false)
            };

            var mazeToTest = new Maze(listOfMazeGridpoints.ToDictionary(m => m.Position, m => m));
            var directionPicker = new BaseDirectionPicker(mazeToTest);

            var directionToProceed = directionPicker.PickDirectionToProceed(solutionPath, currentMazeGridpoint);

            Assert.AreEqual(directionToProceed, DirectionEnum.Down);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void PickDirectionToProceed_CannotProceedInDirectionUpOrDown_PicksDirectionLeft()
        {
            var currentMazeGridpoint =
                new MazeGridpoint(new CartesianCoordinate(2, 2), new DirectionsAvailable(), false, false, false);

            var solutionPath = new List<MazeSolutionElement>
            {
                new MazeSolutionElement
                {
                    MazeGridpoint = new MazeGridpoint(new CartesianCoordinate(2, 0), new DirectionsAvailable(), true, false, false)
                },
                new MazeSolutionElement
                {
                    MazeGridpoint = new MazeGridpoint(new CartesianCoordinate(2, 1), new DirectionsAvailable(), true, false, false)
                }
            };

            var listOfMazeGridpoints = new List<MazeGridpoint>
            {
                new MazeGridpoint(new CartesianCoordinate(  2 ,  0), new DirectionsAvailable(),  true, false, false),
                new MazeGridpoint(new CartesianCoordinate(  0,   2), new DirectionsAvailable(), false, false,  true),

                new MazeGridpoint(new CartesianCoordinate(  1 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  3 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  1 ,  2), new DirectionsAvailable(), false, false, false),

                currentMazeGridpoint,

                new MazeGridpoint(new CartesianCoordinate(  3 ,  2), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  1 ,  3), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  3), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  3 ,  3), new DirectionsAvailable(), false, false, false),

                new MazeGridpoint(new CartesianCoordinate(  4 ,  2), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  4), new DirectionsAvailable(), false,  true, false)
            };

            var mazeToTest = new Maze(listOfMazeGridpoints.ToDictionary(m => m.Position, m => m));
            var directionPicker = new BaseDirectionPicker(mazeToTest);

            currentMazeGridpoint.DirectionsAvailable[DirectionEnum.Down] = false;

            var directionToProceed = directionPicker.PickDirectionToProceed(solutionPath, currentMazeGridpoint);

            Assert.AreEqual(directionToProceed, DirectionEnum.Left);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void PickDirectionToProceed_CannotProceedInDirectionUpDownOrLeft_PicksDirectionRight()
        {
            var currentMazeGridpoint =
                new MazeGridpoint(new CartesianCoordinate(2, 2), new DirectionsAvailable(), false, false, false);

            var solutionPath = new List<MazeSolutionElement>
            {
                new MazeSolutionElement
                {
                    MazeGridpoint = new MazeGridpoint(new CartesianCoordinate(2, 0), new DirectionsAvailable(), true, false, false)
                },
                new MazeSolutionElement
                {
                    MazeGridpoint = new MazeGridpoint(new CartesianCoordinate(2, 1), new DirectionsAvailable(), true, false, false)
                }
            };

            var listOfMazeGridpoints = new List<MazeGridpoint>
            {
                new MazeGridpoint(new CartesianCoordinate(  2 ,  0), new DirectionsAvailable(),  true, false, false),
                new MazeGridpoint(new CartesianCoordinate(  0,   2), new DirectionsAvailable(), false, false,  true),

                new MazeGridpoint(new CartesianCoordinate(  1 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  3 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  1 ,  2), new DirectionsAvailable(), false, false, false),

                currentMazeGridpoint,

                new MazeGridpoint(new CartesianCoordinate(  3 ,  2), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  1 ,  3), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  3), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  3 ,  3), new DirectionsAvailable(), false, false, false),

                new MazeGridpoint(new CartesianCoordinate(  4 ,  2), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  4), new DirectionsAvailable(), false,  true, false)
            };

            var mazeToTest = new Maze(listOfMazeGridpoints.ToDictionary(m => m.Position, m => m));
            var directionPicker = new BaseDirectionPicker(mazeToTest);

            currentMazeGridpoint.DirectionsAvailable[DirectionEnum.Down] = false;
            currentMazeGridpoint.DirectionsAvailable[DirectionEnum.Left] = false;

            var directionToProceed = directionPicker.PickDirectionToProceed(solutionPath, currentMazeGridpoint);

            Assert.AreEqual(directionToProceed, DirectionEnum.Right);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void PickDirectionToProceed_CannotProceedInAnydirection_PicksDirectionNone()
        {
            var currentMazeGridpoint =
                new MazeGridpoint(new CartesianCoordinate(2, 2), new DirectionsAvailable(), false, false, false);

            var solutionPath = new List<MazeSolutionElement>
            {
                new MazeSolutionElement
                {
                    MazeGridpoint = new MazeGridpoint(new CartesianCoordinate(2, 0), new DirectionsAvailable(), true, false, false)
                },
                new MazeSolutionElement
                {
                    MazeGridpoint = new MazeGridpoint(new CartesianCoordinate(2, 1), new DirectionsAvailable(), true, false, false)
                }
            };

            var listOfMazeGridpoints = new List<MazeGridpoint>
            {
                new MazeGridpoint(new CartesianCoordinate(  2 ,  0), new DirectionsAvailable(),  true, false, false),
                new MazeGridpoint(new CartesianCoordinate(  0,   2), new DirectionsAvailable(), false, false,  true),

                new MazeGridpoint(new CartesianCoordinate(  1 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  3 ,  1), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  1 ,  2), new DirectionsAvailable(), false, false, false),

                currentMazeGridpoint,

                new MazeGridpoint(new CartesianCoordinate(  3 ,  2), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  1 ,  3), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  3), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  3 ,  3), new DirectionsAvailable(), false, false, false),

                new MazeGridpoint(new CartesianCoordinate(  4 ,  2), new DirectionsAvailable(), false, false, false),
                new MazeGridpoint(new CartesianCoordinate(  2 ,  4), new DirectionsAvailable(), false,  true, false)
            };

            var mazeToTest = new Maze(listOfMazeGridpoints.ToDictionary(m => m.Position, m => m));
            var directionPicker = new BaseDirectionPicker(mazeToTest);

            currentMazeGridpoint.DirectionsAvailable[DirectionEnum.Down] = false;
            currentMazeGridpoint.DirectionsAvailable[DirectionEnum.Left] = false;
            currentMazeGridpoint.DirectionsAvailable[DirectionEnum.Right] = false;

            var directionToProceed = directionPicker.PickDirectionToProceed(solutionPath, currentMazeGridpoint);
            Assert.AreEqual(directionToProceed, DirectionEnum.None);
        }
    }
}
