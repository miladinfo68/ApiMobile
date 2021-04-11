
namespace Model.BaseServiceModels
{
    public class UnitSelectionSyncModel
    {
        public decimal Id { get; set; }

        public string Name
        {
            get { return $"{StudentId} - {EducationalClassId} - {TermCode}"; }
            set { }
        }
        public bool IsActive { get; set; }
        public decimal? EducationalClassId { get; set; }
        public string StudentCode { get; set; }
        public string StudentId { get; set; }
        public decimal? Score { get; set; }
        public int? ScoreStatus { get; set; }
        public string TermCode { get; set; }
    }
}