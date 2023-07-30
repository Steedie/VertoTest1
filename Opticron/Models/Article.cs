using System.ComponentModel.DataAnnotations;

namespace Opticron.Models
{
    public class Article
    {
        public Int64 Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "Call To Action")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CallToAction { get; set; }

        public string Thumbnail { get; set; }
    }
}
