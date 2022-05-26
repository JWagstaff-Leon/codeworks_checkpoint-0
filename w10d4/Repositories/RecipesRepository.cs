using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using w10d4.Models;

namespace w10d4.Repositories
{
    public class RecipesRepository
    {
        private readonly IDbConnection _db;

        public RecipesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Recipe> GetAll()
        {
            string sql = @"
            SELECT
            rec.*,
            acc.*
            FROM recipes rec
            JOIN accounts acc ON rec.creatorId = acc.id;
            ";
            return _db.Query<Recipe, Profile, Recipe>(sql, (recipe, account) => {
                recipe.Creator = account;
                return recipe;
            }).ToList();
        }

        internal Recipe GetById(int id)
        {
            string sql = @"
            SELECT
            rec.*,
            acc.*
            FROM recipes rec
            JOIN accounts acc ON rec.creatorId = acc.id
            WHERE rec.id = @id;
            ";
            return _db.Query<Recipe, Profile, Recipe>(sql, (recipe, account) => {
                recipe.Creator = account;
                return recipe;
            }, new { id }).FirstOrDefault();
        }

        internal List<Recipe> GetByCategory(string category)
        {
            string sql = @"
            SELECT
            rec.*,
            acc.*
            FROM recipes rec
            JOIN accounts acc ON rec.creatorId = acc.id
            WHERE rec.category = @category;
            ";
            return _db.Query<Recipe, Profile, Recipe>(sql, (recipe, account) => {
                recipe.Creator = account;
                return recipe;
            }, new { category }).ToList();
        }

        internal List<Recipe> GetByCreatorId(string creatorId)
        {
            string sql = @"
            SELECT
            rec.*,
            acc.*
            FROM recipes rec
            JOIN accounts acc ON rec.creatorId = acc.id
            WHERE rec.creatorId = @creatorId;
            ";
            return _db.Query<Recipe, Profile, Recipe>(sql, (recipe, account) => {
                recipe.Creator = account;
                return recipe;
            }, new { creatorId }).ToList();
        }


        internal Recipe Create(Recipe data)
        {
            string sql = @"
            INSERT INTO recipes
            (picture, title, subtitle, category, creatorId)
            VALUES
            (@Picture, @Title, @Subtitle, @Category, @CreatorId);
            SELECT LAST_INSERT_ID();";
            data.Id = _db.ExecuteScalar<int>(sql, data);
            data.CreatedAt = System.DateTime.Now;
            data.UpdatedAt = System.DateTime.Now;
            return data;
        }

        internal Recipe Edit(Recipe update)
        {
            string sql = @"
            UPDATE recipes
            SET
                picture = @Picture,
                title = @Title,
                subtitle = @Subtitle,
                category = @Category
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
            FROM recipes
            WHERE id = @id
            LIMIT 1;
            ";

            _db.Execute(sql, new { id });
        }
    }
}