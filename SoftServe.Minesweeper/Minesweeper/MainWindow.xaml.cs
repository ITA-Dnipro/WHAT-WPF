using MahApps.Metro.Controls;
using Minesweeper.ViewModels;

using System;
using System.Windows;

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var vm = new GameViewModel();

            DataContext = vm;

            vm.ClosingRequest += (sender, e) => this.Close();
        }
    }
}
