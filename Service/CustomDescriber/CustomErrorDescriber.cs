using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CustomDescriber
{
    public class CustomErrorDescriber : IdentityErrorDescriber
    {
        //User Identity hata durumları için özel mesajlar yazdırılır.
        //Örnek 2 adet yazıldı.
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "DuplicateUserName",
                Description = "Bu kullanıcı adı daha önce alınmış!"
            };
            //return base.DuplicateUserName(userName);
        }
        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresUpper",
                Description = "Parola en az bir büyük harf içermelidir!"
            };
            //return base.PasswordRequiresUpper();
        }
    }
}
