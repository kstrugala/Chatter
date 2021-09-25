using AutoMapper;
using Chatter.Core.Entities;
using Chatter.Infrastructure.Responses.V1.Posts;
using Chatter.Infrastructure.Responses.V1.Users;

namespace Chatter.Infrastructure.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<JsonWebToken, JsonWebTokenDto>();
                cfg.CreateMap<Post, PostDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UniqueId));
            }).CreateMapper();
    }
}
