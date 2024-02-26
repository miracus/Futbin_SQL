using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futbin.Models
{
    public class PlayerData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string GoldSilverBronze { get; set; }
        public string RareCommon { get; set; }
        public string Img { get; set; }
        public int Rating { get; set; }
        public string Playstyle { get; set; }
        public double Price { get; set; }
        public double TrendPersent { get; set; }
        public string Position { get; set; }
        public string AltPositions { get; set; }
        public ClubData Club { get; set; }
        public NationData Nation { get; set; }
        public LeagueData League { get; set; }
        public int SKI { get; set; }
        public int WF { get; set; }
        public string WR { get; set; }
        public int PAC { get; set; }
        public int SHO { get; set; }
        public int PAS { get; set; }
        public int DRI { get; set; }
        public int DEF { get; set; }
        public int PHY { get; set; }
        public double HeightCM { get; set; }
        public double HeightD { get; set; }
        public int Weight { get; set; }
        public int Popularity { get; set; }
        public int BS { get; set; }
        public int IGS { get; set; }
        //public CharDetail CharDetail { get; set; }

    }

    //class CharDetail
    //{
    //    public PACE Pace { get; set; }
    //    public SHOOTING SHOOTING { get; set; }
    //    public PASSING PASSING { get; set; }
    //    public DRIBBLING DRIBBLING { get; set; }
    //    public DEFENDING DEFENDING { get; set; }
    //    public PHYSICAL PHYSICAL { get; set; }


    //}
    //class PACE
    //{
    //    public int Acceleration { get; set; }
    //    public int SprintSpeed { get; set; }

    //}
    //class SHOOTING
    //{
    //    public int AttPosition { get; set; }
    //    public int Finishing { get; set; }
    //    public int ShotPower { get; set; }
    //    public int LongShots { get; set; }
    //    public int Volleys { get; set; }
    //    public int Penalties { get; set; }
    //}
    //class PASSING
    //{
    //    public int Vision { get; set; }
    //    public int Crossing { get; set; }
    //    public int FKAcc { get; set; }
    //    public int ShortPass { get; set; }
    //    public int LongPass { get; set; }
    //    public int Curve { get; set; }
    //}
    //class DRIBBLING
    //{
    //    public int Agility { get; set; }
    //    public int Balance { get; set; }
    //    public int Reactions { get; set; }
    //    public int BallControl { get; set; }
    //    public int Dribbling { get; set; }
    //    public int Composure { get; set; }
    //}
    //class DEFENDING
    //{
    //    public int Interceptions { get; set; }
    //    public int HeadingAcc { get; set; }
    //    public int DefAwareness { get; set; }
    //    public int StandTackle { get; set; }
    //    public int SlideTackle { get; set; }
    //}
    //class PHYSICAL
    //{
    //    public int Jumping { get; set; }
    //    public int Stamina { get; set; }
    //    public int Strength { get; set; }
    //    public int Aggression { get; set; }
    //}
}
