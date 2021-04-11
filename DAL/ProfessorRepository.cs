using System;
using System.Data;
using System.Data.SqlClient;
using DAL;
using Dapper;
using Model;
using Utility;


public class ProfessorRepository : IProfessorRepository
{
    public Professor Select(string professorCode, bool withPic = false)
    {
        try
        {


            using (var con = new SqlConnection(StaticValue.SupplementaryConnectionString))
            {
                if (con.State != ConnectionState.Open) con.Open();
                var professor = con.QueryFirstOrDefault<Professor>(StaticValue.SpSelectProfessorByProfessorCode,
                    new { professorCode = professorCode, withPic = withPic },
                    commandType: CommandType.StoredProcedure
                );
                if (professor != null)
                {
                    professor.StateName = professor.StateId.ToInt().ToProfessorStateName();
                    professor.Active = professor.StateId.ToInt().IsActiveProfessor();
                    return professor;
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

    public bool CheckPassword(string professorCode, string password)
    {
        using (var con = new SqlConnection(StaticValue.SupplementaryConnectionString))
        {
            var professorPassword = con.QueryFirstOrDefault<string>(StaticValue.SpGetProfessorPassword,
                new { professorCode = professorCode },
                commandType: CommandType.StoredProcedure
            );
            return professorPassword.DecryptPass() == password;
        }
    }

    public bool ChangePassword(string professorCode, string password)
    {
        using (var con = new SqlConnection(StaticValue.SupplementaryConnectionString))
        {
            var resualt = con.Execute(StaticValue.SpCheangeProfessorPassword,
                new { professorCode = professorCode, password = password.EncryptPass() },
                commandType: CommandType.StoredProcedure
            );
            return resualt == 1;
        }
    }
}
