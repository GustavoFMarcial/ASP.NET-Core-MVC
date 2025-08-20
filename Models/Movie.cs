using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCMovie.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string? Title { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Release Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [StringLength(20)]
        public string? Genre { get; set; }

        [Required]
        [Range(1, 1000)]
        public decimal Price { get; set; }
    }
}