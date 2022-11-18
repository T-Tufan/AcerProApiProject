using AutoMapper;
using Dtos.NewsDtos;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mappings
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<News, NewsListDto>().ReverseMap();
            CreateMap<News, NewsCreateDto>().ReverseMap();
            CreateMap<NewsUpdateDto, News>().
                ForMember(x => x.ImagePath, dto => dto.MapFrom(x => x.ImagePath)).
                ForMember(x => x.Title, dto => dto.MapFrom(x => x.TitleArea)).
                ForMember(x => x.CreateTime, dto => dto.MapFrom(x => x.CreateTime)).
                ForMember(x => x.Context, dto => dto.MapFrom(x => x.ContextArea)).
                ForMember(x => x.ReleaseState, dto => dto.MapFrom(x => x.ReleaseState)).
                ForMember(x => x.ReleasedPerson, dto => dto.MapFrom(x => x.ReleasedPerson))
                .ReverseMap();
            CreateMap<NewsListDto, NewsUpdateDto>().ReverseMap();
        }
    }
}
