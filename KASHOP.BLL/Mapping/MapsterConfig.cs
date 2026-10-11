using KASHOP.DAL.Dto;
using KASHOP.DAL.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Mapping
{
    public static class MapsterConfig
    {
        public static void MapsterConfigRegister()
        {
            //TypeAdapterConfig<Category, CategoryResponse>.NewConfig().Map(dest => dest.UserId, src => src.createdById); return userId with the response
            TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
            .Map(dest => dest.User, src => src.createdBy.UserName)
            .Map(dest => dest.Name, src => src.Translations.Where(t => t.Language ==CultureInfo.CurrentUICulture.Name).Select(t => t.Name).FirstOrDefault());
        }
    }
}
