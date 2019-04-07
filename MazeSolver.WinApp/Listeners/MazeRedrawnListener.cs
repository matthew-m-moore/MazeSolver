using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using IO.Adapters;
using MazeSolver.Core.SolverLogic;
using MazeSolver.Events;

namespace MazeSolver.WinApp.Listeners
{
    public class MazeRedrawnListener
    {
        private Image _mazeImage;
        private MainWindowViewModel _viewModel;

        public void Subscribe(MazeSolution mazeSolution, MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            mazeSolution.MazeToSolve.MazeToBeRedrawn += OnMazeRedrawn;
        }

        public void UnSubscribe(MazeSolution mazeSolution)
        {
            mazeSolution.MazeToSolve.MazeToBeRedrawn -= OnMazeRedrawn;
        }

        public void OnMazeRedrawn(object sender, MazeRedrawnEventArgs e)
        {
            _mazeImage = MazeConverter.ConvertMazeToImage(e.MazeToBeRedrawn);

            var mazeImageBitmap = new Bitmap(_mazeImage);
            var mazeImageBitmapSource = BitmapToBitmapSource(mazeImageBitmap);

            _viewModel.MazeImageBitmapSource = mazeImageBitmapSource;
        }

        // To bind to the bitmap image in XAML, it has to be of an object type 
        // like BitmapSource or ImageSource.
        public static BitmapSource BitmapToBitmapSource(Bitmap bitmap)
        {
            // Using a memory stream will allow for updating the image using
            // events, rather than with a static file path.
            using (var memoryStreamForImage = new MemoryStream())
            {
                bitmap.Save(memoryStreamForImage, ImageFormat.Bmp);
                memoryStreamForImage.Position = 0;

                var bitmapImage = new BitmapImage();
          
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStreamForImage;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}
