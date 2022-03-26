BEGIN TRANSACTION;
DROP TABLE IF EXISTS "Securities_Countries_Link";
CREATE TABLE IF NOT EXISTS "Securities_Countries_Link" (
	"SecurityID"	INTEGER NOT NULL,
	"CountryCode"	TEXT NOT NULL,
	"Part"	REAL NOT NULL,
	PRIMARY KEY("SecurityID","CountryCode"),
	FOREIGN KEY("CountryCode") REFERENCES "Countries"("Code"),
	FOREIGN KEY("SecurityID") REFERENCES "Securities"("ID")
);
DROP TABLE IF EXISTS "Securities_EconomySectors_Link";
CREATE TABLE IF NOT EXISTS "Securities_EconomySectors_Link" (
	"SecurityID"	INTEGER NOT NULL,
	"SectorID"	INTEGER NOT NULL,
	"Part"	REAL NOT NULL,
	PRIMARY KEY("SecurityID","SectorID"),
	FOREIGN KEY("SectorID") REFERENCES "EconomySectors"("ID"),
	FOREIGN KEY("SecurityID") REFERENCES "Securities"("ID")
);
DROP TABLE IF EXISTS "Issuers_EconomySectors_Link";
CREATE TABLE IF NOT EXISTS "Issuers_EconomySectors_Link" (
	"IssuerID"	INTEGER NOT NULL,
	"SectorID"	INTEGER NOT NULL,
	"Part"	REAL NOT NULL,
	PRIMARY KEY("IssuerID","SectorID"),
	FOREIGN KEY("SectorID") REFERENCES "EconomySectors"("ID"),
	FOREIGN KEY("IssuerID") REFERENCES "Issuers"("ID")
);
DROP TABLE IF EXISTS "Issuers_Countries_Link";
CREATE TABLE IF NOT EXISTS "Issuers_Countries_Link" (
	"IssuerID"	INTEGER NOT NULL,
	"CountryCode"	TEXT NOT NULL,
	"Part"	REAL NOT NULL,
	PRIMARY KEY("IssuerID","CountryCode"),
	FOREIGN KEY("IssuerID") REFERENCES "Issuers"("ID"),
	FOREIGN KEY("CountryCode") REFERENCES "Countries"("Code")
);
DROP TABLE IF EXISTS "Issuers_Currencies_Link";
CREATE TABLE IF NOT EXISTS "Issuers_Currencies_Link" (
	"IssuerID"	INTEGER NOT NULL,
	"CurrencyCode"	TEXT NOT NULL,
	"Part"	REAL NOT NULL,
	PRIMARY KEY("IssuerID","CurrencyCode"),
	FOREIGN KEY("IssuerID") REFERENCES "Issuers"("ID")
	FOREIGN KEY("CurrencyCode") REFERENCES "Issuers"("ID")
);
DROP TABLE IF EXISTS "Funds_Assets_Link";
CREATE TABLE IF NOT EXISTS "Funds_Assets_Link" (
	"FundSecurityID"	INTEGER NOT NULL,
	"AssetTypeID"	INTEGER NOT NULL,
	"Part"	REAL NOT NULL,
	PRIMARY KEY("FundSecurityID","AssetTypeID"),
	FOREIGN KEY("FundSecurityID") REFERENCES "Securities"("ID"),
	FOREIGN KEY("AssetTypeID") REFERENCES "Securities"("ID")
);
DROP TABLE IF EXISTS "FundSecurities";
CREATE TABLE IF NOT EXISTS "FundSecurities" (
	"FundSecurityID"	INTEGER NOT NULL,
	"InnerSecurityID"	INTEGER NOT NULL,
	"Part"	REAL NOT NULL,
	PRIMARY KEY("InnerSecurityID","FundSecurityID"),
	FOREIGN KEY("FundSecurityID") REFERENCES "Securities"("ID")
	FOREIGN KEY("InnerSecurityID") REFERENCES "Securities"("ID")
);
DROP TABLE IF EXISTS "Securities_Currencies_Link";
CREATE TABLE IF NOT EXISTS "Securities_Currencies_Link" (
	"SecurityID"	INTEGER NOT NULL,
	"CurrencyCode"	TEXT NOT NULL,
	"Part"	REAL NOT NULL,
	PRIMARY KEY("SecurityID","CurrencyCode"),
	FOREIGN KEY("SecurityID") REFERENCES "Securities"("ID"),
	FOREIGN KEY("CurrencyCode") REFERENCES "Currencies"("Code")
);
DROP TABLE IF EXISTS "Securities";
CREATE TABLE IF NOT EXISTS "Securities" (
	"ID"	INTEGER NOT NULL,
	"ISIN"	TEXT,
	"Ticker"	TEXT,
	"SecurityType"	INTEGER NOT NULL,
	"IssuerID"	INTEGER,
	"NameRus"	TEXT,
	PRIMARY KEY("ID"),
	FOREIGN KEY("IssuerID") REFERENCES "Issuers"("ID")
);
DROP TABLE IF EXISTS "Countries";
CREATE TABLE IF NOT EXISTS "Countries" (
	"Code"	TEXT NOT NULL,
	"NameRus"	TEXT NOT NULL,
	"RegionID"	INTEGER,
	"DevelopmentLevel"	INTEGER,
	PRIMARY KEY("Code"),
	FOREIGN KEY("RegionID") REFERENCES "CountryRegions"("ID"),
	FOREIGN KEY("DevelopmentLevel") REFERENCES "CountryDevelopmentLevels"("ID")
);
DROP TABLE IF EXISTS "CountryDevelopmentLevels";
CREATE TABLE IF NOT EXISTS "CountryDevelopmentLevels" (
	"ID"	INTEGER NOT NULL,
	"NameRus"	TEXT NOT NULL,
	PRIMARY KEY("ID")
);
DROP TABLE IF EXISTS "CountryRegions";
CREATE TABLE IF NOT EXISTS "CountryRegions" (
	"ID"	INTEGER NOT NULL,
	"NameRus"	TEXT NOT NULL,
	PRIMARY KEY("ID")
);
DROP TABLE IF EXISTS "Currencies";
CREATE TABLE IF NOT EXISTS "Currencies" (
	"Code"	TEXT NOT NULL,
	"NameEng"	TEXT NOT NULL,
	"RateToRub"	REAL,
	PRIMARY KEY("Code")
);
DROP TABLE IF EXISTS "Issuers";
CREATE TABLE IF NOT EXISTS "Issuers" (
	"ID"	INTEGER NOT NULL,
	"NameRus"	TEXT NOT NULL,
	PRIMARY KEY("ID")
);
DROP TABLE IF EXISTS "EconomySectors";
CREATE TABLE IF NOT EXISTS "EconomySectors" (
	"ID"	INTEGER NOT NULL,
	"Level"	INTEGER NOT NULL,
	"NameRus"	TEXT NOT NULL,
	"ParentID"	INTEGER,
	PRIMARY KEY("ID"),
	FOREIGN KEY("ParentID") REFERENCES "EconomySectors"("ID")
);
DROP TABLE IF EXISTS "Tickers";
CREATE TABLE IF NOT EXISTS "Tickers" (
	"Ticker"	TEXT NOT NULL,
	"Market"	TEXT NOT NULL,
	"SecurityID"	INTEGER NOT NULL,
	PRIMARY KEY("Ticker","Market"),
	FOREIGN KEY("SecurityID") REFERENCES "Securities"("ID")
);
DROP TABLE IF EXISTS "AssetTypes";
CREATE TABLE IF NOT EXISTS "AssetTypes" (
	"ID"	INTEGER NOT NULL,
	"NameEng"	TEXT NOT NULL,
	"NameRus"	TEXT NOT NULL,
	PRIMARY KEY("ID")
);

INSERT INTO "AssetTypes" VALUES (1,'Share','Акция');
INSERT INTO "AssetTypes" VALUES (2,'Bond','Облигация');
INSERT INTO "AssetTypes" VALUES (3,'ETF','Фонд');
INSERT INTO "AssetTypes" VALUES (4,'Gold','Золото');
INSERT INTO "AssetTypes" VALUES (5,'Cash','Деньги');

COMMIT;
