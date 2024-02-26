using Dapper;
using System;
using System.Threading.Tasks;

namespace Futbin.SQL
{
    public class CreateTables
    {
        private async Task CreateTableAndInsertIfNotExistsAsync(string tableName, Func<Task> createTableAsync, Func<Task> insertDataAsync = null)
        {
            if (!await new TablesIsExist().TableExistsAsync(tableName))
            {
                await createTableAsync();
                if (insertDataAsync != null)
                {
                    await insertDataAsync();
                }
            }
        }

        public async Task Players()
        {
            await CreateTableAndInsertIfNotExistsAsync("Players", () => new CreateTable().Players());
        }

        public async Task Price()
        {
            await CreateTableAndInsertIfNotExistsAsync("Price", () => new CreateTable().Price());
        }

     
    }
}
