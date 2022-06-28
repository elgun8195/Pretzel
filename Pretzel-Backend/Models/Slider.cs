using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pretzel_Backend.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        [NotMapped]
        public IFormFile PhotoLogo { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
