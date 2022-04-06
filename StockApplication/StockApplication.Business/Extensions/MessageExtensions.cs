using AutoMapper;
using StockApplication.Dto;
using StockApplication.Persistence.Entities;

namespace StockApplication.Business.Extensions
{
    public class MessageExtensions : Profile
    {
        public IMapper Mapper => _mapper;

        private static readonly IMapper _mapper = InitializeMapper();

        private static IMapper InitializeMapper()
        {
            var config = new MapperConfiguration(config =>
            {
                config.CreateMap<UserDto, User>()
                    .ForMember(dest => dest.Name,
                        opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.IsBot,
                        opt => opt.MapFrom(src => src.IsBot))
                    .ForAllMembers(m => m.Ignore());
                config.CreateMap<User, UserDto>()
                    .ForMember(dest => dest.Name,
                        opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.IsBot,
                        opt => opt.MapFrom(src => src.IsBot))
                    .ForAllMembers(m => m.Ignore());
                config.CreateMap<MessageDto, Message>().ReverseMap();
            });
           
            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
    }
}
