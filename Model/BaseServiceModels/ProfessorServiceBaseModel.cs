namespace Model.BaseServiceModels
{
    public class ProfessorServiceBaseModel : BaseServiceBaseModel
    {

        public string Name
        {
            get { return FirstName + "-" + LastName; }
            set
            {
                var split = value.Split('-');
                FirstName = split[0];
                LastName = split[1];
            }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Code { get; set; }
        public string NationalCode { get; set; }
        public bool Gender { get; set; }
        public int? ScientificRank { get; set; }
        public int? TeachingExperience { get; set; }
        public int? AcademicDegree { get; set; }
        public int? UniversityStudyPlace { get; set; }
        public int? UniversityWorkPlace { get; set; }
    }
}
