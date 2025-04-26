using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.CategoryDtos;
using Domain.Entity;

namespace Application.Dto.PostDtos
{
    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public bool IsPublished { get; set; }
        public string TagNames { get; set; }
        public int CategoryId { get; set; }
    }
}
