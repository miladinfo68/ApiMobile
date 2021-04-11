using Utility;
namespace Model
{
    public class Professor : IProfessor
    {
        public string ProfessorCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string NationalCode { get; set; }
        public string Mobile { get; set; }
        public bool Active { get; set; }
        public string StateId { get; set; }
        public string StateName { get; set; }
        public byte? Sex { get; set; }
        public Helper.ObjectType ObjectType { get; set; }

        public byte[] Picture { get; set; }
        //public string GroupId { get; set; }
        //public string GroupName { get; set; }
        //public string CollegeId { get; set; }
        //public string CollegeName { get; set; }
        //public string DegreeId { get; set; }
        //public string DegreeName { get; set; }
    }
}