using System.ComponentModel.DataAnnotations;

namespace w10d4.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }

        [Required]
        public string Quantity { get; set; }

        [Required]
        public string Name { get; set; }

        public int RecipeId { get; set; }
    }
}