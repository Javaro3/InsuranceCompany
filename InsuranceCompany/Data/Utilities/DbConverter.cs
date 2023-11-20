using Azure.Core.Pipeline;
using InsuranceCompany.Models;
using System.Text;

namespace InsuranceCompany.Data.Utilities {
    public static class DbConverter {
        private static Dictionary<char, string> translations = new() {
            {'а', "a"}, {'б', "b"}, {'в', "v"}, {'г', "g"},
            {'д', "d"}, {'е', "e"}, {'ё', "yo"}, {'ж', "zh"},
            {'з', "z"}, {'и', "i"}, {'й', "y"}, {'к', "k"},
            {'л', "l"}, {'м', "m"}, {'н', "n"}, {'о', "o"},
            {'п', "p"}, {'р', "r"}, {'с', "s"}, {'т', "t"},
            {'у', "u"}, {'ф', "f"}, {'х', "kh"}, {'ц', "ts"},
            {'ч', "ch"}, {'ш', "sh"}, {'щ', "sch"}, {'ъ', ""},
            {'ы', "y"}, {'ь', ""}, {'э', "e"}, {'ю', "yu"},
            {'я', "ya"},
            {'А', "A"}, {'Б', "B"}, {'В', "V"}, {'Г', "G"},
            {'Д', "D"}, {'Е', "E"}, {'Ё', "Yo"}, {'Ж', "Zh"},
            {'З', "Z"}, {'И', "I"}, {'Й', "Y"}, {'К', "K"},
            {'Л', "L"}, {'М', "M"}, {'Н', "N"}, {'О', "O"},
            {'П', "P"}, {'Р', "R"}, {'С', "S"}, {'Т', "T"},
            {'У', "U"}, {'Ф', "F"}, {'Х', "Kh"}, {'Ц', "Ts"},
            {'Ч', "Ch"}, {'Ш', "Sh"}, {'Щ', "Sch"}, {'Ъ', ""},
            {'Ы', "Y"}, {'Ь', ""}, {'Э', "E"}, {'Ю', "Yu"},
            {'Я', "Ya"}
        };


        public static string GetUserNameTranslator(Client client) {
            string userName = $"{client.Name.Trim()}_{client.Surname.Trim()}_{client.MiddleName.Trim()}";
            return GetTranslator(userName);
        }

        public static string GetUserNameTranslator(InsuranceAgent insuranceAgent) {
            string userName = $"{insuranceAgent.Name.Trim()}_{insuranceAgent.Surname.Trim()}_{insuranceAgent.MiddleName.Trim()}";
            return GetTranslator(userName);
        }

        public static string GetTranslator(string text) {
            StringBuilder userNameTranslate = new();

            foreach (char symbol in text) {
                if (translations.TryGetValue(symbol, out string translatorSymbol))
                    userNameTranslate.Append(translatorSymbol);
                else
                    userNameTranslate.Append(symbol);
            }

            return userNameTranslate.ToString();
        }
    }
}
