using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Models
{
    public class CatergoryTranslation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public int CategoryId { get; set; }
        Category Category { get; set; }
    }
}
