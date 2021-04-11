using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model;
using Utility;

namespace IAUECMobileServices.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BaseServiceConfig _baseServiceConfig;
        public UsersController(IOptions<BaseServiceConfig> options)
        {
            _baseServiceConfig = options.Value;
        }

        /// <summary>
        /// دریافت دانشجویان و اساتید یک مجموعه خاص مانند دانشکده و گروه و غیره  
        /// </summary>
        /// <param name="collegeId">کد دانشکده: در صورتی که به این پارامتر مقدار نمی خواهید بدهید عدد 0 را وارد نمایید  </param>
        /// <param name="groupId">کد گروه: در صورتی که به این پارامتر مقدار نمی خواهید بدهید عدد 0 را وارد نمایید  </param>
        /// <param name="fieldId">کد رشته: در صورتی که به این پارامتر مقدار نمی خواهید بدهید عدد 0 را وارد نمایید  </param>
        /// <param name="lessonId">کد درس: در صورتی که به این پارامتر مقدار نمی خواهید بدهید عدد 0 را وارد نمایید  </param>
        /// <param name="educationalClassId">کد کلاس: در صورتی که به این پارامتر مقدار نمی خواهید بدهید عدد 0 را وارد نمایید  </param>
        /// <param name="objectTypeId"> استاد(2) یا دانشجو(1)1</param>
        /// <returns>لیست </returns>
        [HttpGet("StudentAndProfessor/{collegeId}/{groupId}/{fieldId}/{lessonId}/{educationalClassId}/{objectTypeId}")]
        public async Task<ApiResponse> Get(decimal collegeId, decimal groupId, decimal fieldId, decimal lessonId, decimal educationalClassId, int objectTypeId)
        {
            var userRepo = new UserRepository();
            var users = await userRepo.GetUsersAsync(
                collegeId,
                groupId,
                fieldId,
                lessonId,
                educationalClassId,
                objectTypeId,
                Helper.GetCurrentTerm(_baseServiceConfig));

            var res= new ApiResponse
            {
                ErrorId = (users ==null || users.Count()<=0) //!users.Any()
                    ? (int)Helper.ApiOutputError.NotExists
                    : (int)Helper.ApiOutputError.NoError,
                ErrorTitle = (users == null || users.Count() <= 0) //!users.Any()
                    ? ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString()
                    : ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                Result = (users !=null && users.Count()>0) ? users.SkipWhile(x => x == 0) :null
            };
            return res;
        }
    }
}