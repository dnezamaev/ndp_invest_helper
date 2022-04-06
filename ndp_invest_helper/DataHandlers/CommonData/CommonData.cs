using System;
using System.IO;
using System.Text;

namespace ndp_invest_helper.DataHandlers
{
    /// <summary>
    /// Global common data.
    /// </summary>
    public static class CommonData
    {
        public static AssetTypesManager Assets { get; }
            = new AssetTypesManager();

        public static CountriesManager Countries { get; }
            = new CountriesManager();

        public static CurrenciesManager Currencies { get; }
            = new CurrenciesManager();

        public static SectorsManager Sectors { get; }
            = new SectorsManager();
        
        private static StringBuilder log = new StringBuilder();

        public static DiversityManager GetManagerForItem(DiversityItem item)
        {
            switch (item)
            {
                case AssetType _:
                    return Assets;

                case Currency _:
                    return Currencies;

                case Country _:
                    return Countries;

                case EconomySector _:
                    return Sectors;

                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Load all common data: securities, currencies, counties, sectors.
        /// Source is selected on Settings.
        /// </summary>
        public static void Load()
        {
            switch (Settings.CommonDataSource)
            {
                case CommonDataSources.SqliteDb:
                    LoadFromSqlite();
                    break;
                case CommonDataSources.XmlFiles:
                    LoadFromXml();
                    break;
                default:
                    throw new NotImplementedException
                        (
                        $"Неизвестное значение настройки {nameof(Settings.CommonDataSource)} " +
                        $"= {Settings.CommonDataSource}"
                        );
            }
        }

        private static void LoadFromXml()
        {
            Currencies.LoadFromXmlFile(Settings.CurrenciesXmlFilePath);
            Countries.LoadFromXmlFile(Settings.CountriesXmlFilePath);
            Sectors.LoadFromXmlFile(Settings.SectorsXmlFilePath);
            SecuritiesManager.LoadFromXmlFile(Settings.SecuritiesXmlFilePath);
        }

        private static void InitDatabase()
        {
            DatabaseManager.Init(
                connectionStringName: "DapperSQLite",
                dbCreateScript: File.ReadAllText(Settings.SqliteDatabaseCreateScriptFilePath)
                );
        }

        private static void LoadFromSqlite()
        {
            // Check if database exists. If no, create it from XML.
            var builder = new System.Data.SqlClient.SqlConnectionStringBuilder(
                DatabaseManager.ConnectionString);

            if (!File.Exists(builder.DataSource))
            {
                LoadFromXml();
                XmlToSqlite();
            }

            InitDatabase();

            Currencies.LoadFromDatabase();
            Countries.LoadFromDatabase();
            Sectors.LoadFromDatabase();

            SecuritiesManager.LoadFromDatabase();
        }

        public static void XmlToSqlite()
        {
            InitDatabase();
            DatabaseManager.CreateDatabase();
            DatabaseManager.Import();
        }

        public static void LogAddText(string text)
        {
            if (Settings.WriteLog)
            {
                log.AppendLine(text);
                log.AppendLine();
            }
        }

        public static void LogSave()
        {
            if (Settings.WriteLog)
            {
                File.WriteAllText(Settings.LogFilePath, log.ToString());
            }
        }
    }
}
