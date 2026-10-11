using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services
{
    internal class FileService
    {
        public async Task<string?> UploadAsync(IFormFile file)
        {
            if (file is not null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString()
                    + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                return fileName;
            }

            return null;
        }
    }
}