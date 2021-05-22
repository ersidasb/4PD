using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4PD
{
    class Password
    {
        public string passName { get; set; }
        public string pass { get; set; }
        public string passURL { get; set; }
        public string comment { get; set; }

        public Password(string passName, string pass, string passURL, string comment)
        {
            this.passName = passName;
            this.pass = pass;
            this.passURL = passURL;
            this.comment = comment;
        }
    }
}
