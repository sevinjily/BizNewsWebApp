using System.IO;

namespace WebUI.Helpers
{
    public static class FileHelper
    {
        public static async Task<string> SaveFileAsync(this IFormFile file,string WebRootPath,string folderName)
        {
            if(!Directory.Exists(WebRootPath+folderName))
            {
                Directory.CreateDirectory(WebRootPath+folderName);
            }
        var path = Path.Combine(folderName, Guid.NewGuid() + file.FileName);
            //var path="uploads/"+Guid.NewGuid()+ file.FileName;
            using FileStream fileStream = new (WebRootPath + path,FileMode.Create);
            await file.CopyToAsync(fileStream);
            return path;
        }
}
}
