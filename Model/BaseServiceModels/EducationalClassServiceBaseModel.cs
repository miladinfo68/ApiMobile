using System;

namespace Model.BaseServiceModels
{
    public class EducationalClassServiceBaseModel : BaseServiceBaseModel
    {
        //public decimal ClassCode { get; set; }
        //public decimal GroupId { get; set; }
        //public EducationalGroupServiceBaseModel Group { get; set; }
        //public decimal ProfessorId { get; set; }
        //public ProfessorServiceBaseModel Professor { get; set; }
        //public int Capacity { get; set; }
        //public DateTime? StartDate { get; set; }
        //public DateTime? ExamDate { get; set; }
        //public int NumberOfSessions { get; set; }
        //public string StartTime { get; set; }
        //public string ExamTime { get; set; }
        //public dynamic ClassType { get; set; }
        //public DateTime? HoldingExamDate { get; set; }
        //public string Term { get; set; }
        //public TermServiceBaseModel Term { get; set; }

        public decimal ClassCode { get; set; }
        public decimal GroupId { get; set; }
        public decimal FieldId { get; set; }

        //  public EducationalGroupServiceBaseModel Group { get; set; }
        //   public FieldServiceBaseModel Field { get; set; }
        public int ProfessorId { get; set; }
        //   public ProfessorServiceBaseModel Professor { get; set; }
        public int Capacity { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExamDate { get; set; }
        public int NumberOfSessions { get; set; }
        public string StartTime { get; set; }
        public string ExamTime { get; set; }
        //  public TermServiceBaseModel Term { get; set; }
        public string TermCode { get; set; }
        public DateTime? HoldingExamDate { get; set; }
        public decimal? ContentType { get; set; }
        public decimal? HoldingType { get; set; }
    }
}
