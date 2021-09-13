using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Parsers
{
    class Program
    {
        static void Main(string[] args)
        {
            // Чтобы разделителем целых и дробных были точки, а не запятые.
            System.Threading.Thread.CurrentThread.CurrentCulture =
                System.Globalization.CultureInfo.InvariantCulture;

            HandleFile(@"data\vtbe_sectors.txt", VtbParser.EtfSectors);
            HandleFile(@"data\vtba_sectors.txt", VtbParser.EtfSectors);
            HandleFile(@"data\fxus_sectors.txt", FinexParser.EtfSectors);
            HandleFile(@"data\fxdm_sectors.txt", FinexParser.EtfSectors);
            HandleFile(@"data\fxcn_sectors.txt", FinexParser.EtfSectors);
            HandleFile(@"data\fxde_sectors.txt", FinexParser.EtfSectors);
            HandleFile(@"data\FXRL_sectors.txt", FinexParser.EtfSectors);
        }

        static void HandleFile(string filePath, Func<string, string> handler)
        {
            var output = handler(File.ReadAllText(filePath));
            var outputFilePath = filePath + ".output";
            File.WriteAllText(outputFilePath, output);
        }
    }
}
