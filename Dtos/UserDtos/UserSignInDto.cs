using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.UserDtos
{
    //Bu alanda Data Annotation ile veri kontrolü yapıldı.Genelde Fluent Api kullanırım.
    public class UserSignInDto
    {
        [Required(ErrorMessage = "Kullanıcı adı gereklidir!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Parola gereklidir!")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
