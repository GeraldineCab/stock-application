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
                config.CreateMap<MessageDto, Message>()
                    .ReverseMap());

            return config.CreateMapper();
        }
    }
}
