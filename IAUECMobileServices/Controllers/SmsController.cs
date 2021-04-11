using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Utility;
using System.Web.Http;
using DAL;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IAUECMobileServices.Controllers
{


    [Route("api/v1/[controller]")]
    public class SmsController : Controller
    {

        private BaseServiceConfig _config;
        public SmsController(IOptions<BaseServiceConfig> options)
        {
            _config = options.Value;
        }
        /// <summary>
        /// ارسال کد تایید به کاربر
        /// </summary>
        /// <param name="objectType">
        ///  برای مشخص کردن نوع پروفایل 1:دانشجو 2: استاد 
        /// </param>
        /// <param name="code">کد دانشجویی یا استادی</param>
        /// <param name="randomcode">کد تایید</param>
        /// <returns></returns>
        [HttpGet("send/{objectType}/{code}/{randomNumber}")]
        public ActionResult<ApiResponse> Get(Helper.ObjectType objectType, string code, int randomNumber)
        {
            var smsRepository = new SmsRepository();
            string mobile;

            switch (objectType)
            {
                case Helper.ObjectType.Student:
                    var studentRepository = new StudentRepository();
                    mobile = studentRepository.Select(code).Mobile;

                    return new ApiResponse
                    {
                        ErrorId = mobile == null || mobile.Trim() == ""
                        ? (int)Helper.ApiOutputError.NotExists
                        : (int)Helper.ApiOutputError.NoError,

                        ErrorTitle = mobile == null || mobile.Trim() == ""
                        ? ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString()
                        : ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),

                        Result = $"{mobile}-{smsRepository.SendRandomNumber(mobile, randomNumber.ToString(), _config)}"
                    };

                case Helper.ObjectType.Professor:

                    var professorRepository = new ProfessorRepository();
                    mobile = professorRepository.Select(code).Mobile;

                    return new ApiResponse
                    {
                        ErrorId = mobile == null || mobile.Trim() == ""
                            ? (int)Helper.ApiOutputError.NotExists
                            : (int)Helper.ApiOutputError.NoError,

                        ErrorTitle = mobile == null || mobile.Trim() == ""
                            ? ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString()
                            : ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),

                        Result = $"{mobile}-{smsRepository.SendRandomNumber(mobile, randomNumber.ToString(), _config)}"
                    };
                default:
                    return new ApiResponse();
            }
        }


        /// <summary>
        /// ارسال کد تایید به کاربر
        /// </summary>
        /// <param name="mobile">شماره موبایل</param>
        /// <param name="randomNumber">کد تایید</param>
        /// <returns></returns>
        [HttpGet("send/{mobile}/{randomNumber}")]
        public ActionResult<ApiResponse> Get(string mobile, int randomNumber)
        {
            var smsRepository = new SmsRepository();


            if(Regex.IsMatch(mobile, "^[0-9]*$") && Regex.IsMatch(randomNumber.ToString(), "^[0-9]*$"))
            return new ApiResponse
            {
                ErrorId = mobile == null || mobile.Trim() == "" 
                          
                ? (int)Helper.ApiOutputError.NotExists
                : (int)Helper.ApiOutputError.NoError,

                ErrorTitle = mobile == null || mobile.Trim() == ""
                ? ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString()
                : ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),

                Result = $"{mobile}-{smsRepository.SendRandomNumber(mobile, randomNumber.ToString(), _config)}"
            };
            else
                return new ApiResponse
                {
                    ErrorId = (int)Helper.ApiOutputError.BadRequest,

                    ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.BadRequest).ToString(),
                    
                };




        }

    }
}
