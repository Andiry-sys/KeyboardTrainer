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
using System.Windows.Threading;

namespace KeyboardTrainer
{
   
   internal sealed partial  class MainWindow : Window
    {
        private int _tempTimer = 0;
        private int _fails = 0;
        private string _baceString = "QWERTYUIOPASDFGHJKLZXCVBNM~!@#$%^&*()_+{}|:\"<>?1234567890[],./\\`-=;'qwertyuiopasdfghjklzxcvbnm";
        Random _randChar = new Random();
        private bool _flagCapsLock = true;
        private bool _flagBackespace = true;
        private bool _mesStop = true;
        private DispatcherTimer _timer = null;
        public MainWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Tick += _timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
        }
        private void CapitalLetters()
        {
            Q.Text = "Q";
            W.Text = "W";
            E.Text = "E";
            R.Text = "R";
            T.Text = "T";
            Y.Text = "Y";
            U.Text = "U";
            I.Text = "I";
            O.Text = "O";
            P.Text = "P";
            A.Text = "A";
            S.Text = "S";
            D.Text = "D";
            F.Text = "F";
            G.Text = "G";
            H.Text = "H";
            J.Text = "J";
            K.Text = "K";
            L.Text = "L";
            Z.Text = "Z";
            X.Text = "X";
            C.Text = "C";
            V.Text = "V";
            B.Text = "B";
            N.Text = "N";
            M.Text = "M";

        }

        private void LoverLetters()
        {
            Q.Text = "q";
            W.Text = "w";
            E.Text = "e";
            R.Text = "r";
            T.Text = "t";
            Y.Text = "y";
            U.Text = "u";
            I.Text = "i";
            O.Text = "o";
            P.Text = "p";
            A.Text = "a";
            S.Text = "s";
            D.Text = "d";
            F.Text = "f";
            G.Text = "g";
            H.Text = "h";
            J.Text = "j";
            K.Text = "k";
            L.Text = "l";
            Z.Text = "z";
            X.Text = "x";
            C.Text = "c";
            V.Text = "v";
            B.Text = "b";
            N.Text = "n";
            M.Text = "m";
        }

        private void LoverSymbol()
        {
            Oem3.Content = "`";
            D1.Content = "1";
            D2.Content = "2";
            D3.Content = "3";
            D4.Content = "4";
            D5.Content = "5";
            D6.Content = "6";
            D7.Content = "7";
            D8.Content = "8";
            D9.Content = "9";
            D0.Content = "0";
            OemMinus.Content = "-";
            OemPlus.Content = "=";
            OemOpenBrackets.Text = "[";
            Oem6.Text = "]";
            Oem5.Text = "\\";
            Oem1.Text = ";";
            OemQuotes.Text = "'";
            OemComma.Text = ",";
            OemPeriod.Text = ".";
            OemQuestion.Text = "/";
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            _tempTimer++;
            Speed();
        }

        private void Speed()
        {
            SpeedChar.Content = Math.Round((double)lineUser.Text.Length / _tempTimer * 60).ToString();
        }

