using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ndp_invest_helper.Models;

namespace ndp_invest_helper
{
    class TinkoffReport : Report
    {
        private SheetData sheetData;
        private List<Row> rows;

        public override void ParseFile(string filePath)
        {
            FilePath = filePath;

            using (var spreadsheetDocument =
                SpreadsheetDocument.Open(filePath, false))
            {
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
                rows = sheetData.Elements<Row>().ToList();

                var securityDescriptionTable = FindTable(
                    "4.1 Информация о ценных бумагах",
                    "4.2 Информация об инструментах, не квалифицированных в качестве ценной бумаги");

                // Словарь с ключом - код актива, значением - ISIN.
                var securityDescritionDict = securityDescriptionTable.Select(
                    x => new KeyValuePair<string, string>(x[1].InnerText, x[2].InnerText)
                    ).ToDictionary(x => x.Key, x => x.Value);

                var portfolioTable = FindTable(
                    "3.1 Движение по ценным бумагам инвестора",
                    "3.2 Движение по производным финансовым инструментам");

                foreach (var cells in portfolioTable)
                {
                    var secIsin = securityDescritionDict[cells[1].InnerText];
                    var secCount = UInt64.Parse(cells[7].InnerText);
                    var secPrice = decimal.Parse(cells[8].InnerText);
                    var secCurrency = cells[9].InnerText;

                    secPrice *= CurrenciesManager.RatesToRub[secCurrency];

                    var securityInfo = new SecurityInfo
                    {
                        Price = secPrice,
                        Count = secCount
                    };

                    Security security;
                    if (SecuritiesManager.SecuritiesByIsin.TryGetValue(secIsin, out security))
                    {
                        Securities[security] = securityInfo;
                    }
                    else // такой бумаги нет в базе, добавляем в неизвестные
                    {
                        UnknownSecurities.Add(new Security { Isin = secIsin });
                    }
                }
            }
        }

        /// <summary>
        /// Найти в документе таблицу с заголовком header.
        /// </summary>
        /// <param name="header">Заголовок таблицы.</param>
        /// <param name="end">Критерий конца таблицы. 
        /// Собранный воедино текст строки, следующей за таблицей.</param>
        /// <param name="skipColumnHeaders">Сколько строк пропустить после заголовка?
        /// Обычно следующая строка - с заголовками столбцов.</param>
        /// <returns>Список строк после заголовка, не включая end.</returns>
        private List<Cell[]> FindTable(string header, string end, int skipFirstRows = 1)
        {
            var result = new List<Cell[]>();

            // Находим нужный раздел в отчете.
            var headerRowIndex = rows.FindIndex(
                row => row.InnerText == header
                );

            if (headerRowIndex == -1)
                throw new ArgumentException(
                    $"Не удалось разобрать отчет {FilePath}." +
                    $" Не найден раздел {header}");

            // Следующая строка после названия раздела - обычно заголовок таблицы.
            // Потом - данные, которые нам нужны.
            for (int i = headerRowIndex + 1 + skipFirstRows; i < rows.Count; i++)
            {
                var row = rows[i];

                // Начало следующего раздела или пустота, выходим.
                if (row.InnerText == end || row.InnerText == "")
                    break;

                // Непустые ячейки.
                var cells = row.Elements<Cell>().Where(
                    cell => !string.IsNullOrEmpty(cell.InnerText)).ToArray();

                // Пропускаем номера страниц, например: 2 из 4.
                if (cells.Length == 2 
                    && cells[0].InnerText.EndsWith(" из ")
                    )
                    continue;

                result.Add(cells);
            }

            return result;
        }
    }
}
