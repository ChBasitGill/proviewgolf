using System;
using System.Collections.Generic;
using ProViewGolf.Core.Dbo.Entities;

namespace ProViewGolf.Core.Dbo.Models
{
    public class ShotPracticeModel
    {
        public DateTime DateTime { get; set; }
        public long StudentId { get; set; } = 0;
        public bool IsWithPro { get; set; } = false;

        public List<ShotRecordModel> ShotRecords { get; set; }
    }

    public class ShotRecordModel
    {
        public ShotCategory ShotCategory { get; set; }
        public int ShotType { get; set; }

        public int Shots { get; set; }
        public int GoodShots { get; set; }
    }
}