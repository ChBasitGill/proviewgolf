using System;
using ProViewGolf.Core.Dbo.Entities;
using ProViewGolf.Core.Dbo.Models;

namespace ProViewGolf.DataLayer.Models
{


    public class FilterModel
    {
        public long StudentId { get; set; }
        public DateTime Date { get; set; }

        public ShotGroup ShotGroup { get; set; }
        public int ShotType { get; set; }
        public Duration Duration { get; set; }
    }
}
