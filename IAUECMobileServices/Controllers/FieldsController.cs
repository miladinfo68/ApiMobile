using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model;
using Model.BaseServiceModels;
using Utility;

namespace IAUECMobileServices.Controllers
{
    [Route("api/v1/[controller]")]
    public class FieldsController : Controller
    {

        private readonly BaseServiceConfig _baseServiceConfig;
        public FieldsController(IOptions<BaseServiceConfig> options)
        {
            _baseServiceConfig = options.Value;
        }

        /// <summary>
        /// دریافت رشته ها
        /// </summary>

        [HttpGet("")] 
        public ActionResult<ApiResponse> Get()
        {
            //FieldRelativeAddress="/api/field";
            var resualt = ClientHelper.GetValue<FieldServiceBaseModel>(_baseServiceConfig.FieldRelativeAddress, _baseServiceConfig);


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
        /// لیست رشته ها بر اساس گروه درسی
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("groupid/{groupid}")] 
        public ActionResult<ApiResponse> GetFieldsByGroupId(int groupid)
        {
            //FieldsByGroupIdRelativeAddress="/api/fields/groupid/";
            var url = $"{ _baseServiceConfig.FieldsByGroupIdRelativeAddress}/{groupid}";
            var resualt = ClientHelper.GetValue<FieldServiceBaseModel>(url, _baseServiceConfig);


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