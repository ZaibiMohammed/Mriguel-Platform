using AutoMapper;

namespace Mriguel.Application.Common.Mappings
{
    /// <summary>
    /// Interface for types that can be mapped from a source type
    /// </summary>
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
