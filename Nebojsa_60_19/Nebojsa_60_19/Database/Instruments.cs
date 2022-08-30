using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebojsa_60_19.Database
{
    [Table("Instruments")]
    public class Instruments : BaseTable
    {
        [Unique, NotNull]
        public string Name { get; set; }
        [NotNull]
        public string Year { get; set; }
        [NotNull]
        public string Country { get; set; }
        [NotNull]
        public string Description { get; set; }
        [NotNull]
        public int TypeId { get; set; }
    }
}
