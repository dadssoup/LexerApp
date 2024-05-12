using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace LexerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Analyze_Click(object sender, RoutedEventArgs e)
        {
            lex.Text = "";
            
            Lexer lexer = new Lexer();
            foreach (var token in lexer.Tokenize(UserText.Text))
            {
                lex.Text += $"{token.Type.ToString()}: {token.Value}\n";
            }
        }
    }
}