
using Model;

namespace DAL
{
    public interface IStudentRepository
    {
        Student Select(string studentCode, bool withPic = false ,bool isSelectFromFnewStudent = false);
        bool CheckPassword(string studentCode, string password);
        bool ChangePassword(string studentCode, string password);
        UserTicketState GetStateForTickets(string studentCode);
    }

}