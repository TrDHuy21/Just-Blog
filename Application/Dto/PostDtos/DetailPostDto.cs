using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.CategoryDtos;
using Application.Dto.CommentDtos;
using Domain.Entity;

namespace Application.Dto.PostDtos
{
    public class DetailPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public int Views { get; set; }
        public decimal Rate { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<string> TagNames { get; set; }
        public IndexCategoryDto CategoryDto { get; set; }
        public List<IndexCommentDto> Comments { get; set; }
    }
}
