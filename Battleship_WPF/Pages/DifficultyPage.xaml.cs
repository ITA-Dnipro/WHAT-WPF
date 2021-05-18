using BattleshipLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Battleship_WPF
{
    /// <summary>
    /// Логика взаимодействия для DifficultyPage.xaml
    /// </summary>
    public partial class DifficultyPage : Page
    {
        public DifficultyPage()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton current = (RadioButton)sender;

            string difficulty = (string)current.DataContext;
            int level = int.Parse(difficulty);
            Level currentLevel = Level.None;

            switch ((Level)level)
            {
                case Level.Easy:
                    currentLevel = Level.Easy;
                    break;
                case Level.Meduim:
                    currentLevel = Level.Meduim;
                    break;
                case Level.Hard:
                    currentLevel = Level.Hard;
                    break;
                default:
                    break;
            }

            LevelContainer.Difficulty = currentLevel;
        }

        private void DifficultyButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages\\GameCardPage.xaml", UriKind.Relative));
        }
    }
}