        private void Window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            foreach(UIElement it in ((Grid)Content).Children )
            {
                if(it is Grid)
                {
                    foreach(var item in ((Grid)it).Children)
                    {
                        if(item is Button)
                        {
                            if(((Button)it).Name==e.Key.ToString())
                            {
                                ((Button)item).Opacity = 0.5;
                                if (e.Key.ToString() == "LeftShift" || e.Key.ToString() == "RightShift")
                                {
                                    CapitalLetters();
                                    if (_flagCapsLock)
                                    {
                                        CapitalLetters();
                                    }
                                    else
                                    {
                                        LoverLetters();
                                    }
                                }

                                else if(e.Key.ToString() == "Capital")
                                {
                                    if(_flagCapsLock)
                                    {
                                        CapitalLetters();
                                        _flagCapsLock = false;
                                    }
                                    else
                                    {
                                        LoverLetters();
                                        _flagCapsLock = false;
                                    }
                                }
                                else if(e.Key.ToString() == "Back")
                                {
                                    _flagBackespace = false;
                                }
                                else
                                {
                                    _flagBackespace = true;
                                }
                            }
                        }
                    }
                }

            }
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            lineUser.Focus();
            foreach(UIElement it in ((Grid)Content).Children)
            {
                foreach(object item in ((Grid)it).Children)
                {
                    if(item is Button)
                    {
                        if(((Button)item).Name == e.Key.ToString())
                        {
                            ((Button)item).Opacity = 1;
                            if(e.Key.ToString() == "LeftShift"||e.Key.ToString()=="RightShift")
                            {
                                LoverSymbol();
                                if(!_flagCapsLock)
                                {
                                    CapitalLetters();
;                               }
                                else
                                {
                                    LoverLetters();

                                }
                            }
                        }
                    }
                }
            }
            if(!_mesStop)
            {
                MessageBox.Show($"Задание завершенно!\n Коилчество символов {linePrograms.Text.Length}.\n Коилчество ошибок {Fails.Content}.\nДля завершения задания нажмите Stop.", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                _mesStop = true;

            }

        }

        private void SliderDifficulty_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider s = (Slider)sender;
            int num = (int)s.Value;
            Difficulty.Content = num.ToString();
        }
        private void CollectString(int countChar, string baceString, bool flagSensitive)
        {
            string carhs = "";
            int sensitive = (flagSensitive) ? 47 : 0;
            for (int i = 0; i < countChar; i++)
            {
                carhs += baceString[_randChar.Next(sensitive, baceString.Length)];
            }
            carhs += " ";
            int countString = (flagSensitive) ? 75 : 70;
            for (int i = 0; i < countString; i++)
            {
                linePrograms.Text += carhs[_randChar.Next(0, carhs.Length)];
            }
        }
        private void Start_button_Click(object sender, RoutedEventArgs e)
        {
            Start_button.IsEnabled = false;
            SliderDifficulty.IsEnabled = false;
            CaseSensitive.IsEnabled = false;
            Stop_button.IsEnabled = true;
            _tempTimer = 0;
            _timer.Start();
            CollectString(Convert.ToInt32(Difficulty.Content), _baceString, !(bool)CaseSensitive.IsChecked);
            lineUser.IsReadOnly = false;
            lineUser.IsEnabled = true;
            lineUser.Focus();
        }

        private void Stop_button_Click(object sender, RoutedEventArgs e)
        {
            Start_button.IsEnabled = true;
            SliderDifficulty.IsEnabled = true;
            CaseSensitive.IsEnabled = true;
            Stop_button.IsEnabled = false;
            lineUser.Text = "";
            linePrograms.Text = "";
            Fails.Content = 0;
            SpeedChar.Content = 0;
            lineUser.IsReadOnly = true;
            lineUser.IsEnabled = false;
            _timer.Stop();
            _tempTimer = 0;
            _fails = 0;
        }

        private void lineUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str = linePrograms.Text.Substring(0, lineUser.Text.Length);
            if (lineUser.Text.Equals(str))
            {
                if (_flagBackespace)
                {
                    Speed();
                }
                lineUser.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                if (_flagBackespace)
                {
                    _fails++;
                }
                lineUser.Foreground = new SolidColorBrush(Colors.Red);
                Fails.Content = _fails;
            }
            if (lineUser.Text.Length == linePrograms.Text.Length)
            {
                _timer.Stop();
                Speed();
                lineUser.IsReadOnly = true;
                _mesStop = false;
            }
        }

        private void CaseSensitive_Checked(object sender, RoutedEventArgs e)
        {
            SliderDifficulty.Maximum = 94;
        }

        private void CaseSensitive_Unchecked(object sender, RoutedEventArgs e)
        {
            SliderDifficulty.Maximum = 47;
        }
    }
}