using Dtos.NewsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface INewsService
    {
        Task<List<NewsListDto>> GetAll();
        Task Create(NewsCreateDto dto);
        Task<NewsListDto> GetById(int id);
        Task ChangeReleaseState(int Id,bool state);
        Task Remove(object id);
        Task Update(NewsUpdateDto dto);
    }
}
