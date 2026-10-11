using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Dto
{
    public class CategoryResponse
    {
        public int  Id { get; set; }
        //1  public string UserId { get; set; } return userId with the response
       public string User { get; set; } // return user name with the response 
        //public List<CategoryTranslationResponse> Translations { get; set; } want to reduce response size by delete this an add Name field
        public string Name { get; set; }


    }
}
