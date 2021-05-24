using ProViewGolf.Core.Dbo.Entities;

namespace ProViewGolf.Core.Dbo.Models
{
    public class EquipmentModel
    {
        public long EquipmentId { get; set; }
        public long StudentId { get; set; }

        public EquipmentType Type { get; set; }
        public Club Club { get; set; }

        public string Brand { get; set; }
        public string Shaft { get; set; }
        public string Model { get; set; }
        public int ClubLoft { get; set; } = 0;
        public int Pairs { get; set; } = 0;
        public int Pieces { get; set; } = 0;
        public string Grip { get; set; }
        public string Size { get; set; }
        public string IronWoodTypes { get; set; }
    }
}