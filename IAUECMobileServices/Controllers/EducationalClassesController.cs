using System.Globalization;
using System.Linq;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model;
using Model.BaseServiceModels;
using Utility;

namespace IAUECMobileServices.Controllers
{
    [Route("api/v1/[controller]")]
    public class EducationalClassesController : ControllerBase
    {

        private readonly BaseServiceConfig _baseServiceConfig;
        private readonly EducationalClassRepository _educationalClassRepository;
        public EducationalClassesController(IOptions<BaseServiceConfig> options)
        {
            _baseServiceConfig = options.Value;
            _educationalClassRepository = new EducationalClassRepository();
        }

        /// <summary>
        /// دریافت کلاس های ترم جاری
        /// </summary>
        /// <returns></returns>

        [HttpGet("")]
        public ActionResult<ApiResponse> Get()
        {
            var resualt =
                ClientHelper.GetValue<EducationalClassServiceBaseModel>(_baseServiceConfig.EducationalClassRelativeAddress + $"?term={Helper.GetCurrentTerm(_baseServiceConfig)}",
                    _baseServiceConfig);

            return new ApiResponse
            {
                ErrorId = resualt == null || resualt.Count <= 0
                    ? (int)Helper.ApiOutputError.NotExists
                    : (int)Helper.ApiOutputError.NoError,
                ErrorTitle = resualt == null || resualt.Count <= 0
                    ? ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString()
                    : ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                Result = resualt.SkipWhile(x => x.ClassCode == 0)
            };
        }
        /// <summary>
        /// دریافت کلاسهای ترمی خاص
        /// </summary>
        /// <param name="termCode">کد ترم مد نظر برای مثال 96-97-2</param>
        /// <returns></returns>

        [HttpGet("{termCode}")]
        public ActionResult<ApiResponse> Get(string termCode)
        {
            var resualt =
                ClientHelper.GetValue<EducationalClassServiceBaseModel>(_baseServiceConfig.EducationalClassRelativeAddress + $"?term={termCode}",
                    _baseServiceConfig);

            return new ApiResponse
            {
                ErrorId = resualt == null || resualt.Count <= 0
                    ? (int)Helper.ApiOutputError.NotExists
                    : (int)Helper.ApiOutputError.NoError,
                ErrorTitle = resualt == null || resualt.Count <= 0
                    ? ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString()
                    : ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                Result = resualt.SkipWhile(x => x.ClassCode == 0)
            };
        }

        /// <summary>
        /// دریافت کلاس های براساس رشته
        /// </summary>
        /// <param name="fieldId">کد رشته</param>
        /// <returns></returns>
        [HttpGet("fieldId/{fieldId}")]
        public ActionResult<ApiResponse> GetByFieldId(decimal fieldId)
        {
            
            var resualt =
                ClientHelper.GetValue<EducationalClassServiceBaseModel>(_baseServiceConfig.EducationalClassWithFieldRelativeAddress
                        .Replace("{fieldId}", fieldId.ToString(CultureInfo.InvariantCulture))
                        .Replace("{codeTerm}", Helper.GetCurrentTerm(_baseServiceConfig)),
                    _baseServiceConfig);

            return new ApiResponse
            {
                ErrorId = resualt == null || resualt.Count <= 0
                    ? (int)Helper.ApiOutputError.NotExists
                    : (int)Helper.ApiOutputError.NoError,
                ErrorTitle = resualt == null || resualt.Count <= 0
                    ? ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString()
                    : ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                Result = resualt.SkipWhile(x => x.ClassCode == 0)
            };
        }
        /// <summary>
        /// دریافت کلاس های براساس گروه
        /// </summary>
        /// <param name="fieldId">کد گروه</param>
        /// <returns></returns>
        [HttpGet("groupId/{groupId}")]
        public ActionResult<ApiResponse> GetByGroupId(decimal groupId)
        {
            var resualt =
                ClientHelper.GetValue<EducationalClassServiceBaseModel>(_baseServiceConfig.EducationalClassWithGroupRelativeAddress
                        .Replace("{groupId}", groupId.ToString(CultureInfo.InvariantCulture))
                        .Replace("{codeTerm}", Helper.GetCurrentTerm(_baseServiceConfig)),
                    _baseServiceConfig);

            return new ApiResponse
            {
                ErrorId = resualt == null || resualt.Count <= 0
                    ? (int)Helper.ApiOutputError.NotExists
                    : (int)Helper.ApiOutputError.NoError,
                ErrorTitle = resualt == null || resualt.Count <= 0
                    ? ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString()
                    : ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                Result = resualt.SkipWhile(x => x.ClassCode == 0)
            };
        }

    }
}