using System.ComponentModel.DataAnnotations;

namespace Opticron.Models
{
    public class SpecialOffer
    {
        public Int64 Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Thumbnail { get; set; }
    }
}
