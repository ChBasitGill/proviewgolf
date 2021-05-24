using System;
using System.Collections.Generic;
using ProViewGolf.Core.Dbo.Entities;

namespace ProViewGolf.Core.Dbo.Models
{
    public class ClubPracticeModel
    {
        public DateTime DateTime { get; set; }
        public long StudentId { get; set; } = 0;
        public bool IsWithPro { get; set; } = false;
        public List<ClubRecordModel> ClubRecords { get; set; }
    }

    public class ClubRecordModel
    {
        public Ground Ground { get; set; }
        public Club Club { get; set; }

        public float AvgDistance { get; set; } = 0.0f;
        public float ClubHeadSpeed { get; set; } = 0.0f;
        public float SpinRate { get; set; } = 0.0f;
        public float Apex { get; set; } = 0.0f;
        public float BallsAmount { get; set; } = 0.0f;
        public float Rating { get; set; }
    }
}