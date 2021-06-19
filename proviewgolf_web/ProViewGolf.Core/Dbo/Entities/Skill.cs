using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Generic;

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
        //[ForeignKey("StudentId")] 
        public virtual Student Student { get; protected set; }
    }
    public class SkillsAverages {
        public double Stretching { get; set; }
        public double FitnessSessionLowerBody { get; set; }
        public double FitnessSessionUpperBody { get; set; }
        public double FitnessSessionCore { get; set; }
        public double MentalTraining { get; set; }
        public double AlignmentDrill { get; set; }
        public double GreenReading { get; set; }
        public double CourseManagement { get; set; }
        public double RulesQuiz { get; set; }
        public double VideoSwingAnalysis { get; set; }
        public double _18HolesWalk { get; set; }
        public double _9HolesWalk { get; set; }
        public double _18HolesPlayedWithGolfCar { get; set; }
    }
    public class SkillGrouping{
        public string Text {get;set;}
        public SkillsAverages Average {get;set;}
    }
        public class SkillDto  
    {
        public Skill Skill {get;set;}
        public SkillsAverages Averages {get;set;}

        public IEnumerable<SkillGrouping> MonthlyGrouping {get;set;}
        public IEnumerable<SkillGrouping> YearlyGrouping {get;set;}
        public SkillGrouping WeeklyGrouping {get;set;}
        
    }
    
}