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
            ///проверяем сложность и в свойство GameField.Difficulty передаем Enum Diffuculty
            ///
            RadioButton current = (RadioButton)sender;

            string dif = (string)current.DataContext;

            int level = int.Parse(dif);

            switch ((Difficulty)level)
            {
                case Difficulty.Easy:
                    break;
                case Difficulty.Medium:
                    break;
                case Difficulty.Hard:
                    break;
                default:
                    break;
            }
        }

        private void DifficultyButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages\\GameCardPage.xaml", UriKind.Relative));
        }
    }
}
