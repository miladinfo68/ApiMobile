using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model;
using Model.BaseServiceModels;
using Utility;

namespace IAUECMobileServices.Controllers
{
    [Route("api/v1/[controller]")]
    public class TermsController : ControllerBase
    {

        private readonly BaseServiceConfig _baseServiceConfig;
        public TermsController(IOptions<BaseServiceConfig> options)
        {
            _baseServiceConfig = options.Value;
        }

        /// <summary>
        /// دریافت ترم ها
        /// </summary>

        [HttpGet("")]
        public ActionResult<ApiResponse> Get()
        {
            var resualt = ClientHelper.GetValue<TermServiceBaseModel>(_baseServiceConfig.TermRelativeAddress, _baseServiceConfig);

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