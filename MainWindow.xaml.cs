using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using _2048.View;

namespace _2048
{
	public partial class MainWindow : Window
	{
        public ViewModel viewModel;

        public MainWindow()
		{
			InitializeComponent();
            viewModel = (ViewModel)DataContext;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            string key = e.Key.ToString();

            viewModel.NextStep(key);
        }

        private void inkCanvas_Gesture(object sender, InkCanvasGestureEventArgs e)
        {
            string gesture = null;

            foreach (GestureRecognitionResult res in e.GetGestureRecognitionResults())
            {
                gesture = res.ApplicationGesture.ToString();
            }

            viewModel.NextStep(gesture);
        }
	}
}
