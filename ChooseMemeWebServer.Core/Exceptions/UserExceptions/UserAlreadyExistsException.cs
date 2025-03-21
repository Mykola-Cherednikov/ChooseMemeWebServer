using ChooseMemeWebServer.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Core.Exceptions.UserExceptions
{
    public class UserAlreadyExistsException : ExpectedException
    {
        public UserAlreadyExistsException() : base("User already exists")
        {
        }
    }
}
