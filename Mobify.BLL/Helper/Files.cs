using Microsoft.AspNetCore.Http;

namespace Mobify.BLL.Helper
{

    public static class Files
    {
        public static string UploadFile(string FolderName, IFormFile File)
        {
            try
            {
                string FolderPath = Directory.GetCurrentDirectory() + "/wwwroot/" + FolderName;
                string FileName = Guid.NewGuid() + Path.GetFileName(File.FileName);
                string FinalPath = Path.Combine(FolderPath, FileName);
                using (var Stream = new FileStream(FinalPath, FileMode.Create))
                {
                    File.CopyTo(Stream);
                }
                return FileName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public static string RemoveFile(string FolderName, string FileName)
        {
            try
            {
                var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", FolderName, FileName);

                if (File.Exists(directory))
                {
                    File.Delete(directory);
                    return "File Detected";
                }
                else
                {
                    return "FileNotFound";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }

}
