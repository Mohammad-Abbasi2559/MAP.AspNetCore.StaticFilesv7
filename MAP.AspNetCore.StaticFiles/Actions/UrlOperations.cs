
namespace MAP.AspNetCore.StaticFiles;

public static class UrlOperations
{

    /// <summary>
    /// This method remove slash if url ends with slash
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">url is null</exception>
    private static string RemoveLastSlash(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException("url in null");
        if (url.EndsWith("/"))
            url = url.Remove(url.Length - 1, 1);
        return url;
    }

    /// <summary>
    /// Get file name from Url
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static string? FileNameWithExtensionFromUrl(string url)
    {
        return url.Split("\\").LastOrDefault();
    }

    /// <summary>
    /// Check Url in asp.net Core Actions and Files
    /// </summary>
    /// <param name="url1"></param>
    /// <param name="url2"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static bool CheckUrl(string url1, string url2)
    {
        if (string.IsNullOrWhiteSpace(url1) || string.IsNullOrWhiteSpace(url2)) throw new ArgumentNullException("url is empty");

        url1 = RemoveLastSlash(url1);
        url2 = RemoveLastSlash(url2);

        if (!url1.Contains("\\") || !url2.Contains("\\")) throw new ArgumentException("url not correct");
        if (url1.Split("\\").Last().Contains("?") || url2.Split("\\").Last().Contains("?"))
        {
            url1 = url1.ToLower().Replace("/home/index", string.Empty).Replace("/index", string.Empty);
            url2 = url2.ToLower().Replace("/home/index", string.Empty).Replace("/index", string.Empty);
            
            if (url1 == url2)
                return true;
        }
        else
        {
            if (FileContentTypes.TryContentType(url1) || FileContentTypes.TryContentType(url2))
            {
                url1 = url1.ToLower();
                url2 = url2.ToLower();

                if (url1 == url2)
                    return true;
            }
            else
            {
                url1 = url1.ToLower().Replace("/home/index", string.Empty).Replace("/index", string.Empty);
                url2 = url2.ToLower().Replace("/home/index", string.Empty).Replace("/index", string.Empty);

                if (url1 == url2)
                    return true;
            }
        }
        return false;
    }
}
