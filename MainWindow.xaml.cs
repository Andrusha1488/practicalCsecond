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

namespace practic1
{
    public partial class MainWindow : Window
    {
        private bool playerXTurn = true;
        private bool gameEnded = false;
        private List<Button> buttons;

        public MainWindow()
        {
            InitializeComponent();
            buttons = new List<Button> { btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9 };
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!gameEnded)
            {
                var button = (Button)sender;
                if (button.Content == null)
                {
                    button.Content = playerXTurn ? "X" : "O";
                    playerXTurn = !playerXTurn;
                    button.IsEnabled = false;
                    CheckForWinner();
                    if (!gameEnded && !playerXTurn)
                    {
                        MakeRobotMove();
                    }
                }
            }
        }

        private void MakeRobotMove()
        {
            Random random = new Random();
            int index = random.Next(buttons.Count);
            while (buttons[index].Content != null)
            {
                index = random.Next(buttons.Count);
            }
            buttons[index].Content = "O";
            buttons[index].IsEnabled = false;
            playerXTurn = true;
            CheckForWinner();
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            RestartGame();
        }

        private void RestartGame()
        {
            foreach (var button in buttons)
            {
                button.Content = null;
                button.IsEnabled = true;
            }
            gameEnded = false;
            playerXTurn = true;
        }

        private void CheckForWinner()
        {
            if ((btn1.Content != null && btn1.Content == btn2.Content && btn2.Content == btn3.Content) ||
                (btn4.Content != null && btn4.Content == btn5.Content && btn5.Content == btn6.Content) ||
                (btn7.Content != null && btn7.Content == btn8.Content && btn8.Content == btn9.Content) ||
                (btn1.Content != null && btn1.Content == btn4.Content && btn4.Content == btn7.Content) ||
                (btn2.Content != null && btn2.Content == btn5.Content && btn5.Content == btn8.Content) ||
                (btn3.Content != null && btn3.Content == btn6.Content && btn6.Content == btn9.Content) ||
                (btn1.Content != null && btn1.Content == btn5.Content && btn5.Content == btn9.Content) ||
                (btn3.Content != null && btn3.Content == btn5.Content && btn5.Content == btn7.Content))
            {
                gameEnded = true;
                MessageBox.Show(playerXTurn ? "Нолики победили" : "Крестики победили");
                DisableButtons();
            }
            else if (!buttons.Exists(b => b.Content == null))
            {
                gameEnded = true;
                MessageBox.Show("Ничья");
                DisableButtons();
            }
        }

        private void DisableButtons()
        {
            foreach (var button in buttons)
            {
                button.IsEnabled = false;
            }
        }


    }
}
