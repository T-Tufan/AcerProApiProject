using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class AppRole : IdentityRole<int> // int primary key veri tipini belirtir.<IdentityRole içinde generic olarak oluşturulmuş
    {
        public DateTime CreatedTime { get; set; }
    }
}
