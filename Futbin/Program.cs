using HtmlAgilityPack;
using System;
using System.Text;
using System.Diagnostics;
using Futbin.SQL;
using System.Threading.Tasks;
using Futbin.Models;
using Futbin.Data;
using System.Threading;

namespace Futbin
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(DateTime.Now);




            int pages = 621;

            DateTime startTime = DateTime.Now;
            await new CreateTables().Players();
            await new CreateTables().Price();



            for (int page = 1; page < pages; page++)
            {
                //int page = 1;
                string url = "https://www.futbin.com/players?page=" + page;
                HtmlWeb web = new HtmlWeb();
                //web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
                HtmlDocument doc = web.Load(url);

                HtmlNodeCollection tableNodes = doc.DocumentNode.SelectNodes("//*[@id=\"repTb\"]");

                if (tableNodes != null)
                {

                    var playerRows = doc.DocumentNode.SelectNodes("//tr[contains(@class, 'player_tr_')]");
                    if (playerRows != null)
                    {
                        foreach (HtmlNode playerNode in playerRows)
                        {
                            PlayerData player = new PlayerData();
                            player.Nation = new NationData();
                            player.Club = new ClubData();
                            player.League = new LeagueData();
                        
                            HtmlNode idItem = playerNode.SelectSingleNode(".//*[@class='igs-btn pt-1 px-2']");
                            player.Id = Int32.Parse(idItem.GetAttributeValue("data-playerid", ""));

                            player.Name = playerNode.SelectSingleNode(".//a[@class='player_name_players_table get-tp']").InnerText;

                            var ratingItem = playerNode.SelectNodes(".//*[contains(@class, 'form rating ut24 ')]");
                            if (ratingItem.Count > 1)
                            {

                                HtmlDocument doc1 = new HtmlDocument();
                                doc1.LoadHtml(ratingItem[0].OuterHtml);

                                HtmlNode imgNode0 = doc1.DocumentNode.SelectSingleNode("//img");

                                if (imgNode0 != null)
                                {
                                    string classAttributeValue = imgNode0.GetAttributeValue("class", "");
                                    string[] classes = classAttributeValue.Split(' ');
                                    player.Type = classes[6];
                                    player.GoldSilverBronze = classes[7];
                                    player.RareCommon = classes[8];
                                    player.Img = imgNode0.GetAttributeValue("data-original", null);
                                }

                                //string classAttributeValue = ratingItem.First("class", "");
                                //string[] classes = classAttributeValue.Split(' ');
                                //player.Type = imgNode.GetAttributeValue("data-original", null);


                            
                                player.Rating = Int32.Parse(ratingItem[1].InnerText);
                            }

                            var imgNode1 = playerNode.SelectSingleNode("//*[@id=\"repTb\"]/tbody/tr[1]/td[2]/div[2]/div[2]/div/div/div");

                            player.Playstyle = imgNode1?.GetAttributeValue("title", null);

                            player.Position = playerNode.SelectSingleNode(".//*[@class='font-weight-bold']").InnerHtml;

                            player.AltPositions = playerNode.SelectSingleNode(".//*[@style='font-size: 12px;']").InnerHtml;
                            //player.AltPositions = altPositionItem.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                            player.TrendPersent = Parse.TrendPersent(playerNode);

                            // Parse price
                            var priceItem = playerNode.SelectSingleNode(".//*[@class=' font-weight-bold']").InnerText;
                            player.Price = Parse.Price(priceItem);

                            // Parse nation, club, and league
                            var nationsItem = playerNode.SelectSingleNode(".//*[@class='players_club_nation']").InnerHtml;
                            Parse.NationClubLeague(nationsItem, player);

                            // Parse additional data
                            Parse.AdditionalData(playerNode, player);

                            //await new InsertData.InsertAsync(player);

                            await new InsertData().Player(player);
                            await new InsertData().Price(player);

                            Console.WriteLine("Added player - " + player.Name);
                            //players.Add(player);
                            //Thread.Sleep(500);//1000
                        }
                    }
                    else
                    {
                        Console.WriteLine("Data not found on page " + page);
                    }
                    Thread.Sleep(600);//3000
                }
               
                Console.WriteLine("Page " + page + " processed at " + DateTime.Now);
            }

            Console.WriteLine("Processing complete. - " + startTime + " - " + DateTime.Now);
        }

    }
}
