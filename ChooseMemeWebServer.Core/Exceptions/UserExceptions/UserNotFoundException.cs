using ChooseMemeWebServer.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Core.Exceptions.UserExceptions
{
    public class UserNotFoundException : ExpectedException
    {
        public UserNotFoundException() : base("User not found")
        {
        }
    }
}
