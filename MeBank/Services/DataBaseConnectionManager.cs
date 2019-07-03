using System;
using System.IO;
using SQLite;

namespace MeBank.Services
{
    public class DataBaseConnectionManager 
    {
        public SQLiteAsyncConnection Connection { get; }
        public string PathToDBFile { get; }

        public DataBaseConnectionManager()
        {
            var dbFileName = "MeBankSQLite.db3";
            PathToDBFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbFileName);
            Connection =  new SQLiteAsyncConnection(PathToDBFile);
        }
    }
}