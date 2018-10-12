using install.Interfaces.QueryConfigurator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.QueryConfigurator
{
    class MsSqlQueryConfigurator : QueryConfiguratorInterface
    {
        public string addAdmin(string login, string password, string sult)
        {
            return "INSERT INTO UST VALUES('" + login + "', '" + password +
                "', '" + sult + "' ,1)";
        }

        public string checkUstTable()
        {
            return "SELECT 1 FROM UST";
        }

        public string createTableHistory()
        {
            return "CREATE TABLE History(ConnectionID bigint NOT NULL PRIMARY " +
                "KEY IDENTITY(0,1),UserID int FOREIGN KEY REFERENCES Users(UserID)," +
                "SoftwareID int FOREIGN KEY REFERENCES Software(SoftwareID),DateIN " + 
                "date,DateExit date,TimeIN time,TimeExit time)";
        }

        public string createTableSoftware()
        {
            return "CREATE TABLE Software(SoftwareID int NOT NULL PRIMARY KEY " +
                "IDENTITY(0,1),VendorID int FOREIGN KEY REFERENCES Vendor" +
                "(VendorID),Name char(40),NumberOfPurchased int NOT NULL,Code " + 
                "char(30) NOT NULL,AmountOfInvestments float NOT NULL)";
        }

        public string createTableUsers()
        {
            return "CREATE TABLE Users(UserID int NOT NULL PRIMARY KEY IDENTITY" + 
                "(0,1),Name char(30) NOT NULL,Host char(50) NOT NULL)";
        }

        public string createTableUST()
        {
            return "CREATE TABLE UST(Login char(30) NOT NULL PRIMARY KEY," + 
                "Password char(70) NOT NULL,Sult char(25) NOT NULL,Status int NOT NULL)";
        }

        public string createTableVendor()
        {
            return "CREATE TABLE Vendor(VendorID int NOT NULL PRIMARY KEY " + 
                "IDENTITY(0,1),Name char(40) NOT NULL)";
        }
    }
}
