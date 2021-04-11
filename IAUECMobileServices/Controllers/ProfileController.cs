using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Model;
using Utility;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IAUECMobileServices.Controllers
{

    [Route("api/v1/[controller]")]
    public class ProfileController : Controller
    {

        /// <summary>
        /// دریافت پروفایل کاربران
        /// </summary>

        /// <param name="code">کد دانشجویی یا استادی یا کارمندی</param>
        /// <param name="password">رمز عبور</param>
        /// <returns>NoError = 0,InCorrectPassword = 1,NotExists = 2,InCorrectRandomNumber=3</returns>
        [HttpGet("{code}/{password}")]
        public ActionResult<ApiResponse> Get(string code, string password)
        {

            //studentCode and profCode are numeric string
            //employeeCode is string not numeric

            var trimedCode = code.Trim();
            var trimedPass = password.Trim();

            double inputCode;
            var isNum_Prof_Stu_Code = double.TryParse(trimedCode, out inputCode);

            if (isNum_Prof_Stu_Code)
            {
                var studentRepo = new StudentRepository();
                var fsfStudent = studentRepo.Select(trimedCode);
                if (fsfStudent != null)
                {
                    var checkPassword = studentRepo.CheckPassword(trimedCode, trimedPass);

                    if (checkPassword)
                    {
                        fsfStudent.ObjectType = Helper.ObjectType.Student;
                        return new ApiResponse
                        {
                            ErrorId = (int)Helper.ApiOutputError.NoError,
                            ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                            Result = fsfStudent
                        };
                    }
                    else
                    {
                        return new ApiResponse
                        {
                            ErrorId = (int)Helper.ApiOutputError.InCorrectPassword,
                            ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.InCorrectPassword).ToString()
                        };
                    }
                }
                else
                {
                    var fNewStudent = studentRepo.Select(trimedCode,false,true);
                    if (fNewStudent != null)
                    {
                        if (string.Compare(trimedPass, fNewStudent.NationalCode)==0)
                        {
                            fNewStudent.ObjectType = Helper.ObjectType.Student;
                            return new ApiResponse
                            {
                                ErrorId = (int)Helper.ApiOutputError.NoError,
                                ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                                Result = fNewStudent
                            };
                        }
                        else
                        {
                            return new ApiResponse
                            {
                                ErrorId = (int)Helper.ApiOutputError.InCorrectPassword,
                                ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.InCorrectPassword).ToString()
                            };
                        }
                    }
                }

                //======================================================
                var professorRepo = new ProfessorRepository();
                var professor = professorRepo.Select(trimedCode);
                if (professor != null)
                {
                    var checkPassword = professorRepo.CheckPassword(trimedCode, trimedPass);

                    if (checkPassword)
                    {
                        professor.ObjectType = Helper.ObjectType.Professor;
                        return new ApiResponse
                        {
                            ErrorId = (int)Helper.ApiOutputError.NoError,
                            ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                            Result = professor
                        };
                    }
                    else
                    {
                        return new ApiResponse
                        {
                            ErrorId = (int)Helper.ApiOutputError.InCorrectUserNameOrPassword,
                            ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.InCorrectPassword).ToString()
                        };
                    }
                }


            }
            //#############################################

            var employeeRepository = new EmployeeRepository();
            var employees = employeeRepository.Select(trimedCode);
            if (employees != null )
            {
                var checkPassword = employeeRepository.CheckPassword(trimedCode, trimedPass);

                if (checkPassword)
                {   
                    return new ApiResponse
                    {
                        ErrorId = (int)Helper.ApiOutputError.NoError,
                        ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                        Result = employees
                    };
                }
                else
                {
                    return new ApiResponse
                    {
                        ErrorId = (int)Helper.ApiOutputError.InCorrectPassword,
                        ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.InCorrectPassword).ToString()
                    };
                }
            }
            //#############################################
            return new ApiResponse
            {
                ErrorId = (int)Helper.ApiOutputError.NotExists,
                ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString(),
                Result = "default"
            };
        }



        /// <summary>
        /// دریافت پروفایل کاربران
        /// </summary>

        /// <param name="code">کد دانشجویی یا استادی</param>
        /// <returns>NoError = 0,InCorrectPassword = 1,NotExists = 2,InCorrectRandomNumber=3</returns>
        [HttpGet("checkactive/{code}")]
        public ActionResult<ApiResponse> Get(string code)
        {

            var studentRepo = new StudentRepository();
            var student = studentRepo.Select(code);
            if (student != null)
            {

                return new ApiResponse
                {
                    ErrorId = (int)Helper.ApiOutputError.NoError,
                    ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                    Result = student.Active
                };

            }

            var professorRepo = new ProfessorRepository();
            var professor = professorRepo.Select(code);
            if (professor != null)
            {
                student.ObjectType = Helper.ObjectType.Professor;
                return new ApiResponse
                {
                    ErrorId = (int)Helper.ApiOutputError.NoError,
                    ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                    Result = professor.Active
                };
            }


            return new ApiResponse
            {
                ErrorId = (int)Helper.ApiOutputError.NotExists,
                ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString(),
                Result = "default"
            };
        }

        /// <summary>
        /// دریافت تصویر پروفایل
        /// </summary>
        /// <param name="code">کد دانشجویی یا استادی</param>
        /// <returns></returns>
        [HttpGet("{code}/picture")]
        public ActionResult<ApiResponse> GetPicture(string code)
        {
            var studentRepo = new StudentRepository();
            var studentPicture = studentRepo.Select(code, true)?.Picture;
            if (studentPicture != null)
            {

                return new ApiResponse
                {
                    ErrorId = (int)Helper.ApiOutputError.NoError,
                    ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                    Result = studentPicture
                };

            }

            var professorRepo = new ProfessorRepository();
            var professorPicture = professorRepo.Select(code, true)?.Picture;
            if (professorPicture != null)
            {

                return new ApiResponse
                {
                    ErrorId = (int)Helper.ApiOutputError.NoError,
                    ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                    Result = professorPicture
                };
            }




            return new ApiResponse
            {
                ErrorId = (int)Helper.ApiOutputError.NotExists,
                ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString(),
                Result = "default"
            };
        }
    }


}

