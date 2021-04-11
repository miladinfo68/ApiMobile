
using System.Collections.Generic;
using Utility;

namespace Model
{
    public class Employee:IEmployee
    {
        public string UserName { get; set; }//empCode
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public int RoleId { get; set; }
        public bool Active { get; set; } = false;
       
    }
}
