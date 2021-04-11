using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Model.BaseServiceModels;
using Utility;

namespace DAL
{
    public class EducationalClassRepository : IEducationalClassRepository
    {
        public Task<List<EducationalClassServiceBaseModel>> GetManyByFieldIdAsync(decimal filedId)
        {
            using (var con = new SqlConnection(StaticValue.SupplementaryConnectionString))
            {
                if (con.State != ConnectionState.Open) con.Open();
                var classes = con.QueryFirstOrDefault<List<EducationalClassServiceBaseModel>>(
                    StaticValue.SpSelectClassesByFieldId,
                    new { filedId = filedId },
                    commandType: CommandType.StoredProcedure
                );
                if (classes != null) return Task.FromResult<List<EducationalClassServiceBaseModel>>(classes);
                return null;
            }
        }

        public Task<List<EducationalClassServiceBaseModel>> GetManyByGroupIdAsync(decimal groupId)
        {
            using (var con = new SqlConnection(StaticValue.SupplementaryConnectionString))
            {
                if (con.State != ConnectionState.Open) con.Open();
                var classes = con.QueryFirstOrDefault<List<EducationalClassServiceBaseModel>>(
                    StaticValue.SpSelectClassesByGroupId,
                    new { groupId = groupId },
                    commandType: CommandType.StoredProcedure
                );
                if (classes != null) return Task.FromResult<List<EducationalClassServiceBaseModel>>(classes);
                return null;
            }
        }
    }
}