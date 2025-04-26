using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.UserDtos;

namespace Application.Dto.PostDtos
{
    public class IndexPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal? Rate { get; set; }
        public int Views {  get; set; }
        public AuthorDto Author { get; set; }
        public bool IsPublished { get; set; }

    }
}
