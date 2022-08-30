using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Nebojsa_60_19.Database
{
    public class Db
    {
        private SQLiteConnection connection;

        public Db()
        {
            var dbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbFile = Path.Combine(dbPath, "Nebojsa_60_19.db3");

            connection = new SQLiteConnection(dbFile);
            connection.CreateTable<TypesOfInstruments>();
            connection.CreateTable<Instruments>();

        }

        public SQLiteConnection Connection { get => connection;  }
    }
}
