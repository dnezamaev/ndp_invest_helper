using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Xml.Linq;

namespace ndp_invest_helper
{
    class Utils
    {
        /// <summary>
        /// Разбираем сложный параметр в XML файле.
        /// Он может быть аттрибутом с текстовым значением.
        /// Тогда в список destination заносим его значение как ключ и 1 как значение. 
        /// Если аттрибута нет, то ищем вложенные тэги (подтэги) с тем же именем, что и аттрибут.
        /// У каждого подтэга ищем поля code, value и заносим их в список.
        /// </summary>
        /// <param name="xTag">Тэг для разбора.</param>
        /// <param name="attributeName">Название аттрибута и подтэгов.</param>
        public static Dictionary<string, decimal> HandleComplexStringXmlAttribute(
            XElement xTag,
            string attributeName, 
            string subTagKey = "key")
        {
            var result = new Dictionary<string, decimal>();

            // Смотрим есть ли аттрибут.
            var xAttribute = xTag.Attribute(attributeName);
            if (xAttribute != null) // Если есть, то добавляем его как 100%.
            {
                result[xAttribute.Value] = 1;
            }
            else // Нет аттрибута, смотрим вложенные тэги.
            {
                foreach (var xSubTag in xTag.Descendants(attributeName))
                {
                    result[xSubTag.Attribute(subTagKey).Value] =
                        decimal.Parse(
                         xSubTag.Attribute("value").Value,
                            NumberStyles.Any, CultureInfo.InvariantCulture
                            ) / 100;
                }
            }

            return result;
        }

        public static Dictionary<int, decimal> HandleComplexIntXmlAttribute(
            XElement xTag,
            string attributeName, 
            string subTagKey = "key")
        {
            var result = new Dictionary<int, decimal>();

            // Смотрим есть ли аттрибут.
            var xAttribute = xTag.Attribute(attributeName);
            if (xAttribute != null) // Если есть, то добавляем его как 100%.
            {
                result[int.Parse(xAttribute.Value)] = 1;
            }
            else // Нет аттрибута, смотрим вложенные тэги.
            {
                foreach (var xSubTag in xTag.Descendants(attributeName))
                {
                    result[int.Parse(xSubTag.Attribute(subTagKey).Value)] =
                        decimal.Parse(
                         xSubTag.Attribute("value").Value,
                            NumberStyles.Any, CultureInfo.InvariantCulture
                            ) / 100;
                }
            }

            return result;
        }

        public static void HandleSectorAttribute(
            XElement xTag,
            Dictionary<Sector, decimal> destination,
            SectorsManager sectorsManager)
        {
            var sectors = Utils.HandleComplexStringXmlAttribute(xTag, "sector");

            foreach (var item in sectors)
            {
                var sector = sectorsManager.Sectors.Find(x => x.Id == item.Key);

                if (sector == null)
                    continue;

                destination[sector] = item.Value;
            }

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
