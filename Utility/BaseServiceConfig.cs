using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class BaseServiceConfig
    {
        public string BaseServiceEndPoint { get; set; }
        public string BaseServiceUserName { get; set; }
        public string BaseServicePassword { get; set; }
        public string BaseServiceUrlChangePasswordStudent { get; set; }
        public string BaseServiceUrlChangePasswordTeacher { get; set; }

        public List<string> IneligibleProfessorCodes { get; set; }
        public List<decimal> IneligibleEducationalGroupCodes { get; set; }
        public string CollegeRelativeAddress { get; set; }
        public string TokenEndPoint { get; set; }
        public string EducationalGroupRelativeAddress { get; set; }
        public string EducationalGroupCurrentTermRelativeAddress { get; set; }
        public string FieldRelativeAddress { get; set; }
        public string FieldsByGroupIdRelativeAddress { get; set; }       

        public string EducationalClassRelativeAddress { get; set; }
        public string EducationalClassWithFieldRelativeAddress { get; set; }
        public string EducationalClassWithGroupRelativeAddress { get; set; }
        public string StudentEducationalClass { get; set; }
        public string ProfessorRelativeAddress { get; set; }
        public string TermRelativeAddress { get; set; }
        public string CurrentTermRelativeAddress { get; set; }
        public string SendRandomNumberRelativeAddress { get; set; }

    }
}
