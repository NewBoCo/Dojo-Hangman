using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanLibrary
{
    public static class HangmanUtilities
    {
        private static readonly IEnumerable<string> _words = new List<string> { "suppress",
"qualify",
"tank",
"trustee",
"revenge",
"ladder",
"ruin",
"sniff",
"affair",
"sensitive",
"eliminate",
"breathe",
"message",
"integration",
"lack",
"surprise",
"deserve",
"insure",
"wage",
"sight",
"rhetoric",
"leg",
"guide",
"charm",
"sugar",
"clue",
"absolute",
"loot",
"purpose",
"fragment",
"root",
"evolution",
"user",
"seminar",
"tourist",
"introduce",
"nut",
"housewife",
"main",
"standard",
"labour",
"pocket",
"disagree",
"salesperson",
"biology",
"limited",
"collar",
"difficulty",
"honest",
"engine",
"castle",
"manner",
"golf",
"basket",
"adult",
"field",
"dismissal",
"transmission",
"generate",
"rider",
"policeman",
"cream",
"challenge",
"ignorance",
"painter",
"sign",
"wedding",
"length",
"houseplant",
"close",
"thrust",
"kidnap",
"convert",
"linear",
"decide",
"effort",
"authorise",
"relate",
"position",
"moon",
"assembly",
"overeat",
"hide",
"audience",
"heir",
"strength",
"dawn",
"roof",
"ceremony",
"wound",
"romantic",
"execution",
"mood",
"plot",
"nail",
"criminal",
"date",
"dairy",
"prison",
"fair"};

        public static readonly SortedSet<char> AllLetters = new SortedSet<char>
        {
            'A','B','C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
            'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        public static string GetRandomWord()
        {
            var wordCount = _words.Count();

            var random = new Random();

            var index = random.Next(0, wordCount + 1);

            return _words.ElementAt(index);
        }
    }
}
