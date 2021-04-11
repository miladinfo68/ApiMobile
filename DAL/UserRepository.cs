using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Utility;

namespace DAL
{
    public class UserRepository : IUserRepository
    {
        public async Task<List<decimal>> GetUsersAsync(decimal collegeId, decimal groupId, decimal fieldId, decimal lessonId, decimal educationalClassId, int objectTypeId, string termCode)
        {
            try
            {
                using (var con = new SqlConnection(StaticValue.SupplementaryConnectionString))
                {
                    var users = await con.QueryAsync<decimal>(StaticValue.SpGetUsers, new
                        {
                            collegeId = collegeId,
                            groupId = groupId,
                            fieldId = fieldId,
                            lessonId = lessonId,
                            educationalClassId = educationalClassId,
                            objectTypeId = objectTypeId,
                            termCode = termCode
                        },
                        commandType: CommandType.StoredProcedure);

                    if (users != null && users.Any()) return users.ToList();
                    //else return null;
                    else return new List<decimal>();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }
    }
}