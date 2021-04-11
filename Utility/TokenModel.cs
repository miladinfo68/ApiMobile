using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class TokenModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
    }
}
