using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUserRepository
    {
        Task<List<decimal>> GetUsersAsync(decimal collegeId, decimal groupId, decimal fieldId,decimal lessonId, decimal educationalClassId, int objectTypeId,string termCode);

    }
}