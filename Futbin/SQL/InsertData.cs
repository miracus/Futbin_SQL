using Dapper;
using Futbin.Models;
using Futbin.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Futbin.SQL
{

    public class InsertData
    {
        private readonly IDbConnection _database;

        public InsertData()
        {
            _database = Context.ConnectToSQL;
        }








        public async Task Price(PlayerData player)
        {
            var query = "INSERT INTO [dbo].[Price] ([Id], [PlayerId],[UpdateDT],[Name] ,[Type],[GoldSilverBronze],[RareCommon],[Price] ,[TrendPersent]) VALUES (@Id ,@PlayerId ,@UpdateDT ,@Name ,@Type ,@GoldSilverBronze, @RareCommon, @Price ,@TrendPersent)";
            var parameters = new
            {
                Id = Guid.NewGuid(),
                PlayerId = player.Id,
                UpdateDT = DateTime.Now,
                Name = player.Name,
                Type = player.Type,
                GoldSilverBronze = player.GoldSilverBronze,
                RareCommon = player.RareCommon,
                Price = player.Price,
                TrendPersent = player.TrendPersent
            };

            await _database.ExecuteAsync(query, parameters);
        }
        
        public async Task Player(PlayerData player)
        {
            var query = "INSERT INTO [dbo].[Players] ([Id], [IdFut], [Name], [Type],[GoldSilverBronze],[RareCommon], [Img], [Rating], [Playstyle], [Price], [TrendPersent], [Position], [AltPositions], [ClubId], [ClubTitle], [ClubImg], [NationId], [NationTitle], [NationImg], [LeagueId], [LeagueTitle], [LeagueImg], [SKI], [WF], [WR], [PAC], [SHO], [PAS], [DRI], [DEF], [PHY], [HeightCM], [HeightD], [Weight], [Popularity], [BS], [IGS])      VALUES (@Id, @IdFut, @Name , @Type ,@GoldSilverBronze, @RareCommon, @Img , @Rating, @Playstyle , @Price, @TrendPersent, @Position, @AltPositions , @ClubId, @ClubTitle , @ClubImg , @NationId, @NationTitle , @NationImg , @LeagueId, @LeagueTitle , @LeagueImg , @SKI, @WF, @WR , @PAC, @SHO, @PAS, @DRI, @DEF, @PHY, @HeightCM, @HeightD, @Weight, @Popularity, @BS, @IGS)";
            var parameters = new
            {
             Id = Guid.NewGuid(),
             IdFut = player.Id,
             Name = player.Name,
             Type = player.Type,
            GoldSilverBronze = player.GoldSilverBronze,
            RareCommon = player.RareCommon,
             Img = player.Img,
             Rating = player.Rating,
             Playstyle= player.Playstyle,
             Price = player.Price,
             TrendPersent = player.TrendPersent,
             Position = player.Position,
             AltPositions = player.AltPositions,
             ClubId = player.Club.Id,
             ClubTitle= player.Club.Title,
             ClubImg = player.Club.Img,
             NationId = player.Nation.Id,
             NationTitle =  player.Nation.Title,
             NationImg = player.Nation.Img,
             LeagueId = player.League.Id,
             LeagueTitle = player.League.Title,
             LeagueImg = player.League.Img,
             SKI = player.SKI,
             WF = player.WF,
             WR = player.WR,
             PAC = player.PAC,
             SHO = player.SHO,
             PAS = player.PAS,
             DRI = player.DRI,
             DEF = player.DEF,
             PHY = player.PHY,
             HeightCM = player.HeightCM,
             HeightD = player.HeightD,
             Weight = player.Weight,
             Popularity = player.Popularity,
             BS = player.BS,
             IGS = player.IGS,
            };

            await _database.ExecuteAsync(query, parameters);
        }

    }
}
