using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using w10d4.Models;

namespace w10d4.Repositories
{
    public class StepsRepository
    {
        private readonly IDbConnection _db;

        public StepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Step GetById(int id)
        {
            string sql = @"
            SELECT
            ste.*,
            rec.creatorId AS CreatorId
            FROM steps ste
            JOIN recipes rec ON ste.recipeId = rec.Id
            WHERE ste.id = @id;
            ";
            return _db.Query<Step>(sql, new { id }).FirstOrDefault();
        }

        internal List<Step> GetByRecipeId(int id)
        {
            string sql = @"
            SELECT
            ste.*,
            rec.creatorId AS CreatorId
            FROM steps ste
            JOIN recipes rec ON ste.recipeId = rec.id
            WHERE ste.recipeId = @id;
            ";
            return _db.Query<Step>(sql, new { id }).ToList();
        }

        internal Step Create(Step data)
        {
            string sql = @"
            INSERT
            INTO steps
            (position, body, recipeId)
            VALUES
            (@Position, @Body, @recipeId);
            SELECT LAST_INSERT_ID();
            ";
            data.Id = _db.ExecuteScalar<int>(sql, data);
            data.CreatedAt = System.DateTime.Now;
            data.UpdatedAt = System.DateTime.Now;
            return data;
        }

        internal Step Edit(Step update)
        {
            string sql = @"
            UPDATE steps
            SET
                position = @Position,
                body = @Body
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
            FROM steps
            WHERE id = @id
            LIMIT 1;
            ";
            _db.Execute(sql, new { id });
        }
    }
}