using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.NewsDtos
{
    public class NewsCreateDto
    {
        public string Title { get; set; }
        public string Context { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreateTime { get; set; }
        public int ReleasedPerson { get; set; }
        public bool ReleaseState { get; set; }

    }
}
