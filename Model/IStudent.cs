namespace Model
{
    public interface IStudent : IProfile
    {
        string StudentCode { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string FatherName { get; set; }
        string NationalCode { get; set; }
        string Mobile { get; set; }
        string FieldId { get; set; }
        string FieldName { get; set; }
        string CollegeId { get; set; }
        string CollegeName { get; set; }
        string DegreeId { get; set; }
        string DegreeName { get; set; }

    }
}
