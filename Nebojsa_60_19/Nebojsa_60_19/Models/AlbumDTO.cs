using System;
using System.Collections.Generic;
using System.Text;

namespace Nebojsa_60_19.Models
{
    public class AlbumDTO
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public string Artist { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
