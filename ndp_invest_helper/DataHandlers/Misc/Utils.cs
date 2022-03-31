using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Xml.Linq;

namespace ndp_invest_helper
{
    public class Utils
    {
        /// <summary>
        /// Разбираем сложный параметр в XML файле.
        /// Он может быть XML аттрибутом с текстовым значением или вложенными тегами (подтегами).
        /// В первую очередь проверяются вложенные теги с именем attributeName, 
        /// они группируются по аттрибуту subTagKey, 
        /// значения аттрибутов subTagValue суммируются в группе.
        /// Если вложенных тегов нет, тогда ищем проверяется аттрибут attributeName, 
        /// его значение используется как ключ и 1 как значение. 
        /// </summary>
        /// <param name="destination">Куда писать результат.</param>
        /// <param name="xTag">Тэг для разбора.</param>
        /// <param name="attributeName">Название аттрибута и подтегов.</param>
        /// <param name="fillTo100">Дополнять до 100%, если не хватает.</param>
        /// <param name="keyForUnknown">Ключ для незаполненной части.</param>
        /// <param name="subTagKey">Имя аттрибута с ключом у подтегов.</param>
        /// <param name="subTagValue">Имя аттрибута со значением у подтегов.</param>
        public static void HandleComplexStringXmlAttribute(
            Dictionary<string, decimal> destination,
            XElement xTag,
            string attributeName,
            bool fillTo100 = true,
            string keyForUnknown = "???",
            string subTagKey = "key",
            string subTagValue = "value"
            )
        {
            // Пример:
            // <security only_here='key' both='key1'>
            //     <both key='key2' value='20' />
            //     <both key='key3' value='30' />
            // </security>
            //
            // В результате для вызова
            //   с attributeName равным "only_here" будет одна запись { "key", 1 };
            //
            // В результате для вызова
            //   с attributeName равным "both" будет три записи, т.к. у подтегов приоритет:
            //     { "key2", 0.2 } - из первого вложенного тега
            //     { "key3", 0.3} - из второго вложенного тега
            //     { "???", 0.5} - дополнение до 100%

            var children = xTag.Elements(attributeName).ToArray();

            if (children.Length == 0) // Нет вложенных тегов.
            {
                // Смотрим есть ли аттрибут.
                var xAttribute = xTag.Attribute(attributeName);

                // Если есть, то добавляем его как 100%.
                if (xAttribute != null) 
                    destination[xAttribute.Value] = 1;

                return;
            }

            decimal partsSum = 0; // Общая сумма по найденным долям.

            // Есть вложенные теги, раз попали сюда.
            foreach (var xSubTag in children)
            {
                var part = decimal.Parse(
                     xSubTag.Attribute(subTagValue).Value,
                        NumberStyles.Any, CultureInfo.InvariantCulture
                        ) / 100;

                destination[xSubTag.Attribute(subTagKey).Value] = part;
                partsSum += part;
            }

            if (fillTo100 && partsSum < 1M) // Сумма найденных долей оказалась меньше 100%.
            {
                // Возможно, такой ключ уже есть, тогда надо учесть его текущее значение.
                var unknownPart = destination.GetValueOrDefault(keyForUnknown, 0);
                unknownPart += 1 - partsSum;
                destination[keyForUnknown] = unknownPart;
            }
        }

        public static Dictionary<string, decimal> HandleComplexStringXmlAttribute(
            XElement xTag,
            string attributeName, 
            bool fillTo100 = true,
            string keyForUnknown = "???",
            string subTagKey = "key",
            string subTagValue = "value"
            )
        {
            var result = new Dictionary<string, decimal>();

            HandleComplexStringXmlAttribute(
                result, xTag, attributeName, fillTo100, keyForUnknown,
                subTagKey, subTagValue);

            return result;
        }

        public static void HandleSectorAttribute(
            Dictionary<Sector, decimal> destination,
            XElement xTag,
            string unknownSector)
        {
            var sectors = HandleComplexStringXmlAttribute(xTag, "sector", true, unknownSector);

            foreach (var item in sectors)
            {
                var sector = SectorsManager.Sectors.Find(x => x.Id == item.Key);

                if (sector == null)
                    throw new ArgumentException(string.Format(
                        "Обнаружен неизвестный сектор {0} в теге {1}",
                        item.Key, xTag.ToString()));

                destination[sector] = item.Value;
            }

        }

        public static Dictionary<Sector, decimal> HandleSectorAttribute(
            XElement xTag,
            string unknownSector)
        {
            var result = new Dictionary<Sector, decimal>();
            var sectors = HandleComplexStringXmlAttribute(xTag, "sector", true, unknownSector);

            foreach (var item in sectors)
            {
                var sector = SectorsManager.Sectors.Find(x => x.Id == item.Key);

                if (sector == null)
                    throw new ArgumentException(string.Format(
                        "Обнаружен неизвестный сектор {0} в теге {1}",
                        item.Key, xTag.ToString()));

                result[sector] = item.Value;
            }

            return result;
        }

        public static string SelectFileWithDialog (
            string filter = "Text files|*.txt|All files|*.*"
        )
        {
            var file_dialog = new System.Windows.Forms.OpenFileDialog();
            file_dialog.Filter = filter;

            if (file_dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return null;

            return file_dialog.FileName;
        }

    }

    /// <summary>
    /// Полезные вещи из новых версий фреймворка.
    /// </summary>
    public static class NewerDotNetExtensions
    {
        public static StringBuilder AppendJoin<T>(this StringBuilder stringBuilder,
            string separator, IEnumerable<T> values)
        {
            if (separator == null)
                separator = string.Empty;

            var list = new List<T>(values);

            if (list.Count == 0)
                return stringBuilder;

            for (int i = 0; i < list.Count - 1; i++)
            {
                stringBuilder.Append(list[i]);
                stringBuilder.Append(separator);
            }

            stringBuilder.Append(list.Last());

            return stringBuilder;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary, 
            TKey key, TValue defaultValue)
        {
            return dictionary.TryGetValue(key, out TValue value) ? value : defaultValue;
        }

        public static TValue GetValueOrSetDefault<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary, 
            TKey key, TValue defaultValue)
        {
            var resultValue = dictionary.GetValueOrDefault(key, defaultValue);
            dictionary[key] = resultValue;
            return resultValue;
        }

        public static HashSet<TSource> ToHashSet<TSource>(
            this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer = null)
        {
            if (comparer == null)
                return new HashSet<TSource>(source);
            else 
                return new HashSet<TSource>(source, comparer);
        }
    }
}
