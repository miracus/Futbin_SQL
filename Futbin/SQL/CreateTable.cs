using Dapper;
using System.Threading.Tasks;

namespace Futbin.SQL
{

    public class CreateTable
    {
        private async Task CreateAsync(string tableName, string columns)
        {
            using (var database = Context.ConnectToSQL)
            {
                var query = $"CREATE TABLE {tableName} ({columns});";
                await database.ExecuteAsync(query);
            }
        }

        private async Task AlterTableAsync(string tableName, string alterStatement)
        {
            using (var database = Context.ConnectToSQL)
            {
                var query = $"ALTER TABLE {tableName} {alterStatement};";
                await database.ExecuteAsync(query);
            }
        }

        public async Task Players()
        {
            await CreateAsync("Players", "Id UNIQUEIDENTIFIER PRIMARY KEY, IdFut INT, Name NVARCHAR(MAX), Type NVARCHAR(MAX),GoldSilverBronze NVARCHAR(MAX),RareCommon NVARCHAR(MAX), Img NVARCHAR(MAX), Rating INT, Playstyle NVARCHAR(MAX), Price FLOAT, " +
                "TrendPersent FLOAT, Position NVARCHAR(MAX), AltPositions NVARCHAR(MAX), ClubId INT, ClubTitle NVARCHAR(MAX), ClubImg NVARCHAR(MAX), " +
                "NationId INT, NationTitle NVARCHAR(MAX), NationImg NVARCHAR(MAX), LeagueId INT, LeagueTitle NVARCHAR(MAX), LeagueImg NVARCHAR(MAX), " +
                "SKI INT, WF INT, WR NVARCHAR(MAX), PAC INT, SHO INT, PAS INT, DRI INT, DEF INT, PHY INT, HeightCM FLOAT, HeightD FLOAT, " +
                "Weight INT, Popularity INT, BS INT, IGS INT");
        }
        public async Task Price()
        {
            await CreateAsync("Price", "Id UNIQUEIDENTIFIER PRIMARY KEY, PlayerId INT, UpdateDT DATETIME, " +
                "Name NVARCHAR(MAX), Type NVARCHAR(MAX),GoldSilverBronze NVARCHAR(MAX),RareCommon NVARCHAR(MAX), Price FLOAT, TrendPersent FLOAT");
        }

       
    }
}
