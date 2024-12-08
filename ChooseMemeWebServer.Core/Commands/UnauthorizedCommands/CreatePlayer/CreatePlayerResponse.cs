using ChooseMemeWebServer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Core.Commands.UnauthorizedCommands.CreatePlayer
{
    public class CreatePlayerResponse
    {
        public Player Player { get; set; } = null!;
    }
}
