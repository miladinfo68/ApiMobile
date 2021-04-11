using System;

namespace Utility
{
    public static class StaticValue
    {
        public static string SupplementaryConnectionString => "Server=192.168.1.8;Database=Supplementary;User Id=mobileUser;Password=$Mobile#%@9812elibom*%^&%$$jfchjkf;";
        //public static string SupplementaryConnectionString => "Server=192.168.1.21;Database=Supplementary;User Id=teamuser;Password=t123*t456;";
        public static string SpSelectStudentByStudentCode => "[Mobile].[SP_SelectStudentByStudentCode]";
        public static string SpSelectProfessorByProfessorCode => "[Mobile].[SP_SelectProfessorByProfessorCode]";
        //------------------------
        public static string SpSelectEmployeeByEmployeeCode => "[Mobile].[SP_SelectEmployeeByEmployeeCode]";
        public static string SpSelectEmployeeByEmployeeCodeFromFnewStudent => "[Mobile].[SP_SelectStudentByStudentCode_InfNewStudent]";
      
        public static string SpSelectRoles => "[Mobile].[SP_SelectRoles]";
        public static string SpGetEmployeePassword => "[Mobile].[SP_GetEmployeePassword]";
        public static string SpChangeEmployeePassword => "[Mobile].[SP_ChangeEmployeePassword]";
        //------------------------
        //public static string SpCheckStudentPassword => "[Mobile].[SP_CheckStudentPassword]";
        public static string SpGetStudentPassword => "[Mobile].[SP_GetStudentPassword]";
        public static string SpGetProfessorPassword => "[Mobile].[SP_GetProfessorPassword]";
        public static string SpCheangeStudentPassword => "[Mobile].[SP_CheangeStudentPassword]";
     
        public static string SpCheangeProfessorPassword => "[Mobile].[SP_CheangeProfessorPassword]";
        public static string SpGetUsers => "[Mobile].[SP_GetUsers]";
        public static string SpGetRandomNumber => "[Mobile].[SP_GetRandomNumber]";
        public static string SpCheckRandomNumber => "[Mobile].[SP_CheckRandomNumber]";
        public static string SpSelectClassesByFieldId => "[Mobile].[SP_GetClassesByFieldId]";
        public static string SpSelectClassesByGroupId => "[Mobile].[SP_GetClassesByGroupId]";

        public static string CurrentTermRelativeAddress => "/api/term/currentTerm";

        public static string SpSelectStudentStateTickeByStudentCode => "[Mobile].[SP_SelectStudentStateTicketByStudentCode]";

        //public static string BaseServiceDomain => "http://localhost:63543/";
        //public static string BaseServiceDomain => "http://192.168.2.101:63543/";
        //public static string BaseServiceToken => "/token";
        //public static string BaseServiceUserName => "iauec@service";
        //public static string BaseServicePassword => "$client@1029%";
        //public static string SendRandomNumberRelativeAddress => "api/sms/send/activecode/";

    }
}
