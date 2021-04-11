namespace Model.BaseServiceModels
{
    public class EducationalGroupServiceBaseModel : BaseServiceBaseModel
    {
        public decimal CollegeId { get; set; }
        public CollegeServiceBaseModel College { get; set; }
        public decimal EducationalGroupCode { get; set; }
        //public decimal Termid { get; set; }
        //public TermServiceBaseModel Term { get; set; }
    }
}
