using Dapper;
using System.Threading.Tasks;

namespace Futbin.SQL
{
    public class TablesIsExist
    {
        public async Task<bool> TableExistsAsync(string tableName)
        {
            using (var database = Context.ConnectToSQL)
            {
                var tableExistsQuery = $"SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'";
                var tableExists = await database.QueryFirstOrDefaultAsync<int>(tableExistsQuery);

                return tableExists == 1;
            }
        }

        public async Task<bool> PlayersAsync() => await TableExistsAsync("Players");
       
    }
}
