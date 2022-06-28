using Pretzel_Backend.Models;
using System.Collections.Generic;

namespace Pretzel_Backend.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Slider { get; set; }
        public IEnumerable<Bread> Bread { get; set; }
    }
}
