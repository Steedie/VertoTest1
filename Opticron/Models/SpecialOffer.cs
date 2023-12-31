﻿using System.ComponentModel.DataAnnotations;

namespace Opticron.Models
{
    public class SpecialOffer
    {
        public Int64 Id { get; set; }
        [Required]
        public string Heading { get; set; }
        public string Subheading { get; set; }
        public string Thumbnail { get; set; }
    }
}
