using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Parsers
{
    class VtbParser
    {
        private static Dictionary<string, int> etfSectorsDict =
            new Dictionary<string, int>
            {
                { "Здравоохранение", 1 },
                { "Электроэнергетика", 2 },
                { "Communication", 3 },
                { "Потребительский сектор - ППН", 5 },
                { "Потребительский сектор - прочее", 6 },
                { "Производство и сфера обслуживания", 7 },
                { "Сырье и материалы", 9 },
                { "Технологии", 10 },
                { "Финансовый сектор", 12 },
                { "Энергетика", 13 },
                { "Связь и телекоммуникации", 32 },
                { "Девелопмент / управление недвижимостью", 38 },
                { "Real Estate", 41 },
                { "ИТ", 128 },
                { "Other", 999 },
                //{ "",  },
            };

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
