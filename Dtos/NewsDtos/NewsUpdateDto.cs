using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.NewsDtos
{
    public class NewsUpdateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Id alanı boş geçilemez!")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Alan boş bırakılamaz!")]
        public string TitleArea { get; set; }
        public string ContextArea { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreateTime { get; set; }
        public int ReleasedPerson { get; set; }
        public bool ReleaseState { get; set; }
    }
}
