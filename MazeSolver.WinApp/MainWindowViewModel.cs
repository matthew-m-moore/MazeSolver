using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using IO.Adapters;
using IO.Utilities;
using MazeSolver.Core.MazeElements;
using MazeSolver.Core.SolverLogic;
using MazeSolver.WinApp.Core;
using MazeSolver.WinApp.Listeners;

namespace MazeSolver.WinApp
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private const string _mazeImageFilePath = @"..\..\Images\SmallSizeWinAppMaze.png";
        public string MazeSolutionImageFilePath { get; set; }

        // This property is mapped to the image that appears in the application
        public BitmapSource _mazeImageBitmapSource;
        public BitmapSource MazeImageBitmapSource
        {
            get
            {
                return _mazeImageBitmapSource;
            }
            set
            {
                _mazeImageBitmapSource = value;
                OnPropertyChanged();
            }
        }

        private Image _mazeImage { get; set; }
        private Maze _mazeToSolve { get; set; }
        private BackgroundWorker _solveMazeWorker;
        private bool _isBusySolving;

        // This property binds to the check-box for "Watch Maze Get Solved"
        private bool? _watchMazeGetSovled;
        public bool WatchMazeGetSovled
        {
            get
            {
                return _watchMazeGetSovled ?? true;
            }
            set
            {
                _watchMazeGetSovled = value;
                OnPropertyChanged();
            }
        }

        // This property binds to the combo-box (i.e. drop-down menu) for logic selection
        public List<string> MazeSolverLogicDescriptions =>
            _mazeSolverLogics.Select(solverLogic => solverLogic.Description).ToList();
        private List<MazeSolution> _mazeSolverLogics { get; set; }

        // This property binds to the item selected in the combo-box
        private string _selectedSolverLogicDescription;
        public string SelectedSolverLogicDescription
        {
            get
            {
                return _selectedSolverLogicDescription;
            }
            set
            {
                _selectedSolverLogicDescription = value;
                OnPropertyChanged();
            }
        }

        private MazeSolution _selectedSolverLogic =>
            _mazeSolverLogics.Single(solverLogic => solverLogic.Description == _selectedSolverLogicDescription);

        public MainWindowViewModel()
        {
            const bool useEmbeddedColorManagement = true;
            _mazeImage = Image.FromFile(_mazeImageFilePath, useEmbeddedColorManagement);
            _mazeToSolve = MazeConverter.ConvertImageToMaze(_mazeImage);

            var mazeImageBitmap = new Bitmap(_mazeImage);
            MazeImageBitmapSource = MazeRedrawnListener.BitmapToBitmapSource(mazeImageBitmap);

            InstantiateMazeSolutionLogicList();
        }

        // This is the command bound to the "Solve" button
        private ICommand _solve;
        public ICommand Solve
        {
            get
            {
                return _solve ?? 
                    (
                        _solve = new ExecuteCommand
                        (
                            parameter => SolveMaze(), 
                            parameter => !string.IsNullOrEmpty(MazeSolutionImageFilePath)
                                      && !string.IsNullOrEmpty(SelectedSolverLogicDescription)
                                      && !_isBusySolving
                        )
                    );
            }
        }

        private void SolveMaze()
        {
            _solveMazeWorker = new BackgroundWorker();
            _solveMazeWorker.DoWork += (sender, e) => SolveMazeOnThread();
            _solveMazeWorker.RunWorkerAsync();
        }

        private void SolveMazeOnThread()
        {
            // If the maze image has already been solved, this will reset it
            var mazeImageBitmap = new Bitmap(_mazeImage);
            MazeImageBitmapSource = MazeRedrawnListener.BitmapToBitmapSource(mazeImageBitmap);

            var mazeRedrawnListener = new MazeRedrawnListener();

            try
            {
                _isBusySolving = true;

                // Subscribed to the event to watch the maze get solved
                if (WatchMazeGetSovled)
                {
                    mazeRedrawnListener.Subscribe(_selectedSolverLogic, this);

                    // Pretreatment logic is not very fun to watch
                    _selectedSolverLogic.PreTreatmentLogics.Clear();
                }

                _selectedSolverLogic.SolveMaze();
                _selectedSolverLogic.MarkSolutionPath();

                var mazeWriter = new MazeWriterUtility(MazeSolutionImageFilePath, _selectedSolverLogic.MazeToSolve);
                mazeWriter.SaveMazeImage();

                // This should dispaly the solved path on the maze highlighted in green
                mazeImageBitmap = new Bitmap(mazeWriter.MazeImage);
                MazeImageBitmapSource = MazeRedrawnListener.BitmapToBitmapSource(mazeImageBitmap);
            }
            catch (Exception exceptionCaught)
            {
                throw exceptionCaught;
            }
            finally
            {
                _isBusySolving = false;
                mazeRedrawnListener.UnSubscribe(_selectedSolverLogic);
                
                const bool useEmbeddedColorManagement = true;
                _mazeImage = Image.FromFile(_mazeImageFilePath, useEmbeddedColorManagement);
                _mazeToSolve = MazeConverter.ConvertImageToMaze(_mazeImage);
                InstantiateMazeSolutionLogicList();
            }
        }

        private void InstantiateMazeSolutionLogicList()
        {
            _mazeSolverLogics = new List<MazeSolution>();
            _mazeSolverLogics.Add(new BruteForceMazeSolution(_mazeToSolve));
            _mazeSolverLogics.Add(new BruteForceConstantDirectionMazeSolution(_mazeToSolve));
            _mazeSolverLogics.Add(new BruteForceWallHuggingMazeSolution(_mazeToSolve));
            _mazeSolverLogics.Add(new BreadthFirstMazeSolution(_mazeToSolve));
        }

        // This property changed event handler will allow detection of changes to the selected
        // solver logic from the user
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = PropertyChanged;
            if (eventHandler == null) return;

            var e = new PropertyChangedEventArgs(propertyName);
            eventHandler(this, e);
        }
    }
}
