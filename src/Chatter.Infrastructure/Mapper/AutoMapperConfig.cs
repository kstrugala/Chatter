using AutoMapper;
using Chatter.Core.Entities;
using Chatter.Infrastructure.Responses.V1;

namespace Chatter.Infrastructure.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<JsonWebToken, JsonWebTokenDto>();
            }).CreateMapper();
    }
}
