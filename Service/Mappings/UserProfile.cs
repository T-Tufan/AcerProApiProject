using AutoMapper;
using Dtos.UserDtos;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {

            CreateMap<UserCreateDto, AppUser>().
                ForMember(x => x.UserName, dto => dto.MapFrom(x => x.UserName)).
                ForMember(x => x.Gender, dto => dto.MapFrom(x => x.Gender)).
                ForMember(x => x.Email, dto => dto.MapFrom(x => x.Email))
                .ReverseMap();
            //CreateMap<NewsListDto, NewsUpdateDto>().ReverseMap();
        }
    }
}
