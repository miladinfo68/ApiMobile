
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model.BaseServiceModels;
using Utility;

namespace IAUECMobileServices.Controllers
{
    [Route("api/v1/[controller]")]
    //[ApiController]
    public class RolesController : Controller
    {
        //private readonly BaseServiceConfig _baseServiceConfig;
        private readonly RolesRepository roleRepository;
        public RolesController( /*IOptions<BaseServiceConfig> options */)
        {
            //_baseServiceConfig = options.Value;
            roleRepository = roleRepository ?? new RolesRepository();
        }
      

        /// <summary>
        /// لیست نقش ها
        /// </summary>
        /// <returns></returns>

        [HttpGet("")] 
        public ActionResult<ApiResponse> Get()
        {
            var resualt = roleRepository.SelectAll();
            return new ApiResponse
            {
                ErrorId = resualt == null || resualt.Count <= 0
                    ? (int)Helper.ApiOutputError.NotExists
                    : (int)Helper.ApiOutputError.NoError,
                ErrorTitle = resualt == null || resualt.Count <= 0
                    ? ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString()
                    : ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                Result = resualt
            };
        }


        /// <summary>
        /// نام نقش بر اساس شناسه نقش
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("roleId/{roleId}")] 
        public ActionResult<ApiResponse> GetRoleNameByRoleId(int roleId)
        {           
            var resualt = roleRepository.SelectOne(roleId);
            return new ApiResponse
            {
                ErrorId = resualt == null
                    ? (int)Helper.ApiOutputError.NotExists
                    : (int)Helper.ApiOutputError.NoError,
                ErrorTitle = resualt == null
                    ? ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString()
                    : ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                Result = resualt
            };
        }
    }
}