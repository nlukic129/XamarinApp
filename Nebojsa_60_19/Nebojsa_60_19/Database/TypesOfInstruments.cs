using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebojsa_60_19.Database
{
    [Table("TypesOfInstruments")]
    public class TypesOfInstruments : BaseTable
    {
        [NotNull, Unique]
        public string Name { get; set; }
    }
}
