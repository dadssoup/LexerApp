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
            
            
            Lexer lexer = new Lexer();
            var tokens = lexer.Tokenize(UserText.Text);
            var tokensG = tokens.GroupBy(token => token.Type).Select(token => new { Тип = token.Key, Количество = token.Count() });
            var iTokens = tokens.Where(token => token.Type == TokenType.Identifier 
                                        || token.Type == TokenType.KeywordError).Select(token => new { Тип = token.Type, Значение = token.Value });
            var cTokens = tokens.Where(token => token.Type == TokenType.NumberConstant
                                        || token.Type == TokenType.StringConstant
                                        || token.Type == TokenType.CharConstant
                                        || token.Type == TokenType.UnavailableConstant).Select(token => new { Тип = token.Type, Значение = token.Value });
            LexTable.ItemsSource = tokensG;
            identifierTable.ItemsSource = iTokens;
            constantTable.ItemsSource = cTokens;
        }
    }
}