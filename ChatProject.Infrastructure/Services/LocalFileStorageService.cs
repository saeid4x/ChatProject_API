using ChatProject.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 


namespace ChatProject.Infrastructure.Services
{
    public class LocalFileStorageService:IFileStorageService
    {
        private readonly string _uploadFolder;

        public LocalFileStorageService(string uploadFolder)
        {
            _uploadFolder = uploadFolder;


            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }
        }


        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if(file == null || file.Length  == 0) 
            {
               throw new ArgumentException("Invalid file");
            }

            string uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            string filePath = Path.Combine(_uploadFolder, uniqueFileName);

            using(var fileStream = new FileStream(filePath , FileMode.Create))
            {
               await file.CopyToAsync(fileStream);  
            }

            return $"/uploads/{uniqueFileName}"; // Return relative URL
        }


        public async Task<bool> DeleteFileAsync(string fileUrl)
        {
            string filePath = Path.Combine(_uploadFolder, Path.GetFileName(fileUrl));

            if(File.Exists(filePath)) 
            {
                await Task.Run(() => File.Delete(filePath)); // Delete file asynchronously
                return true;
            }

            return false;   

        }


    }
}
