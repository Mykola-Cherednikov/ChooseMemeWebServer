using ChooseMemeWebServer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Core.Interfaces
{
    public interface IGameService
    {
        public void CreateGame(Lobby lobby);

        public void HandleGame();
    }
}
