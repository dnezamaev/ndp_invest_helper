using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Parsers
{
    class FinexParser
    {
        private static Dictionary<string, int> etfSectorsDict =
            new Dictionary<string, int>
            {
                { "Health Care", 1 },
                { "Utilities", 2 },
                { "Communication", 3 },
                { "Consumer Discretionary", 5 },
                { "Consumer Staples", 6 },
                { "Industrials", 7 },
                { "Basic Materials", 9 },
                { "Financials", 12 },
                { "Energy", 13 },
                { "Communication Services", 32 },
                { "Real Estate", 41 },
                { "Information Technology", 128 },
                { "Not Classified", 999 },
                //{ "",  },
            };

        // https://cdn.finex-etf.ru/documents/FinEx_Funds_ICAV_Annual_FS_-_30_Sept_2020.pdf
        // page 92+
        public static string EtfSectors(string input)
        {
            var sb = new StringBuilder();
            var errors = new StringBuilder();
            var regex = new Regex(@"(.*) (\d?\d?\d\.?\d?\d?)%");

            var lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var matches = regex.Matches(line);

                if (matches.Count == 0)
                {
                    errors.AppendFormat("!!! Ничего не найдено в строке: {0}\n", line);
                    continue;
                }

                var match = matches[0];

                if (match.Groups.Count != 3)
                {
                    errors.AppendFormat("!!! Не удалось разобрать строку: {0}\n", line);
                    continue;
                }

                var sectorName = match.Groups[1].Value;

                if (!etfSectorsDict.ContainsKey(sectorName))
                {
                    errors.AppendFormat("!!! Не найден сектор с названием: {0}\n", sectorName);
                    continue;
                }

                var valueString = match.Groups[2].Value;
                var value = decimal.Parse(valueString, NumberStyles.Any, CultureInfo.InvariantCulture);
                var sectorId = etfSectorsDict[sectorName];

                sb.AppendFormat("      <sector key=\"{0}\" value=\"{1:0.00}\" /> <!-- {2} -->\n", 
                    sectorId, value, sectorName);
            }

            if (errors.Length > 0)
            {
                sb.AppendLine();
                sb.AppendLine(errors.ToString());
            }

            return sb.ToString();
        }
    }
}
