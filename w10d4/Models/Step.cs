using System.ComponentModel.DataAnnotations;

namespace w10d4.Models
{
    public class Step
    {
        public int Id { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }
        
        [Required]
        public int Position { get; set; }

        [Required]
        public string Body { get; set; } 

        public int RecipeId { get; set; }

        public string CreatorId { get; set; }
    }
}