using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculate
{
    public partial class Form1 : Form
    {
        // Добавить в калькулятор скобки и применить алгоритм Рутисхаузера
        // Тренировка: программа с текстовым полем и лейблом. печатаю в лэйбле символы и в текстовом поле отображается кол-во введёных символов.
        // Text.Change в  actions textBox1

        private char[] _operations = { '+', '−', '÷', 'x' };
        private char _currentOperation = ' ';
        public Form1()
        {
            InitializeComponent();
        }

        #region Numbers
        private void btn0_Click(object sender, EventArgs e)
        {
            textBox1.Text += "0";
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            textBox1.Text += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            textBox1.Text += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            textBox1.Text += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            textBox1.Text += "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            textBox1.Text += "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            textBox1.Text += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "9";
        }
        #endregion

        #region Operations
        private void btnPoint_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
                textBox1.Text += "0,";

            if (_currentOperation != ' ')
            {
                if (textBox1.Text.ToCharArray().Last() == _currentOperation)
                    textBox1.Text += "0,";
                else
                {
                    if(!textBox1.Text.Split(_operations)[1].Contains(','))
                        textBox1.Text += ",";
                } 
            }
            else
            {
                if(!textBox1.Text.Contains(','))
                    textBox1.Text += ",";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string str = textBox1.Text;
            if (str.Length != 0)
                textBox1.Text = str.Remove(str.Length - 1);
        }

        private void btnDiv_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)
            {
                CalculateResult();
                if (_operations.ToList().Intersect(textBox1.Text.ToCharArray()).Count() == 0)
                {
                    textBox1.Text += "÷";
                    _currentOperation = '÷';
                } 
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)
            {
                CalculateResult();
                if (_operations.ToList().Intersect(textBox1.Text.ToCharArray()).Count() == 0)
                {
                    textBox1.Text += "x";
                    _currentOperation = 'x';
                }
            }
        }

        private void btnDif_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)
            {
                CalculateResult();
                if (_operations.ToList().Intersect(textBox1.Text.ToCharArray()).Count() == 0)
                {
                    textBox1.Text += "−";
                    _currentOperation = '−';
                }
            }       
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)
            {
                CalculateResult();
                if (_operations.ToList().Intersect(textBox1.Text.ToCharArray()).Count() == 0)
                {
                    textBox1.Text += "+";
                    _currentOperation = '+';
                }
            }
        }
        
        private void btnSign_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
                textBox1.Text += '-';
            else
            { 
                if (_currentOperation == ' ')
                {
                    char firstSym = textBox1.Text.First();
                    if (firstSym == '-')
                        textBox1.Text = textBox1.Text.Remove(0, 1);
                    else
                        textBox1.Text = '-' + textBox1.Text;
                }
                else 
                {
                    int index = textBox1.Text.IndexOf(_currentOperation);

                    if (textBox1.Text.Length-1 == index)
                        textBox1.Text += '-';
                    else
                    {
                        char firstSym = textBox1.Text.ToCharArray()[index + 1];

                        if (firstSym == '-')
                            textBox1.Text = textBox1.Text.Remove(index + 1, 1);
                        else
                        {
                            var parts = textBox1.Text.Split(_operations);

                            textBox1.Text = parts[0] + _currentOperation + '-' + parts[1];
                        }
                    }                  
                }
            }
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
                CalculateResult();
        }

        #endregion

        private void CalculateResult()
        {
            string txt = textBox1.Text;

            var parts = txt.Split(_operations);

            if (parts.Count() != 2 || _operations.Contains(txt.ToCharArray().Last()))
                return;
            
            double leftPart = Convert.ToDouble(parts[0]);
            double rightPart = Convert.ToDouble(parts[1]);

            double result = 0;

            switch (_currentOperation)
            {
                case 'x':
                    result = leftPart * rightPart;
                    break;
                case '−':
                    result = leftPart - rightPart;
                    break;
                case '÷':
                    result = leftPart / rightPart;
                    break;
                case '+':
                    result = leftPart + rightPart;
                    break;
            }

            result = Math.Round(result, 2);

            textBox1.Text = result.ToString();

            _currentOperation = ' ';
        }
    }
}