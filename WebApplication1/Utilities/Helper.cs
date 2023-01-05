using System.IO;
namespace WebApplication1.Utilities;

public static class Helper
{
    public static bool DeleteFile(params string[] arrPath)
    {
        var resultPath = String.Empty;
        foreach(var path in arrPath)
        {
            resultPath = Path.Combine(resultPath, path);
        }
        if(File.Exists(resultPath))
        {
            File.Delete(resultPath);
            return true;
        }
        return false;
    }
    public enum RoleType:byte
    {
        Admin,
        Member,
        Moderator
    }
}
