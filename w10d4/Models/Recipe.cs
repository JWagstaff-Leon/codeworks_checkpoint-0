using System.ComponentModel.DataAnnotations;

namespace w10d4.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }

        [Required]
        [Url]
        public string Picture { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string Subtitle { get; set; }

        [Required]
        public string Category { get; set; }

        public string CreatorId { get; set; }

        public Profile Creator { get; set; }
    }

    public class RecipeFavoriteVM : Recipe
    {

    }
}