using DAL;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Threading.Tasks;
using Utility;

namespace IAUECMobileServices.Controllers
{
    [Route("api/[controller]")]
    public class TicketController : Controller
    {
        //Get Ticket User Information
        [HttpGet("users/{userId}")]
        public async Task<ApiResponse> GetUser(string userId)
        {
            var isNumeric =int.TryParse(userId, out int resualt);
            if (isNumeric)
            {


                var studentRepo = new StudentRepository();
                var student = studentRepo.Select(studentCode: userId);
                var professorRepo = new ProfessorRepository();
                var professor = professorRepo.Select(professorCode: userId);

                if (!(student is null))
                    return new ApiResponse
                    {
                        ErrorId = (int)Helper.ApiOutputError.NoError,
                        ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                        Result = student
                    };
                if (!(professor is null))
                    return new ApiResponse
                    {
                        ErrorId = (int)Helper.ApiOutputError.NoError,
                        ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                        Result = professor
                    };
            }
            return new ApiResponse
            {
                ErrorId = (int)Helper.ApiOutputError.NotExists,
                ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString(),
                Result = null
            };
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        [HttpGet("usersState/{userId}")]
        public async Task<ApiResponse> GetStateUser(string userId)
        {
            var isNumeric = int.TryParse(userId, out int resualt);

            try
            {


            if (isNumeric)
            {


                var studentRepo = new StudentRepository();
                var student = studentRepo.Select(studentCode: userId);
                var professorRepo = new ProfessorRepository();
                var professor = professorRepo.Select(professorCode: userId);

                if (!(student is null))
                {
                    var studentState = studentRepo.GetStateForTickets(studentCode: userId);
                    return new ApiResponse
                    {
                        ErrorId = (int)Helper.ApiOutputError.NoError,
                        ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                        Result = studentState
                    };
                }
                if (!(professor is null))
                {
                    var professorState = new UserTicketState();
                    return new ApiResponse
                    {
                        ErrorId = (int)Helper.ApiOutputError.NoError,
                        ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NoError).ToString(),
                        Result = professorState
                    };
                }
            }
            return new ApiResponse
            {
                ErrorId = (int)Helper.ApiOutputError.NotExists,
                ErrorTitle = ((Helper.ApiOutputError)Helper.ApiOutputError.NotExists).ToString(),
                Result = null
            };
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    }
}