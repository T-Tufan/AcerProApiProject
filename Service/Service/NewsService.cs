using AutoMapper;
using DataAccess.UnitOfWork;
using Dtos.NewsDtos;
using Entities.Entity;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        public NewsService(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task ChangeReleaseState(int Id, bool state)
        {
            var updatedEntity = await _unitofwork.GetRepository<News>().GetByFilter(x => x.Id == Id);
            updatedEntity.ReleaseState = state;
            _unitofwork.GetRepository<News>().Update(updatedEntity);
            await _unitofwork.SaveChanges();

        }

        public async Task Create(NewsCreateDto dto)
        {
            await _unitofwork.GetRepository<News>().Add(_mapper.Map<NewsCreateDto, News>(dto));
            await _unitofwork.SaveChanges();
        }

        public async Task<List<NewsListDto>> GetAll()
        {
            var list = await _unitofwork.GetRepository<News>().GetAll();
            var mappedList = new List<NewsListDto>();
            if (list != null && list.Count > 0)
            {
                //Mapper Kullanıldı.
                mappedList = _mapper.Map<List<News>, List<NewsListDto>>(list);
            }
            return mappedList;
        }

        public async Task<NewsListDto> GetById(int id)
        {
            var news = await _unitofwork.GetRepository<News>().GetByFilter(x => x.Id == id);
            return _mapper.Map<NewsListDto>(news);
        }

        public async Task Remove(object id)
        {
            _unitofwork.GetRepository<News>().Delete(id);
            await _unitofwork.SaveChanges();
        }

        public async Task Update(NewsUpdateDto dto)
        {
            _unitofwork.GetRepository<News>().Update(_mapper.Map<News>(dto));
            await _unitofwork.SaveChanges();
        }
    }
}
