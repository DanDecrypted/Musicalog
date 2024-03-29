﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Musicalog.Data.Models
{
    public class Album : Entity
    {
        [ForeignKey(nameof(Artist))]
        public Guid ArtistId { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        public AlbumType Type { get; set; }
        public Artist Artist { get; set; }
        public int Stock { get; set; }
    }
}
