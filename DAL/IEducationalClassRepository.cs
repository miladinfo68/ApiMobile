using System.Collections.Generic;
using System.Threading.Tasks;
using Model.BaseServiceModels;

namespace DAL
{
    public interface IEducationalClassRepository
    {
        Task<List<EducationalClassServiceBaseModel>> GetManyByFieldIdAsync(decimal filedId);
        Task<List<EducationalClassServiceBaseModel>> GetManyByGroupIdAsync(decimal groupId);
    }
}