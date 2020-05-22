using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCLearn.Models.Dto
{
    public class Subscription
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class SignIn
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
