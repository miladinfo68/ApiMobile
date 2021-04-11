using Utility;

namespace Model
{
    public class Student : IStudent
    {
        public string StudentCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string NationalCode { get; set; }
        public string Mobile { get; set; }
        public string FieldId { get; set; }
        public string FieldName { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string CollegeId { get; set; }
        public string CollegeName { get; set; }
        public string DegreeId { get; set; }
        public string DegreeName { get; set; }
        public Helper.ObjectType ObjectType { get; set; }
        public string StateId { get; set; }
        public string StateName { get; set; }
        public bool Active { get; set; }
        public int? TermIn { get; set; }
        public byte? Sex { get; set; }
        public string BirthDate { get; set; }      
        public string LastTerm { get; set; }
        public byte[] Picture { get; set; }

    }
}
