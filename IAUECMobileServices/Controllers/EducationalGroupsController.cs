using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model;
using Model.BaseServiceModels;
using NuGet.Frameworks;
using Utility;

namespace IAUECMobileServices.Controllers
{
    [Route("api/v1/[controller]")]
    public class EducationalGroupsController : Controller
    {

        private readonly BaseServiceConfig _baseServiceConfig;
        public EducationalGroupsController(IOptions<BaseServiceConfig> options)
        {
            _baseServiceConfig = options.Value;
        }
        /// <summary>
        /// دریافت گروههای آموزشی
        /// </summary>

        [HttpGet("")]
        public ActionResult<ApiResponse> Get()
        {
            var resualt =
                ClientHelper.GetValue<EducationalGroupServiceBaseModel>(_baseServiceConfig.EducationalGroupRelativeAddress,
                    _baseServiceConfig);

            return new ApiResponse
            {
                ErrorId = resualt == null || resualt.Count <= 0
                    ? (int)Helper.ApiOutputError.NotExists
                    : (int)Helper.ApiOutputError.NoError,
                ErrorTitle = resualt == null || resualt.Count <= 0
                    ? ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString()
                    : ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                Result = resualt.SkipWhile(x => x.Id == 0)
            };
        }


        /// <summary>
        /// دریافت گروههای آموزشی ترم جاری
        /// </summary>

        [HttpGet("currentTerm")]
        public ActionResult<ApiResponse> GetCurrentTerm()
        {
            var resualt =
                ClientHelper.GetValue<EducationalGroupServiceBaseModel>(_baseServiceConfig.EducationalGroupCurrentTermRelativeAddress + Helper.GetCurrentTerm(_baseServiceConfig),
                    _baseServiceConfig);

            return new ApiResponse
            {
                ErrorId = resualt == null || resualt.Count <= 0
                    ? (int)Helper.ApiOutputError.NotExists
                    : (int)Helper.ApiOutputError.NoError,
                ErrorTitle = resualt == null || resualt.Count <= 0
                    ? ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString()
                    : ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                Result = resualt.SkipWhile(x => x.Id == 0)
            };
        }

    }
}