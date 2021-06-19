using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProViewGolf.Core.Dbo.Entities
{
    public enum EquipmentType
    {
        Irons = 1,
        Woods = 2,
        Putter = 3,
        Ball = 4,
        Gloves = 5,
        TrackingSystem = 6,
        RangerFinder = 7,
        LaunchFinder = 9,
        Shoes = 10
    }

    //public enum EquipmentClub
    //{
    //    None = 0,

    //    IronHybrid2 = 100,
    //    IronHybrid3 = 101,
    //    IronHybrid4 = 102,
    //    IronHybrid5 = 103,
    //    Iron6 = 104,
    //    Iron7 = 105,
    //    Iron8 = 106,
    //    Iron9 = 107,
    //    PitchingWedge = 108,
    //    GapWedge = 109,
    //    SandWedge = 110,
    //    LobWedge = 111,
    //    Driver = 200,
    //    Wood3 = 201,
    //    Wood5 = 202,
    //    Wood7 = 203,
    //    Rescue = 204
    //}

    public class Equipment
    {
        public long EquipmentId { get; set; }

        public EquipmentType Type { get; set; }
        public Club Club { get; set; }

        public string Brand { get; set; }
        public string Shaft { get; set; }
        public string Model { get; set; }
        public int ClubLoft { get; set; } = 0;
        public string Grip { get; set; }
        public string Size { get; set; }
        public int Pairs { get; set; } = 0;
        public int Pieces { get; set; } = 0;

        public long StudentId { get; set; }
       // [ForeignKey("StudentId"), JsonIgnore] 
        public virtual Student Student { get; protected set; }
    }
}