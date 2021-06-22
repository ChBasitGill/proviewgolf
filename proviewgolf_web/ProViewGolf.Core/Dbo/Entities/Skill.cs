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

        public decimal Stretching { get; set; }
        public decimal FitnessSessionLowerBody { get; set; }
        public decimal FitnessSessionUpperBody { get; set; }
        public decimal FitnessSessionCore { get; set; }
        public decimal MentalTraining { get; set; }
        public decimal AlignmentDrill { get; set; }
        public decimal GreenReading { get; set; }
        public decimal CourseManagement { get; set; }
        public decimal RulesQuiz { get; set; }
        public decimal VideoSwingAnalysis { get; set; }
        public decimal _18HolesWalk { get; set; }
        public decimal _9HolesWalk { get; set; }
        public decimal _18HolesPlayedWithGolfCar { get; set; }

        public long StudentId { get; set; }
        //[ForeignKey("StudentId")] 
        public virtual Student Student { get; protected set; }
    }
    public class SkillsAverages {
        public decimal Stretching { get; set; }
        public decimal FitnessSessionLowerBody { get; set; }
        public decimal FitnessSessionUpperBody { get; set; }
        public decimal FitnessSessionCore { get; set; }
        public decimal MentalTraining { get; set; }
        public decimal AlignmentDrill { get; set; }
        public decimal GreenReading { get; set; }
        public decimal CourseManagement { get; set; }
        public decimal RulesQuiz { get; set; }
        public decimal VideoSwingAnalysis { get; set; }
        public decimal _18HolesWalk { get; set; }
        public decimal _9HolesWalk { get; set; }
        public decimal _18HolesPlayedWithGolfCar { get; set; }
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