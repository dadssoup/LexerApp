using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexerApp
{
    public enum TokenType
    {
        TypeKeyword,
        Keyword,
        BoolValue,
        Identifier,
        Operator,
        AndOperator,
        OrOperator,
        LogicalOperator,
        Constant,
        NumberConstant,
        CharConstant,
        StringConstant,
        UnavailableConstant,
        Arithmetical,
        Assignment,
        ExtendedAssignment,
        Separator,
        LineSeparator
        // Другие типы лексем, если необходимо
    }

    public struct Token
    {
        public TokenType Type;
        public string Value;
        public Token(TokenType type, string value)
        {
            Type = type; Value = value;
        }
    }

    internal class Lexer
    {
        private HashSet<string> dynamicTypes = new HashSet<string>() { "var", "auto", "dynamic" };

        private HashSet<string> types = new HashSet<string>() { "int", "char", "string", "bool" };

        private HashSet<string> boolValues = new HashSet<string>() { "true", "false" };

        private HashSet<string> keywords = new HashSet<string> { "do", "while", "loop" };

        private HashSet<string> operators = new HashSet<string> { "input", "output" };

        private HashSet<string> arithmertical = new HashSet<string> { "+", "-", "*", "/"};

        private HashSet<string> extendedAssignment = new HashSet<string> { "+=", "-=", "*=", "/=" };

        private HashSet<string> and = new HashSet<string> { "and", "&&" };

        private HashSet<string> or = new HashSet<string> { "or", "||" };

        private HashSet<string> logicalOperators = new HashSet<string> { "<", "<=", "==", ">=", ">", "!=" };

        //private HashSet<string> splitCollection = new HashSet<string> { "&&", "||",  "<=", "==", ">=", "!=", ";", ",", "<", ">", "=" };

        private HashSet<char> chars = new HashSet<char> { '&', '|', '<', '>', '=', ',', ';' , '+', '-', '*', '/'};

        public List<Token> Tokenize(string code)
        {
            List<Token> tokens = new List<Token>();

            var words = SmartSplit(code);
            //var words = code.Split(' ', '\n', '\t', '\r');
            foreach (string word in words)
            {
                string token = word.Trim();
                if (!string.IsNullOrEmpty(token))
                {
                    if (dynamicTypes.Contains(token))
                    {
                        tokens.Add(new(TokenType.TypeKeyword, token));
                    }
                    else if (types.Contains(token))
                    {
                        tokens.Add(new(TokenType.TypeKeyword, token));
                    }
                    else if (keywords.Contains(token))
                    {
                        tokens.Add(new(TokenType.Keyword, token));
                    }
                    else if (boolValues.Contains(token))
                    {
                        tokens.Add(new(TokenType.BoolValue, token));
                    }
                    else if (logicalOperators.Contains(token))
                    {
                        tokens.Add(new(TokenType.LogicalOperator, token));
                    }
                    else if (and.Contains(token))
                    {
                        tokens.Add(new(TokenType.AndOperator, token));
                    }
                    else if (or.Contains(token))
                    {
                        tokens.Add(new(TokenType.OrOperator, token));
                    }
                    else if (operators.Contains(token))
                    {
                        tokens.Add(new(TokenType.Operator, token));
                    }
                    else if (Char.IsNumber(token[0]))
                    {
                        if (HasOnlyNumbers(token))
                        {
                            tokens.Add(new(TokenType.NumberConstant, token));
                        }
                        else
                        {
                            tokens.Add(new(TokenType.UnavailableConstant, token));
                        }
                    }
                    else if (token.StartsWith("\"") && token.EndsWith("\""))
                    {
                        tokens.Add(new(TokenType.StringConstant, token.Replace("\"", "")));
                    }
                    else if (token.StartsWith("\'") && token.EndsWith("\'"))
                    {
                        if (token.Length > 3)
                        {
                            tokens.Add(new(TokenType.CharConstant, token.Replace("\'", "")));
                        }
                        else
                        {
                            tokens.Add(new(TokenType.UnavailableConstant, token.Replace("\'", "")));
                        }
                    }
                    else if (token.Equals("="))
                    {
                        tokens.Add(new(TokenType.Assignment, token));
                    }
                    else if (extendedAssignment.Contains(token))
                    {
                        tokens.Add(new(TokenType.ExtendedAssignment, token));
                    }
                    else if (arithmertical.Contains(token))
                    {
                        tokens.Add(new(TokenType.Arithmetical, token));
                    }
                    else if(token.Equals(","))
                    {
                        tokens.Add(new(TokenType.Separator, token));
                    }
                    else if (token.Equals(";"))
                    {
                        tokens.Add(new(TokenType.LineSeparator, token));
                    }
                    else
                    {
                        tokens.Add(new(TokenType.Identifier, token));
                    }
                }
            }

            return tokens;
        }

        public bool HasOnlyNumbers(string word)
        {
            foreach (var c in word)
            {
                if (!Char.IsNumber(c))
                {
                    return false;
                }
            }
            return true;
        }

        public List<string> SmartSplit(string code)
        {
            string[] words = code.Split(' ', '\n', '\t', '\r');
            List<string> newWords = new List<string>();
            foreach (string word in words)
            {
                string token = word.Trim();
                if (!string.IsNullOrEmpty(token))
                {
                    string nw = "" + token[0];
                    // replcaced from
                    for (int i = 1; i < token.Length; i++)
                    {
                        if(chars.Contains(token[i]) == chars.Contains(token[i - 1]))
                        {
                            nw += token[i];
                        }
                        else
                        {
                            newWords.Add(nw);
                            nw = "" + token[i];
                        }
                    }
                    newWords.Add(nw);
                }
            }
            return newWords;
        }
    }
}
// replaced
//bool addition = false;
//foreach (string splitter in splitCollection)
//{
//    var splitted = token.Split(splitter);
//    if (splitted.Length > 1)
//    {
//        addition = true;
//        bool first = true;
//        newWords.Add(splitted[0].Trim());
//        foreach (var s in splitted)
//        {
//            if (first)
//            {
//                first = false;
//                continue;
//            }
//            else
//            {
//                newWords.Add(splitter);
//                newWords.Add(s.Trim());
//            }
//        }
//    }
//    if(addition)
//    {
//        break;
//    }
//}
//if(!addition)
//{
//    newWords.Add(token);
//}