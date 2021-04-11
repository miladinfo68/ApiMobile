using System.Data;
using System.Data.SqlClient;
using Dapper;
using Utility;
using Model;
using static Utility.Helper;
using System.Collections.Generic;
using DTO;
using System;

namespace DAL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public EmployeeDTO Select(string employeeCode, bool withPic = false)
        {
            EmployeeDTO empDTO = null; 
            using (var con = new SqlConnection(StaticValue.SupplementaryConnectionString))
            {
                if (con.State != ConnectionState.Open) con.Open();
                var employees = con.Query<Employee>(StaticValue.SpSelectEmployeeByEmployeeCode,new { employeeCode = employeeCode, withPic = withPic }, commandType: CommandType.StoredProcedure ).AsList();
                if (employees !=null && employees.Count>0)
                {
                    var employee = employees.Find(x => x.UserName == employeeCode);

                    var roles = new List<string>();
                    employees.ForEach(emp => { roles.Add(emp.RoleId.ToString()); });
                    empDTO = new EmployeeDTO()
                    {
                        UserName = employee.UserName,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Mobile = employee.Mobile,
                        RoleIds = string.Join(",", roles),
                        Active=employee.Active,                      
                        ObjectType = ObjectType.Employee
                    };
                } 
            }
            return empDTO;
        }
        
        public bool CheckPassword(string employeeCode, string password)
        {
            
            using (var con = new SqlConnection(StaticValue.SupplementaryConnectionString))
            {
                var professorPassword = con.QueryFirstOrDefault<string>(StaticValue.SpGetEmployeePassword,
                    new { employeeCode = employeeCode },
                    commandType: CommandType.StoredProcedure
                );
                return professorPassword.DecryptPass() == password;
            }
        }


        public bool ChangePassword(string employeeCode, string password)
        {
            //using (var con = new SqlConnection(StaticValue.SupplementaryConnectionString))
            //{
            //    var resualt = con.Execute(StaticValue.SpChangeEmployeePassword,
            //        new { professorCode = employeeCode, password = password.EncryptPass() },
            //        commandType: CommandType.StoredProcedure
            //    );
            //    return resualt == 1;
            //}

            return false;
        }

    }
}
