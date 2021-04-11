using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using Model;
using Utility;


namespace DAL
{
    public class StudentRepository : IStudentRepository
    {
        public Student Select(string studentCode, bool withPic = false, bool isSelectFromFnewStudent = false)
        {
            try
            {
                using (var con = new SqlConnection(StaticValue.SupplementaryConnectionString))
                {
                    if (con.State != ConnectionState.Open) con.Open();

                    var sp = StaticValue.SpSelectStudentByStudentCode;
                    if (isSelectFromFnewStudent)
                        sp = StaticValue.SpSelectEmployeeByEmployeeCodeFromFnewStudent;

                    var student = con.QueryFirstOrDefault<Student>(sp, new { studentCode = studentCode, withPic = withPic }, commandType: CommandType.StoredProcedure);
                    if (student != null)
                    {
                        student.StateName = student.StateId.ToInt().ToStudentStateName();
                        student.Active = student.StateId.ToInt().IsActiveStudent();
                        student.DegreeName = student.DegreeId.ToInt().ToStudentDegreeName();
                        return student;
                    }


                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public UserTicketState GetStateForTickets(string studentCode)
        {
            try
            {
                using (var con = new SqlConnection(StaticValue.SupplementaryConnectionString))
                {
                    if (con.State != ConnectionState.Open) con.Open();

                    var state = con.QueryFirstOrDefault<UserTicketState>(StaticValue.SpSelectStudentStateTickeByStudentCode,
                        new { studentCode = studentCode },
                        commandType: CommandType.StoredProcedure
                    );
                    if (state != null)
                    {
                        state.IsStudent = true;
                        return state;
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public bool CheckPassword(string studentCode, string password)
        {

            using (var con = new SqlConnection(StaticValue.SupplementaryConnectionString))
            {
                var studentPassword = con.QueryFirstOrDefault<string>(StaticValue.SpGetStudentPassword,
                    new { studentCode = studentCode },
                   commandType: CommandType.StoredProcedure
                    );
                return studentPassword.DecryptPass() == password;
            }

        }

        public bool ChangePassword(string studentCode, string password)
        {

            //Isida4_webservice_main ms = new Isida4_webservice_main.Isida4_webservice_mainservice();
            using (var con = new SqlConnection(StaticValue.SupplementaryConnectionString))
            {
                var resualt = con.Execute(StaticValue.SpCheangeStudentPassword,
                    new { studentCode = studentCode, password = password.EncryptPass() },
                    commandType: CommandType.StoredProcedure
                );

                //ms.ctrl_elecAsync(studentCode, password, "1", "0", "97iauec1206");
                return resualt == -1;
            }

        }


    }
}
