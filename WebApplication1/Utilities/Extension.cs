namespace WebApplication1.Utilities;

public static class Extension
{
    public static bool CheckFileSize(this IFormFile file, int kb)
    {
        return file.Length / 1024 <= kb;
    }
    public static bool CheckFileFormat(this IFormFile file, string fileFormat)
    {
        return file.ContentType.Contains(fileFormat);
    }
    public static async Task<string> CopyFileAsync(this IFormFile file, string wwwroot, params string[] folders)
    {
        try
        {
            var fileName = Guid.NewGuid().ToString() + file.FileName;
            var resultPath = wwwroot;
            foreach (var item in folders)
            {
                resultPath = Path.Combine(resultPath, item);
            }
            resultPath = Path.Combine(resultPath, fileName);    
            using (FileStream stream = new(resultPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
