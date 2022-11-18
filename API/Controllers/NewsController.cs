using AutoMapper;
using Dtos.NewsDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService, IMapper mapper)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNews()
        {
            //Tüm Haberler çekilir.
            var result = await _newsService.GetAll();
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetNewsById(int id)
        {
            //Habere ait id ile tüm haber bilgileri çekilir.
            var result = await _newsService.GetById(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddNews(NewsCreateDto dto)
        {
            var userInfo = User.Claims.Select(x=> x.Value).ToList().FirstOrDefault();
            //var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            if (userInfo != null)
            {
                dto.ReleasedPerson = int.Parse(userInfo);
                var result = _newsService.Create(dto);
                return Ok(result);
            }
                //Kullanıcı giriş yapmamış.Login Sayfasına yönlendirilecek.
                //Frontend yok şuan logine yönlendirme yapılamıyor.
                //Http400 hatası veriliyor.
                return BadRequest();
            
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteNews(object id)
        {
            //Giriş yapan kullanıcı Id alır.
            var userInfo = User.Claims.Select(x => x.Value).ToList().FirstOrDefault();
            //Silinmesi istenen haber veritabanından çekilir.
            var deletedEntity = await _newsService.GetById(int.Parse(id.ToString()));
            //Giriş yapan kullanıcı ve haberi yayınlayan ıd kontrolü yapılır.
            if (deletedEntity.ReleasedPerson.ToString() == userInfo)
            {
                //İlgili kişi haberi silebilir.
                var result = _newsService.Remove(id);
                return Ok(result);
            }
            //Kullanıcı başka bir kullanıcıya ait haberi silmeye çalışırsa http400 hatası alır.
            return BadRequest();

        }
        [HttpPost]
        public async Task<IActionResult> UpdateNews(NewsUpdateDto dto)
        {
            //Giriş yapan kullanıcı Id alır.
            var userInfo = User.Claims.Select(x => x.Value).ToList().FirstOrDefault();
            //Güncellenmesi istenen haber veritabanından çekilir.
            var updatedEntity = await _newsService.GetById(int.Parse(dto.Id));
            //Giriş yapan kullanıcı ve haberi yayınlayan ıd kontrolü yapılır.
            if (updatedEntity.ReleasedPerson.ToString() == userInfo)
            {
                //İlgili kişi haberi güncelleyebilir.
                var result = _newsService.Update(dto);
                return Ok(result);
            }
            //Kullanıcı başka bir kullanıcıya ait haberi güncellemeye çalışırsa http400 hatası alır.
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> ChangeReleaseState(int id, bool state)
        {
            //Giriş yapan kullanıcı ve haberi yayınlayan ıd kontrolü yapılır.
            var result = _newsService.ChangeReleaseState(id, state);
            return Ok(result);

        }
    }
}
