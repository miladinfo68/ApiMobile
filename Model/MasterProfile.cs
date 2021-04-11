
using Model;
using static Utility.Helper;

public class MasterProfile 
    {
        public ProfileType Type { get; set; }
        public IProfile Profile { get; set; }
    }

    public class ApiResponse
    {
        public ApiResponse()
        {            
        }

        public ApiResponse(int errorId,string erorrTitle,object result)
        {
            ErrorId = errorId;
            ErrorTitle = erorrTitle;
            Result = result;
        }
        public int ErrorId { get; set; }
        public string ErrorTitle { get; set; }
        public object Result { get; set; } 
    }
