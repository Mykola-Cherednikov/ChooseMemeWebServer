using AutoMapper;

namespace ChooseMemeWebServer.Core.Common.Mappings
{
    internal interface IMapWith<T>
    {
        void Mapping(Profile profile)
        {
            {
                profile.AllowNullCollections = true;
                profile.CreateMap(typeof(T), GetType());
            }
        }
    }
}
