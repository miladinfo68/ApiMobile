using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utility;

namespace DAL
{
    public class RolesRepository : IRolesRepository
    {
        public UserRole SelectOne(int roleId)
        {
            try
            {
                using (var con = new SqlConnection(StaticValue.SupplementaryConnectionString))
                {
                    if (con.State != ConnectionState.Open) con.Open();
                    var userRole = con.QueryFirstOrDefault<UserRole>(StaticValue.SpSelectRoles,
                        new { roleId = roleId },
                        commandType: CommandType.StoredProcedure
                    );
                    return (userRole != null) ? userRole : null;
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                throw e;
            }
        }
        //==

        public List<UserRole> SelectAll()
        {
            try
            {
                int? roleId = null;
                using (var con = new SqlConnection(StaticValue.SupplementaryConnectionString))
                {
                    if (con.State != ConnectionState.Open) con.Open();
                    var userRoles = con.Query<UserRole>(StaticValue.SpSelectRoles,
                        new { roleId = roleId },
                        commandType: CommandType.StoredProcedure
                    ).AsList<UserRole>();
                    return (userRoles != null && userRoles.Count > 0) ? userRoles : null;
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                throw e;
            }
        }
    }
}