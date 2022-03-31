using System;
using System.Text.RegularExpressions;

namespace ndp_invest_helper.ReportHandlers
{
    /// <summary>
    /// Справка для госслужащих от ВТБ.
    /// </summary>
    public class VtbOfficerReport : OfficerReport
    {
        public override void ParseFile(string filePath)
        {
            base.ParseFile(filePath);

            var issuerAndSecTypeRegex = new Regex(@"\d+ (.+) (Акция.+)");
            var capitalAndPartRegex = new Regex(@"(.+) 0.\d+%");
            var priceRegex = new Regex(@"(.+) руб.");
            var countRegex = new Regex(@"(.+) шт.");

            var lines = System.IO.File.ReadAllLines(filePath);

            if (lines.Length % 6 != 0)
            {
                ThrowExceptionOnParse
                (
                    "Должно быть по 6 строк на бумагу."
                );
            }

            for (int i = 0; i < lines.Length;)
            {
                // Issuer and share type.
                var issuerAndSecType = lines[i++];
                var matches = issuerAndSecTypeRegex.Matches(issuerAndSecType);

                // Must be only 1 match with 2 groups found:
                // issuer name and share type.
                // Groups[0] is always full match, so Groups.Count == 3.
                if (matches.Count != 1 || matches[0].Groups.Count != 3)
                {
                    ThrowExceptionOnParse
                    (
                        $"Не удалось разобрать строку: {issuerAndSecType}"
                    );
                }

                var shareInfo = new OfficerReportShareInfo();

                var matchGroups = matches[0].Groups;
                shareInfo.Issuer = matchGroups[1].Value;
                shareInfo.ShareTypeString = matchGroups[2].Value;
                shareInfo.TypeOfShare = GetShareType(shareInfo.ShareTypeString);

                // Guess issuer type by searching its type in name.
                // First letter can be upper or lower, so ignore it.
                if (shareInfo.Issuer.Contains("убличное акционерное общество"))
                    shareInfo.TypeOfIssuer = Models.IssuerType.Public;

                shareInfo.AddressFull = lines[i++];

                // Authorized capital of issuer and user part in it.
                var capitalAndPart = lines[i++];
                matches = capitalAndPartRegex.Matches(capitalAndPart);

                // Must be only 1 match with 1 group found: capital. 
                // Part is not required.
                if (matches.Count != 1 || matches[0].Groups.Count != 2)
                {
                    ThrowExceptionOnParse
                    (
                        $"Не удалось разобрать строку: {capitalAndPart}"
                    );
                }

                shareInfo.AuthorizedCapital = matches[0].Groups[1].Value;;

                var price = lines[i++];
                matches = priceRegex.Matches(price);

                // Must be only 1 match with 1 group found: price value. 
                // Currency is not required.
                if (matches.Count != 1 || matches[0].Groups.Count != 2)
                {
                    ThrowExceptionOnParse
                    (
                        $"Не удалось разобрать строку: {price}"
                    );
                }

                shareInfo.Price = matches[0].Groups[1].Value;

                var count = lines[i++];
                matches = countRegex.Matches(count);

                // Must be only 1 match with 1 group found: count value. 
                // Pieces is not required.
                if (matches.Count != 1 || matches[0].Groups.Count != 2)
                {
                    ThrowExceptionOnParse
                    (
                        $"Не удалось разобрать строку: {count}"
                    );
                }

                shareInfo.Count = matches[0].Groups[1].Value;

                i++; // ignore Total

                Shares.Add(shareInfo);
            }
        }

        private void ThrowExceptionOnParse(string whatHappened)
        {
            throw new Exception
            (
                $"Файл {FilePath} неверно сформирован. {whatHappened}"
            );
        }

        private Models.ShareType GetShareType(string text)
        {
            switch (text)
            {
                case "Акция обыкновенная именная":
                    return Models.ShareType.Common;
                case "Акция привилегированная именная":
                    return Models.ShareType.Common;
                default:
                    throw new Exception("Неизвестный тип бумаги в отчете госслужащего.");
            }
        }
    }
}
