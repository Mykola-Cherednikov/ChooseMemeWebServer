using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface IFeatureService
    {
        public Task ClearUnusedMedia();
    }
}
