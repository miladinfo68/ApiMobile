using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Model;
using Utility;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IAUECMobileServices.Controllers
{
    [Route("api/v1/[controller]")]
    public class PasswordController : Controller
    {
        private BaseServiceConfig _config;
        public PasswordController(IOptions<BaseServiceConfig> options)
        {
            _config = options.Value;
        }
        /// <summary>
        /// درخواست تغییر رمز عبور تو سط کاربر
        /// </summary>
        /// <param name="objectType">
        ///  برای مشخص کردن نوع پروفایل 1:دانشجو 2: استاد 
        /// </param>
        /// <param name="code">کد دانشجویی یا استادی</param>
        /// <returns></returns>
        [HttpGet("changerequest/{objectType}/{code}")]

        public ActionResult<ApiResponse> Get(Helper.ObjectType objectType, string code)
        {
            switch (objectType)
            {
                case Helper.ObjectType.Student:
                    var studentRepo = new StudentRepository();
                    var student = studentRepo.Select(code);
                    if (student.StudentCode != null)
                    {
                        var smsRepository = new SmsRepository();
                        var randomNumber = smsRepository.GetRandomNumber(student.Mobile);
                        return new ApiResponse
                        {
                            ErrorId = student.Mobile == null || student.Mobile.Trim() == ""
                                ? (int)Helper.ApiOutputError.NotExists
                                : (int)Helper.ApiOutputError.NoError,

                            ErrorTitle = student.Mobile == null || student.Mobile.Trim() == ""
                                ? ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString()
                                : ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),

                            Result = $"{student.Mobile}-{ smsRepository.SendRandomNumber(student.Mobile, randomNumber, _config)}"
                        };
                    }
                    else
                    {
                        return new ApiResponse
                        {
                            ErrorId = (int)Helper.ApiOutputError.NotExists,
                            ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString(),

                        };
                    }

                case Helper.ObjectType.Professor:
                    var professorRepository = new ProfessorRepository();
                    var professor = professorRepository.Select(code);
                    if (professor.ProfessorCode != null)
                    {
                        var smsRepository = new SmsRepository();
                        var randomNumber = smsRepository.GetRandomNumber(professor.Mobile);
                        return new ApiResponse
                        {
                            ErrorId = professor.Mobile == null || professor.Mobile.Trim() == ""
                                ? (int)Helper.ApiOutputError.NotExists
                                : (int)Helper.ApiOutputError.NoError,

                            ErrorTitle = professor.Mobile == null || professor.Mobile.Trim() == ""
                                ? ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString()
                                : ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),

                            Result = $"{professor.Mobile}-{ smsRepository.SendRandomNumber(professor.Mobile, randomNumber, _config)}"
                        };
                    }
                    else
                    {
                        return new ApiResponse
                        {
                            ErrorId = (int)Helper.ApiOutputError.NotExists,
                            ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString(),

                        };
                    }


                default:
                    return new ApiResponse
                    {
                        ErrorId = (int)Helper.ApiOutputError.NotExists,
                        ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString(),

                    };
            }

        }


        /// <summary>
        /// تغییر رمز عبور توسط کاربر
        /// </summary>
        /// <param name="objectType">
        ///  برای مشخص کردن نوع پروفایل 1:دانشجو 2: استاد 
        /// </param>
        /// <param name="code">کد دانشجویی یا استادی</param>
        /// <param name="password">رمز عبور</param>
        /// <param name="randomcode">کد تایید</param>
        /// <returns></returns>
        [HttpGet("change/{objectType}/{code}/{password}/{randomcode}")]

        public ActionResult<ApiResponse> Get(Helper.ObjectType objectType, string code, string password, string randomcode)
        {
            switch (objectType)
            {
                case Helper.ObjectType.Student:
                    var studentRepo = new StudentRepository();
                    var student = studentRepo.Select(code);
                    if (student.StudentCode != null)
                    {
                        var smsRepository = new SmsRepository();
                        var checkRandomnumberResult = smsRepository.CheckRandomNumber(student.Mobile, randomcode);

                        //var studentNewPass = this.GetNewPassword(student.StudentCode ,"");
                        var UrlChangePassForStudent_InBaseService = _config.BaseServiceUrlChangePasswordStudent + $"{student.StudentCode}/{password}";

                        var resualt1 = ClientHelper.GetValue<bool>(UrlChangePassForStudent_InBaseService, _config).FirstOrDefault();

                        if (checkRandomnumberResult)
                            return new ApiResponse
                            {
                                ErrorId = student.Mobile == null || student.Mobile.Trim() == ""
                                    ? (int)Helper.ApiOutputError.NotExists
                                    : (int)Helper.ApiOutputError.NoError,

                                ErrorTitle = student.Mobile == null || student.Mobile.Trim() == ""
                                    ? ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString()
                                    : ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),

                                Result = studentRepo.ChangePassword(student.StudentCode, password) && resualt1
                            };
                        else
                        {
                            return new ApiResponse
                            {
                                ErrorId = (int)Helper.ApiOutputError.InCorrectRandomNumber,
                                ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.InCorrectRandomNumber).ToString(),

                            };
                        }
                    }
                    else
                    {
                        return new ApiResponse
                        {
                            ErrorId = (int)Helper.ApiOutputError.NotExists,
                            ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString(),

                        };
                    }

                case Helper.ObjectType.Professor:
                    var professorRepository = new ProfessorRepository();
                    var professor = professorRepository.Select(code);
                    if (professor.ProfessorCode != null)
                    {
                        var smsRepository = new SmsRepository();
                        var checkRandomnumberResult = smsRepository.CheckRandomNumber(professor.Mobile, randomcode);

                        //var profNewPass = this.GetNewPassword("",professor.ProfessorCode);
                        var UrlChangePassForTeacher_InBaseService = _config.BaseServiceUrlChangePasswordTeacher + $"{professor.ProfessorCode}/{password}";

                        var resualt2 = ClientHelper.GetValue<bool>(UrlChangePassForTeacher_InBaseService, _config).FirstOrDefault();

                        return new ApiResponse
                        {
                            ErrorId = professor.Mobile == null || professor.Mobile.Trim() == ""
                                ? (int)Helper.ApiOutputError.NotExists
                                : (int)Helper.ApiOutputError.NoError,

                            ErrorTitle = professor.Mobile == null || professor.Mobile.Trim() == ""
                                ? ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString()
                                : ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),

                            Result = professorRepository.ChangePassword(professor.ProfessorCode, password) && resualt2
                        };
                    }
                    else
                    {
                        return new ApiResponse
                        {
                            ErrorId = (int)Helper.ApiOutputError.NotExists,
                            ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString(),

                        };
                    }

                default:
                    return new ApiResponse
                    {
                        ErrorId = (int)Helper.ApiOutputError.NotExists,
                        ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString(),

                    };
            }
        }


        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@


        //public string GetNewPassword(string studentCode = "", string professorCode = "")
        //{
        //    string res = "";

        //    if (!string.IsNullOrEmpty(studentCode))
        //    {
        //        //res = studentCode.EncryptPass();
        //        using (var con = new SqlConnection(StaticValue.SupplementaryConnectionString))
        //        {
        //            var resualt = con.Query(StaticValue.SpGetStudentPassword, new { studentCode = studentCode }, commandType: CommandType.StoredProcedure).AsList();

        //            //ms.ctrl_elecAsync(studentCode, password, "1", "0", "97iauec1206");
        //            res = resualt[0]["Password"];
        //        }

        //    }
        //    else if (!string.IsNullOrEmpty(professorCode))
        //    {
        //        //res = professorCode.EncryptPass();
        //        using (var con = new SqlConnection(StaticValue.SupplementaryConnectionString))
        //        {
        //            var resualt = con.Query(StaticValue.SpCheangeStudentPassword, new { professorCode = professorCode }, commandType: CommandType.StoredProcedure).AsList();

        //            //ms.ctrl_elecAsync(studentCode, password, "1", "0", "97iauec1206");
        //            res = resualt[0]["Password"];
        //        }
        //    }
        //    return res;

        //}

        //public string GetNewPassword2(string studentCode = "", string ostCode = "")
        //{
        //    string res = "";

        //    if (!string.IsNullOrEmpty(studentCode))
        //    {
        //        res = studentCode.EncryptPass();
        //    }
        //    else if (!string.IsNullOrEmpty(ostCode))
        //    {
        //        res = ostCode.EncryptPass();
        //    }
        //    return res;
        //}


    }
}
