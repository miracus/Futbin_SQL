using Futbin.Models;
using HtmlAgilityPack;
using System;
using System.Text.RegularExpressions;

namespace Futbin.Data
{
    public class Parse
    {
        public static double Price(string priceItem)
        {
            if (priceItem.Trim().EndsWith("M"))
            {
                return double.TryParse(priceItem.Replace("M", "").Replace(".", ","), out double priceValue)
                    ? priceValue * 1000000
                    : 0;
            }
            else if (priceItem.Trim().EndsWith("K"))
            {
                return double.TryParse(priceItem.Replace("K", "").Replace(".", ","), out double priceValue)
                    ? priceValue * 1000
                    : 0;
            }
            else
            {
                return double.TryParse(priceItem.Trim().Replace(".", ","), out double priceValue)
                    ? priceValue
                    : 0;
            }
        }

        public static double TrendPersent(HtmlNode playerNode)
        {
            var trendItem = playerNode.SelectSingleNode(".//*[@class='trend-text trend-plus']");
            double TrendPersent = 0;

            if (trendItem != null)
            {
                if (double.TryParse(trendItem.InnerText.Trim().Replace("%", "").Replace(".", ","), out double trend1))
                {
                   return TrendPersent = 0 - trend1;
                }
            }
            else
            {
                trendItem = playerNode.SelectSingleNode(".//*[@class='trend-text trend-minus']");
                if (trendItem != null)
                {
                    if (double.TryParse(trendItem.InnerText.Trim().Replace("%", "").Replace(".", ","), out double trend2))
                    {
                        return TrendPersent = trend2;
                    }
                }
                else
                {
                    return TrendPersent = 0;
                }
            }
            return TrendPersent = 0;
        }

        public static void NationClubLeague(string nationsItem, PlayerData player)
        {
            var doc1 = new HtmlDocument();
            doc1.LoadHtml(nationsItem);
            var links = doc1.DocumentNode.SelectNodes("//a");

            if (links != null)
            {
                int i = 0;
                foreach (var link in links)
                {
                    string href = link.GetAttributeValue("href", "");
                    string club = link.GetAttributeValue("data-original-title", "");
                    string src = link.GetAttributeValue("src", "");
                    HtmlNode imgTag = link.SelectSingleNode(".//img");

                    if (i == 0)
                    {
                        Match match = Regex.Match(href, "(?<=&club=)\\d+");
                        if (match.Success && Int32.TryParse(match.Value, out int number))
                        {
                            player.Club.Id = number;
                            player.Club.Title = club;
                            player.Club.Img = imgTag.GetAttributeValue("src", "");
                        }
                    }
                    if (i == 1)
                    {
                        Match match = Regex.Match(href, "(?<=&nation=)\\d+");
                        if (match.Success && Int32.TryParse(match.Value, out int number))
                        {
                            player.Nation.Id = number;
                            player.Nation.Title = club;
                            player.Nation.Img = imgTag.GetAttributeValue("src", "");
                        }
                    }
                    if (i == 2)
                    {
                        Match match = Regex.Match(href, "(?<=&league=)\\d+");
                        if (match.Success && Int32.TryParse(match.Value, out int number))
                        {
                            player.League.Id = number;
                            player.League.Title = club;
                            player.League.Img = imgTag.GetAttributeValue("src", "");
                        }
                    }
                    i++;
                }
            }
        }

        public static void AdditionalData(HtmlNode playerNode, PlayerData player)
        {
            var starNodes = playerNode.SelectSingleNode("//*[@id=\"repTb\"]/tbody/tr[1]/td[7]");
            if (starNodes != null)
            {
                player.SKI = Int32.Parse(starNodes.InnerText);
            }

            var textNodes = playerNode.SelectSingleNode("//*[@id=\"repTb\"]/tbody/tr[1]/td[8]");
            if (textNodes != null)
            {
                player.WF = Int32.Parse(textNodes.InnerText);
            }

            var badgeNodes = playerNode.SelectSingleNode("//*[@id=\"repTb\"]/tbody/tr[1]/td[9]");
            if (badgeNodes != null)
            {
                player.WR = badgeNodes.InnerText;
            }

            Characteristics(playerNode, player);
        }

