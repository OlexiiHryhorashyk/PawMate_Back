using AutoMapper;
using Domain.Entities;
using Models.InvoiceDTOs;
using Models.UserDTOs;

namespace Services.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDTO>();

            CreateMap<UpdateUserDTO, User>();

            CreateMap<CreateUserDTO, User>();

            CreateMap<Invoice, InvoiceDTO>()
                .ForMember(dto => dto.Likes, g => g.MapFrom(i => i.Likes.Count))
                .ForMember(dto => dto.UserName, g => g.MapFrom(i => $"{i.User.Name} {i.User.Surname}"))
                .ForMember(dto => dto.UserNumber, g => g.MapFrom(i => i.User.Number));

            CreateMap<CreateInvoiceDTO, Invoice>();

            CreateMap<UpdateInvoiceDTO, Invoice>();
        }
    }
}
