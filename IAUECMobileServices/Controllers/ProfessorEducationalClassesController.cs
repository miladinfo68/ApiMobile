using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model;
using Model.BaseServiceModels;
using Utility;

namespace IAUECMobileServices.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProfessorEducationalClassesController : ControllerBase
    {

        private readonly BaseServiceConfig _baseServiceConfig;
        public ProfessorEducationalClassesController(IOptions<BaseServiceConfig> options)
        {
            _baseServiceConfig = options.Value;
        }


        /// <summary>
        /// دریافت کلاسهای استاد در ترمی خاص
        /// </summary>
        /// <param name="professorCode">کد استاد</param>
        /// <param name="termCode">کد ترم مثال 2-97-96</param>
        /// <returns></returns>
        [HttpGet("{professorId}/{termCode}")]
        public ActionResult<ApiResponse> Get(int professorId, string termCode)
        {
            var resualt =
                ClientHelper.GetValue<EducationalClassServiceBaseModel>(_baseServiceConfig.EducationalClassRelativeAddress + $"?term={termCode}",
                    _baseServiceConfig).Where(x => x.ProfessorId == professorId).Select(x => x.ClassCode);

            return new ApiResponse
            {
                ErrorId = !resualt.Any()
                    ? (int)Helper.ApiOutputError.NotExists
                    : (int)Helper.ApiOutputError.NoError,
                ErrorTitle = !resualt.Any()
                    ? ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString()
                    : ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                Result = resualt.SkipWhile(x => x == 0)
            };
        }

        /// <summary>
        /// دریافت کلاسهای اساتید در ترمی خاص
        /// </summary>
        /// <param name="termCode">کد ترم مثال 2-97-96</param>
        /// <returns></returns>
        [HttpGet("{termCode}")]
        public ActionResult<ApiResponse> Get(string termCode)
        {
            var resualt =
                ClientHelper.GetValue<EducationalClassServiceBaseModel>(
                        _baseServiceConfig.EducationalClassRelativeAddress + $"?term={termCode}",
                        _baseServiceConfig)
                    .Select(x => new { ClassCode = x.ClassCode, ProfessorId = x.ProfessorId })
                    .GroupBy(y => y.ProfessorId, y => y.ClassCode,
                        (key, m) => new { ProfessorId = key, ClassList = m.ToList() });

            return new ApiResponse
            {
                ErrorId = !resualt.Any()
                    ? (int)Helper.ApiOutputError.NotExists
                    : (int)Helper.ApiOutputError.NoError,
                ErrorTitle = !resualt.Any()
                    ? ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString()
                    : ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                Result = resualt.SkipWhile(x => x.ProfessorId == 0)
            };
        }
    }
}