using System.Collections.Generic;
using Utility;

namespace DTO
{
    public class EmployeeDTO
    {
        public string UserName { get; set; }//empCode
        //public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string RoleIds { get; set; }
        public bool Active { get; set; } 
        public Helper.ObjectType ObjectType { get; set; }
    }
}
