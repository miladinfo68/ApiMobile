using Model;

namespace DAL
{
    public interface IProfessorRepository
    {
        Professor Select(string professorCode, bool withPic = false);
        bool CheckPassword(string professorCode, string password);
        bool ChangePassword(string professorCode, string password);
    }
}