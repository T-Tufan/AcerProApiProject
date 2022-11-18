using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.UserDtos
{
    //Bu alanda Data Annotation ile veri kontrolü yapıldı.Genelde Fluent Api kullanırım.
    public class UserCreateDto
    {
        [Required(ErrorMessage = "Alan boş geçilemez!")]
        [MaxLength(15, ErrorMessage = "Kullanıcı adı karakterden fazla olamaz!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Alan boş geçilemez!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Alan boş geçilemez!")]
        [MaxLength(8, ErrorMessage = "Şifre 8 karakterden fazla olamaz!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Alan boş geçilemez!")]
        [Compare("Password", ErrorMessage = "Parolalar Eşleşmiyor!")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Alan boş geçilemez!")]
        public string Gender { get; set; }
    }
}
