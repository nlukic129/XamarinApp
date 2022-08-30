using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebojsa_60_19.Database
{
    public class BaseTable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
