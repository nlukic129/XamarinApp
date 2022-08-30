using System;
using System.Collections.Generic;
using System.Text;

namespace Nebojsa_60_19.Models
{
    public class GenresDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SongsCount { get; set; }
        public int AlbumsCount { get; set; }
        public string Description { get; set; }

    }
}
