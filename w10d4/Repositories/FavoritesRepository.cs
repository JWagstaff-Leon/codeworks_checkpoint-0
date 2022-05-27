using System.Data;
using w10d4.Models;
using System.Collections.Generic;
using Dapper;
using System.Linq;

namespace w10d4.Repositories
{
    public class FavoritesRepository
    {
        private readonly IDbConnection _db;

        public FavoritesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<RecipeFavoriteVM> GetByAccountId(string id)
        {
            string sql = @"
            SELECT
            rec.*,
            fav.id AS FavoriteId,
            acc.*
            FROM favorites fav
            JOIN recipes rec ON fav.recipeId = rec.id
            JOIN accounts acc ON rec.creatorId = acc.id
            WHERE fav.accountId = @id;
            ";
            return _db.Query<RecipeFavoriteVM, Account, RecipeFavoriteVM>(sql, (recipeFavorite, account) => {
                recipeFavorite.Creator = account;
                return recipeFavorite;
            }, new { id }).ToList();
        }

        internal List<AccountFavoriteVM> GetByRecipeId(int id)
        {
            string sql = @"
            SELECT
            acc.*,
            fav.id AS FavoriteId
            FROM favorites fav
            JOIN accounts acc ON fav.accountId = acc.id
            WHERE fav.recipeId = @id;
            ";
            return _db.Query<AccountFavoriteVM>(sql, new { id }).ToList();
        }
    }
}