        public static void Characteristics(HtmlNode playerNode, PlayerData player)
        {
            for (int i = 10; i <= 19; i++)
            {
                var charNodes = playerNode.SelectSingleNode($"//*[@id=\"repTb\"]/tbody/tr[1]/td[{i}]");
                if (charNodes != null)
                {
                    var weightNode = playerNode.SelectSingleNode("//*[@id=\"repTb\"]/tbody/tr[1]/td[16]/div[2]");
                    if (weightNode != null)
                    {
                        string weight = weightNode.InnerText.Trim();
                        string[] parts = weight.Split('|');
                        string pattern = @"\d+";
                        Match match = Regex.Match(weight, pattern);

                        if (match.Success)
                        {
                            if (Int32.TryParse(match.Value, out int value0))
                            {
                                player.Weight = value0;
                            }
                            else
                            {
                                player.Weight = -1;
                            }
                        }
                        else
                        {
                            player.Weight = -1;
                        }
                    }

                    switch (i)
                    {
                        case 10:
                            if (Int32.TryParse(charNodes.InnerText, out int value10))
                            {
                                player.PAC = value10;
                            }
                            else
                            {
                                player.PAC = -1;
                            }
                            break;
                        case 11:
                            if (Int32.TryParse(charNodes.InnerText, out int value11))
                            {
                                player.SHO = value11;
                            }
                            else
                            {
                                player.SHO = -1;
                            }
                            break;
                        case 12:
                            if (Int32.TryParse(charNodes.InnerText, out int value12))
                            {
                                player.PAS = value12;
                            }
                            else
                            {
                                player.PAS = -1;
                            }
                            break;
                        case 13:
                            if (Int32.TryParse(charNodes.InnerText, out int value13))
                            {
                                player.DRI = value13;
                            }
                            else
                            {
                                player.DRI = -1;
                            }
                            break;
                        case 14:
                            if (Int32.TryParse(charNodes.InnerText, out int value14))
                            {
                                player.DEF = value14;
                            }
                            else
                            {
                                player.DEF = -1;
                            }
                            break;
                        case 15:
                            if (Int32.TryParse(charNodes.InnerText, out int value15))
                            {
                                player.PHY = value15;
                            }
                            else
                            {
                                player.PHY = -1;
                            }
                            break;
                        case 16:
                            // Parse height
                            var heightNode = playerNode.SelectSingleNode("//div[contains(text(), 'cm')]");
                            if (heightNode != null)
                            {
                                string height = heightNode.InnerText.Trim();
                                string[] parts = height.Split('|');
                                player.HeightCM = Double.Parse(parts[0].Trim().Replace("cm", ""));
                                player.HeightD = Double.Parse(parts[1].Trim().Replace("\"", "").Replace("'", ","));
                            }
                            break;
                        //case 17:
                        //    // Parse weight
                        //    var weightNode = playerNode.SelectSingleNode("//*[@id=\"repTb\"]/tbody/tr[1]/td[16]/div[2]");
                        //    if (weightNode != null)
                        //    {
                        //        string weight = weightNode.InnerText.Trim();
                        //        string[] parts = weight.Split('|');
                        //        string pattern = @"\d+";
                        //        Match match = Regex.Match(weight, pattern);

                        //        if (match.Success)
                        //        {
                        //            player.Weight = Int32.Parse(match.Value);
                        //        }
                        //        else
                        //        {
                        //            player.Weight = -1;
                        //        }
                        //    }
                        //    break;
                        case 17:
                            if (Int32.TryParse(charNodes.InnerText, out int value17))
                            {
                                player.Popularity = value17;
                            }
                            else
                            {
                                player.Popularity = -1;
                            }
                            break;
                        case 18:
                            if (Int32.TryParse(charNodes.InnerText, out int value18))
                            {
                                player.BS = value18;
                            }
                            else
                            {
                                player.BS = -1;
                            }
                            break;
                        case 19:
                            if (Int32.TryParse(charNodes.InnerText, out int value19))
                            {
                                player.IGS = value19;
                            } else
                            {
                                player.IGS = -1;
                            }
                            break;
                    }
                }
            }
        }
    }

}
