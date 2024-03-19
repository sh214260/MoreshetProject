using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Product
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public int? Price { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public bool Weight { get; set; }
        public string? Comment { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public string? Image { get; set; }

    }

}
