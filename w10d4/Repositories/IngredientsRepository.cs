using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using w10d4.Models;

namespace w10d4.Repositories
{
    public class IngredientsRepository
    {
        private readonly IDbConnection _db;

        public IngredientsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Ingredient> GetByRecipeId(int id)
        {
            string sql = @"
            SELECT *
            FROM ingredients
            WHERE recipeId = @id;
            ";
            return _db.Query<Ingredient>(sql, new { id }).ToList();
        }

        internal Ingredient Create(Ingredient data)
        {
            string sql = @"
            INSERT
            INTO ingredients
            (quantity, name, recipeId)
            VALUES
            (@Quantity, @Name, @RecipeId);
            SELECT LAST_INSERT_ID();
            ";
            data.Id = _db.ExecuteScalar<int>(sql, data);
            data.CreatedAt = System.DateTime.Now;
            data.UpdatedAt = System.DateTime.Now;
            return data;
        }

        internal Ingredient Edit(Ingredient update)
        {
            string sql = @"
            UPDATE ingredients
            SET
                quantity = @Quantity,
                name = @Name
            WHERE id = @Id;
            ";
            _db.Execute(sql, update);
            update.UpdatedAt = System.DateTime.Now;
            return update;
        }

        internal void Remove(int id)
        {
            string sql = @"
            DELETE
            FROM ingredients
            WHERE id = @id
            LIMIT 1;
            ";
            _db.Execute(sql, new { id });
        }
    }
}