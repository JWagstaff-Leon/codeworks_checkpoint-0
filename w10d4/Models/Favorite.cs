namespace w10d4.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }

        public string AccountId { get; set; }
        public int RecipeId { get; set; }
    }
}