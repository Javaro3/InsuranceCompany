using System.Text;

namespace InsuranceCompany.Data.Utilities {
    public static class DbConverter {
        public static Dictionary<char, string> translations = new() {
            {'а', "a"}, {'б', "b"}, {'в', "v"}, {'г', "g"},
                {'д', "d"}, {'е', "e"}, {'ё', "yo"}, {'ж', "zh"},
                {'з', "z"}, {'и', "i"}, {'й', "y"}, {'к', "k"},
                {'л', "l"}, {'м', "m"}, {'н', "n"}, {'о', "o"},
                {'п', "p"}, {'р', "r"}, {'с', "s"}, {'т', "t"},
                {'у', "u"}, {'ф', "f"}, {'х', "kh"}, {'ц', "ts"},
                {'ч', "ch"}, {'ш', "sh"}, {'щ', "sch"}, {'ъ', ""},
                {'ы', "y"}, {'ь', ""}, {'э', "e"}, {'ю', "yu"},
                {'я', "ya"}
        };

        public static string GetUserNameTranslator(string userName) {
            userName = userName.ToLower();
            StringBuilder userNameTranslate = new StringBuilder();

            foreach (char symbol in userName) {
                if (translations.TryGetValue(symbol, out string translatorSymbol)) {
                    userNameTranslate.Append(translatorSymbol);
                }
                else {
                    userNameTranslate.Append(symbol);
                }
            }

            return userNameTranslate.ToString();
        }
    }
}
