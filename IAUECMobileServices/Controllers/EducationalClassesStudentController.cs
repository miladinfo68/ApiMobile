using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model;
using Model.BaseServiceModels;
using Utility;

namespace IAUECMobileServices.Controllers
{
    [Route("api/v1/[controller]")]
    public class EducationalClassesStudentController : ControllerBase
    {

        private readonly BaseServiceConfig _baseServiceConfig;
        public EducationalClassesStudentController(IOptions<BaseServiceConfig> options)
        {
            _baseServiceConfig = options.Value;
        }


        /// <summary>
        /// دریافت کلاسهای استاد در ترمی خاص
        /// </summary>
        /// <param name="classCode">کد کلاس</param>
        /// <param name="termCode">کد ترم مثال 2-97-96</param>
        /// <returns></returns>
        [HttpGet("{classCode}/{termCode}")]
        public ActionResult<ApiResponse> Get(decimal classCode, string termCode)
        {
            var resualt =
                ClientHelper.GetValue<UnitSelectionSyncModel>(_baseServiceConfig.StudentEducationalClass + $"?term={termCode}",
                    _baseServiceConfig).Where(x => x.EducationalClassId == classCode).Select(x => x.StudentCode);

            return new ApiResponse
            {
                ErrorId = !resualt.Any()
                    ? (int)Helper.ApiOutputError.NotExists
                    : (int)Helper.ApiOutputError.NoError,
                ErrorTitle = !resualt.Any()
                    ? ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString()
                    : ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                Result = resualt.SkipWhile(string.IsNullOrWhiteSpace)
            };
        }

        /// <summary>
        /// دریافت دانشجویان کلاسها در ترمی خاص
        /// </summary>
        /// <param name="termCode">کد ترم مثال 2-97-96</param>
        /// <returns></returns>
        [HttpGet("{termCode}")]
        public ActionResult<ApiResponse> Get(string termCode)
        {
            var resualt =
                ClientHelper.GetValue<UnitSelectionSyncModel>(
                        _baseServiceConfig.StudentEducationalClass + $"?term={termCode}",
                        _baseServiceConfig)
                    .Select(x => new { ClassCode = x.EducationalClassId, StudentId = x.StudentCode })
                    .GroupBy(y => y.ClassCode, y => y.StudentId,
                        (key, m) => new { ClassCode = key, StudentList = m.ToList() });

            return new ApiResponse
            {
                ErrorId = !resualt.Any()
                    ? (int)Helper.ApiOutputError.NotExists
                    : (int)Helper.ApiOutputError.NoError,
                ErrorTitle = !resualt.Any()
                    ? ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString()
                    : ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                Result = resualt.SkipWhile(x => x.ClassCode == 0)
            };
        }
    }
}