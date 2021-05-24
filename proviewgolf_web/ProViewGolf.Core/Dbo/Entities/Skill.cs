using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProViewGolf.Core.Dbo.Entities
{
    public class Skill
    {
        [Key] public long SkillId { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;

        public int Stretching { get; set; }
        public int FitnessSessionLowerBody { get; set; }
        public int FitnessSessionUpperBody { get; set; }
        public int FitnessSessionCore { get; set; }
        public int MentalTraining { get; set; }
        public int AlignmentDrill { get; set; }
        public int GreenReading { get; set; }
        public int CourseManagement { get; set; }
        public int RulesQuiz { get; set; }
        public int VideoSwingAnalysis { get; set; }
        public int _18HolesWalk { get; set; }
        public int _9HolesWalk { get; set; }
        public int _18HolesPlayedWithGolfCar { get; set; }

        public long StudentId { get; set; }
        [ForeignKey("StudentId")] public virtual Student Student { get; protected set; }
    }
}