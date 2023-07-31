using System.ComponentModel.DataAnnotations;

namespace Opticron.Models
{
    public class Category
    {
        public Int64 Id { get; set; }

        [Display(Name = "Category Name")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required]
        public string Name { get; set; }

        public string Thumbnail { get; set; }
    }
}
