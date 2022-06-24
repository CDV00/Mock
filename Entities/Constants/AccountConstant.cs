using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Constants
{
    public static class Provider
    {
        public const string GOOGLE = "GOOGLE";
        public const string FACEBOOK = "FACEBOOK";

    }
    public static class WorkingMail
    {
        public const string Register = "REGISTER";
        public const string SubjectsRegister = "CONFIRM REGISTRATION";
        //public const string Message = "<h2>Your verification code: </h2>";

        public const string ForgetPassword = "FORGET PASSWORD";

        public static string Message(string codeNumber)
        {
            return $"<h2 style=\"text-align: center;\" >Your verification code: <b style=\"background-color:powderblue;font-size:40px;\">{codeNumber}</b> </h2>";
        }
    }
}
