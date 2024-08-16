using Microsoft.AspNetCore.Http;

namespace HotelReservationSystem.Service.Services.Helper
{
    public static class DocumentSetting
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            //folder path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", folderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            //file name(unique)
            var fileName = $"{Guid.NewGuid()}-{Path.GetExtension(file.FileName)}";

            //file path 
            var filePath = Path.Combine(folderPath, fileName);

            //save file as stream
            //unmanaged resource should be disposed
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return $"Files/{folderName}/{fileName}";
        }
    }
}
