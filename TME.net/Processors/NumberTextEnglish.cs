using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using TME.Enums;
using TME.Interfaces;

namespace TME.Processors
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class NumberTextEnglish : INumberText
    {
        // SS_NUMBERS, one|two|three|four|five|six|seven|eight|nine|ten|eleven|twelve|thirteen|fourteen|fifteen|sixteen|seventeen|eighteen|nineteen|twenty|thirty|forty|fifty|sixty|seventy|eighty|ninety|hundred|thousand
        private readonly string[] _numberTokens;

        // SS_ZEROTOKENS,|no|none|zero
        private readonly string[] _zeroTokens;

        public NumberTextEnglish(IStrings strings)
        {
            var numbers = strings.GetBySymbol("SS_NUMBERS");
            _numberTokens = numbers?.Text.Split('|') ?? Array.Empty<string>();

            var zero = strings.GetBySymbol("SS_ZEROTOKENS");
            _zeroTokens = zero?.Text.Split('|') ?? Array.Empty<string>();
        }

        public string Describe(int number, ZeroMode zeroMode)
        {
            var builder = new StringBuilder();
            var useAnd = false;

            var thousands = number / 1000;
            number %= 1000;
            var hundreds = number / 100;
            number %= 100;
            var tens = number / 10;
            number %= 10;

            if (thousands > 0)
            {
                builder.Append(Describe(thousands, zeroMode));
                builder.Append(" ");
                builder.Append(_numberTokens[28]);
                useAnd = true;
            }

            if (hundreds > 0)
            {
                if (useAnd) builder.Append(" ");
                builder.Append(Describe(hundreds, zeroMode));
                builder.Append(" ");
                builder.Append(_numberTokens[27]);
                useAnd = true;
            }

            if (tens > 1)
            {
                if (useAnd) builder.Append(" and ");
                builder.Append(_numberTokens[19 + tens - 2]);
                useAnd = false;
                if (number == 0)
                {
                    return builder.ToString();
                }

                builder.Append(" ");
            }
            else if (tens == 1)
            {
                number += 10;
            }

            if (useAnd)
            {
                if (number == 0)
                {
                    return builder.ToString();
                }

                builder.Append(" and ");
            }

            builder.Append(number == 0 
                ? _zeroTokens[(int) zeroMode+1] 
                : _numberTokens[number - 1]);

            return builder.ToString();
        }
    }
}