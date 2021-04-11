namespace Model
{
    public interface IProfessor : IProfile
    {
        string ProfessorCode { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string FatherName { get; set; }
        string NationalCode { get; set; }
        string Mobile { get; set; }
        bool Active { get; set; }
        string StateId { get; set; }
        string StateName { get; set; }
        //string GroupId { get; set; }
        //string GroupName { get; set; }
        //string CollegeId { get; set; }
        //string CollegeName { get; set; }
        //string DegreeId { get; set; }
        //string DegreeName { get; set; }



    }
}