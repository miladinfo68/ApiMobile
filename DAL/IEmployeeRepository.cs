using DTO;
using Model;
using System.Collections.Generic;

namespace DAL
{
    public interface IEmployeeRepository
    {
        EmployeeDTO Select(string employeeCode, bool withPic = false);
        bool CheckPassword(string employeeCode, string password);
        bool ChangePassword(string employeeCode, string password);
    }
}