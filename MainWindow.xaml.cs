using System.Windows;
using System.Windows.Input;
using _2048.Enums;
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
            switch (e.Key)
            {
                case Key.Up: 
                    viewModel.NextStep(MoveDirection.Up); 
                    break;
                case Key.Down: 
                    viewModel.NextStep(MoveDirection.Down); 
                    break;
                case Key.Left: 
                    viewModel.NextStep(MoveDirection.Left); 
                    break;
                case Key.Right: 
                    viewModel.NextStep(MoveDirection.Right); 
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) //TODO: Command
        {
            viewModel.Initialize();
		}
	}
}
