using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace MathEditorStandalone
{
    public partial class MainWindow : Window
    {
        private Dictionary<string, double> variables = new Dictionary<string, double>();

        public MainWindow()
        {
            InitializeComponent();
            LoadSampleCode();
            KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                RunScript();
            }
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            RunScript();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ScriptEditor.Text = "";
            variables.Clear();
            MathOutputPanel.Children.Clear();
            
            var helpText = new TextBlock
            {
                Text = "Mathematical results will appear here\nUse ; to suppress output, omit ; to show result",
                FontStyle = FontStyles.Italic,
                Foreground = Brushes.Gray,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 50, 0, 0),
                TextAlignment = TextAlignment.Center
            };
            MathOutputPanel.Children.Add(helpText);
        }

        private void RunScript()
        {
            string code = ScriptEditor.Text;
            
            if (string.IsNullOrWhiteSpace(code))
            {
                return;
            }

            try
            {
                variables.Clear();
                MathOutputPanel.Children.Clear();
                
                var results = ParseAndExecuteMath(code);
                DisplayMathResults(results);
            }
            catch (Exception ex)
            {
                MathOutputPanel.Children.Clear();
                var errorText = new TextBlock
                {
                    Text = $"Error: {ex.Message}",
                    Foreground = Brushes.Red,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 10, 0, 0)
                };
                MathOutputPanel.Children.Add(errorText);
            }
        }

        private void DisplayMathResults(List<MathResult> results)
        {
            foreach (var result in results)
            {
                if (!string.IsNullOrEmpty(result.Error))
                {
                    var errorText = new TextBlock
                    {
                        Text = $"Error: {result.Error}",
                        Foreground = Brushes.Red,
                        Margin = new Thickness(0, 5, 0, 0)
                    };
                    MathOutputPanel.Children.Add(errorText);
                    continue;
                }

                if (result.ShowOutput)
                {
                    var mathPanel = CreateMathEquation(result);
                    MathOutputPanel.Children.Add(mathPanel);
                }
            }

            if (MathOutputPanel.Children.Count == 0)
            {
                var noOutputText = new TextBlock
                {
                    Text = "No output to display (all expressions ended with ;)",
                    FontStyle = FontStyles.Italic,
                    Foreground = Brushes.Gray,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 20, 0, 0)
                };
                MathOutputPanel.Children.Add(noOutputText);
            }
        }

        private StackPanel CreateMathEquation(MathResult result)
        {
            var panel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 10, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            // Parte izquierda de la ecuación
            if (!string.IsNullOrEmpty(result.Variable))
            {
                // Variable name
                var varText = new TextBlock
                {
                    Text = result.Variable,
                    FontFamily = new FontFamily("Times New Roman"),
                    FontSize = 18,
                    FontStyle = FontStyles.Italic,
                    VerticalAlignment = VerticalAlignment.Center
                };
                panel.Children.Add(varText);
            }
            else
            {
                // Expression
                var exprText = CreateFormattedExpression(result.Expression);
                panel.Children.Add(exprText);
            }

            // Signo igual
            var equalsText = new TextBlock
            {
                Text = " = ",
                FontFamily = new FontFamily("Times New Roman"),
                FontSize = 18,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5, 0, 5, 0)
            };
            panel.Children.Add(equalsText);

            // Resultado
            var resultText = new TextBlock
            {
                Text = FormatDisplayNumber(result.Value),
                FontFamily = new FontFamily("Times New Roman"),
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Color.FromRgb(0, 100, 0)),
                VerticalAlignment = VerticalAlignment.Center
            };
            panel.Children.Add(resultText);

            return panel;
        }

        private TextBlock CreateFormattedExpression(string expression)
        {
            var formattedExpr = FormatMathExpression(expression);
            
            return new TextBlock
            {
                Text = formattedExpr,
                FontFamily = new FontFamily("Times New Roman"),
                FontSize = 18,
                VerticalAlignment = VerticalAlignment.Center
            };
        }

        private string FormatMathExpression(string expression)
        {
            string formatted = expression;

            // Funciones matemáticas con símbolos especiales
            formatted = formatted.Replace("sin", "sin");
            formatted = formatted.Replace("cos", "cos");
            formatted = formatted.Replace("tan", "tan");
            formatted = formatted.Replace("sqrt", "√");
            formatted = formatted.Replace("pi", "π");
            formatted = formatted.Replace("*", "×");
            formatted = formatted.Replace("/", "÷");

            // Superíndices para potencias (limitado)
            formatted = Regex.Replace(formatted, @"([a-zA-Z0-9π]+)\^2", "$1²");
            formatted = Regex.Replace(formatted, @"([a-zA-Z0-9π]+)\^3", "$1³");
            formatted = Regex.Replace(formatted, @"([a-zA-Z0-9π]+)\^4", "$1⁴");
            formatted = Regex.Replace(formatted, @"([a-zA-Z0-9π]+)\^5", "$1⁵");
            formatted = Regex.Replace(formatted, @"([a-zA-Z0-9π]+)\^6", "$1⁶");
            formatted = Regex.Replace(formatted, @"([a-zA-Z0-9π]+)\^7", "$1⁷");
            formatted = Regex.Replace(formatted, @"([a-zA-Z0-9π]+)\^8", "$1⁸");
            formatted = Regex.Replace(formatted, @"([a-zA-Z0-9π]+)\^9", "$1⁹");
            formatted = Regex.Replace(formatted, @"([a-zA-Z0-9π]+)\^(-?\d+)", "$1^($2)");

            return formatted;
        }

        private string FormatDisplayNumber(double value)
        {
            if (Math.Abs(value - Math.Round(value)) < 1e-10)
            {
                // Número entero
                return Math.Round(value).ToString();
            }
            else if (Math.Abs(value) >= 1000)
            {
                // Números grandes: notación científica o decimal
                if (Math.Abs(value) >= 1000000)
                    return $"{value:E2}";
                else
                    return $"{value:F0}";
            }
            else if (Math.Abs(value) <= 0.001 && value != 0)
            {
                // Números muy pequeños: notación científica
                return $"{value:E2}";
            }
            else
            {
                // Decimales normales
                return Math.Round(value, 6).ToString("G6");
            }
        }

        private List<MathResult> ParseAndExecuteMath(string code)
        {
            var results = new List<MathResult>();
            var lines = code.Split('\n');
            
            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith("%"))
                    continue;

                try
                {
                    bool suppressOutput = trimmedLine.EndsWith(";");
                    string cleanLine = trimmedLine.TrimEnd(';');

                    if (cleanLine.Contains("="))
                    {
                        var result = ProcessVariableAssignment(cleanLine, suppressOutput);
                        if (result != null) results.Add(result);
                    }
                    else if (variables.ContainsKey(cleanLine))
                    {
                        var result = new MathResult
                        {
                            Variable = cleanLine,
                            Value = variables[cleanLine],
                            ShowOutput = !suppressOutput,
                            IsVariableReference = true,
                            OriginalLine = trimmedLine
                        };
                        results.Add(result);
                    }
                    else if (!string.IsNullOrWhiteSpace(cleanLine))
                    {
                        var result = ProcessDirectExpression(cleanLine, suppressOutput);
                        if (result != null) results.Add(result);
                    }
                }
                catch (Exception ex)
                {
                    results.Add(new MathResult { Expression = trimmedLine, Error = ex.Message, ShowOutput = true });
                }
            }

            return results;
        }

        private MathResult ProcessVariableAssignment(string line, bool suppressOutput)
        {
            var match = Regex.Match(line, @"(\w+)\s*=\s*(.+)$");
            if (!match.Success) return null;

            string varName = match.Groups[1].Value;
            string expression = match.Groups[2].Value;

            double value = EvaluateExpression(expression);
            variables[varName] = value;

            return new MathResult 
            { 
                Variable = varName, 
                Expression = expression, 
                Value = value,
                ShowOutput = !suppressOutput,
                OriginalLine = line
            };
        }

        private MathResult ProcessDirectExpression(string line, bool suppressOutput)
        {
            try
            {
                double value = EvaluateExpression(line);
                
                return new MathResult 
                { 
                    Expression = line, 
                    Value = value,
                    ShowOutput = !suppressOutput,
                    OriginalLine = line
                };
            }
            catch
            {
                return null;
            }
        }

        private double EvaluateExpression(string expression)
        {
            foreach (var variable in variables)
            {
                expression = Regex.Replace(expression, @"\b" + variable.Key + @"\b", variable.Value.ToString(CultureInfo.InvariantCulture));
            }

            expression = ReplaceMatlabFunctions(expression);

            try
            {
                var table = new DataTable();
                var result = table.Compute(expression, null);
                return Convert.ToDouble(result);
            }
            catch
            {
                return EvaluateBasicExpression(expression);
            }
        }

        private string ReplaceMatlabFunctions(string expression)
        {
            expression = expression.Replace("pi", Math.PI.ToString(CultureInfo.InvariantCulture));
            expression = expression.Replace("e", Math.E.ToString(CultureInfo.InvariantCulture));

            expression = Regex.Replace(expression, @"\bsin\(([^)]+)\)", m => Math.Sin(EvaluateBasicExpression(m.Groups[1].Value)).ToString(CultureInfo.InvariantCulture));
            expression = Regex.Replace(expression, @"\bcos\(([^)]+)\)", m => Math.Cos(EvaluateBasicExpression(m.Groups[1].Value)).ToString(CultureInfo.InvariantCulture));
            expression = Regex.Replace(expression, @"\btan\(([^)]+)\)", m => Math.Tan(EvaluateBasicExpression(m.Groups[1].Value)).ToString(CultureInfo.InvariantCulture));
            expression = Regex.Replace(expression, @"\bsqrt\(([^)]+)\)", m => Math.Sqrt(EvaluateBasicExpression(m.Groups[1].Value)).ToString(CultureInfo.InvariantCulture));
            expression = Regex.Replace(expression, @"\blog\(([^)]+)\)", m => Math.Log(EvaluateBasicExpression(m.Groups[1].Value)).ToString(CultureInfo.InvariantCulture));
            expression = Regex.Replace(expression, @"\bexp\(([^)]+)\)", m => Math.Exp(EvaluateBasicExpression(m.Groups[1].Value)).ToString(CultureInfo.InvariantCulture));
            expression = Regex.Replace(expression, @"\babs\(([^)]+)\)", m => Math.Abs(EvaluateBasicExpression(m.Groups[1].Value)).ToString(CultureInfo.InvariantCulture));
            
            expression = Regex.Replace(expression, @"([^*+\-/\s\^]+)\s*\^\s*([^*+\-/\s\^]+)", m => 
                Math.Pow(EvaluateBasicExpression(m.Groups[1].Value), EvaluateBasicExpression(m.Groups[2].Value)).ToString(CultureInfo.InvariantCulture));

            return expression;
        }

        private double EvaluateBasicExpression(string expression)
        {
            try
            {
                var table = new DataTable();
                var result = table.Compute(expression, null);
                return Convert.ToDouble(result);
            }
            catch
            {
                if (double.TryParse(expression, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                    return value;
                return 0;
            }
        }

        private void LoadSampleCode()
        {
            string sampleCode = @"% Mathematical expressions with visual output
x = 10;
y = 5;
result = x + y

% Show variables
x
y

% Trigonometric functions
sin(pi/2)
cos(0)
tan(pi/4)

% Powers and roots
2^8
sqrt(16)
3^2";

            ScriptEditor.Text = sampleCode;
        }
    }

    public class MathResult
    {
        public string Variable { get; set; }
        public string Expression { get; set; }
        public double Value { get; set; }
        public string Error { get; set; }
        public string OriginalLine { get; set; }
        public bool ShowOutput { get; set; } = true;
        public bool IsVariableReference { get; set; } = false;
    }
}
