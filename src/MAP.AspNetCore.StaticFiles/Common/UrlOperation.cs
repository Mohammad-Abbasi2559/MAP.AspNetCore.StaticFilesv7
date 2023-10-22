
namespace MAP.AspNetCore.StaticFiles;

public static class UrlOperation
{

    /// <summary>
    /// This method remove slash if url ends with slash
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private static string RemoveLastSlash(string url) => url.EndsWith("/") ? url.Remove(url.Length - 1, 1) : url;

    /// <summary>
    /// Get file name from Url
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static string? FileNameWithExtensionFromUrl(string url)
    {
        string lastData = url.Split("/").LastOrDefault()!;
        return FileContentType.TryContentType(lastData) ? lastData.Contains('?') ? lastData.Split('?')[1] : lastData : null;
    }

    /// <summary>
    /// Remove url Parameter
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private static string RemoveParameter(string url)
    {
        string lastData = url.Split("/").LastOrDefault()!;
        string removeParameter = string.Empty;
        if (lastData.Contains('?'))
        {
            string[] questionSplit = lastData.Split('?');
            for (int i = 1; i < questionSplit.Length; i++) removeParameter += "?" + questionSplit[i];
        }
        return removeParameter.Length > 0 ? lastData.Replace(removeParameter, string.Empty) : url;
    }

    /// <summary>
    /// Check Url in asp.net Core Actions and Files
    /// </summary>
    /// <param name="url1"></param>
    /// <param name="url2"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static bool CheckUrlWithOutParameter(string url1, string url2)
    {
        if (string.IsNullOrWhiteSpace(url1) || string.IsNullOrWhiteSpace(url2)) throw new ArgumentNullException("url1 or url2", "{0} is empty");
        if (!url1.Contains('/') || !url2.Contains('/')) throw new ArgumentException("url not correct");

        string[] urlSplit1 = RemoveParameter(RemoveLastSlash(url1)).ToLower().Replace("https://", string.Empty).Replace("http://", string.Empty).Split("/");
        string[] urlSplit2 = RemoveParameter(RemoveLastSlash(url2)).ToLower().Replace("https://", string.Empty).Replace("http://", string.Empty).Split("/");

        if (FileContentType.TryContentType(urlSplit1.Last()) || FileContentType.TryContentType(urlSplit2.Last())) return url1 == url2;
        else
        {
            List<string> checkUrl1 = new();
            List<string> checkUrl2 = new();

            for (int i = 0; i < urlSplit1.Length; i++) if (!urlSplit1.Contains("home") && !urlSplit1.Contains("index")) checkUrl1.Add(urlSplit1[i]);
            for (int i = 0; i < urlSplit2.Length; i++) if (!urlSplit2.Contains("home") && !urlSplit2.Contains("index")) checkUrl2.Add(urlSplit2[i]);

            if (checkUrl1.Count == checkUrl2.Count)
            {
                for (int i = 0; i < checkUrl1.Count; i++) if (checkUrl1[i] != checkUrl2[i]) return false;
            }
            else
            {
                if (checkUrl1.Count < checkUrl2.Count)
                {
                    for (int i = 0; i < checkUrl1.Count; i++) if (checkUrl1[i] != checkUrl2[i]) return false;
                }
                else if (checkUrl1.Count != checkUrl2.Count + 1) return false;
                else for (int i = 0; i < checkUrl2.Count; i++) if (checkUrl1[i] != checkUrl2[i]) return false;

            }
        }

        return true;
    }

    /// <summary>
    /// Check path in asp.net Core Actions and Files
    /// </summary>
    /// <param name="path1"></param>
    /// <param name="path2"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static bool CheckPath(string path1, string path2)
    {
        if (string.IsNullOrWhiteSpace(path1) || string.IsNullOrWhiteSpace(path2)) throw new ArgumentNullException("url is empty");
        if (!path1.Contains("/") || !path2.Contains("/")) throw new ArgumentException("path not correct");
        if (!path1.StartsWith("/") || !path2.StartsWith("/")) throw new ArgumentException("path not correct");

        path1 = RemoveLastSlash(path1);
        path2 = RemoveLastSlash(path2);

        if (path1.Split("/").Last().Contains("?") || path2.Split("/").Last().Contains("?"))
        {
            path1 = path1.ToLower().Replace("/home/index", string.Empty).Replace("/index", string.Empty).Replace("/home", string.Empty);
            path2 = path2.ToLower().Replace("/home/index", string.Empty).Replace("/index", string.Empty).Replace("/home", string.Empty);

            if (path1 == path2)
                return true;
        }
        else
        {
            if (FileContentType.TryContentType(path1) || FileContentType.TryContentType(path2))
            {
                path1 = path1.ToLower();
                path2 = path2.ToLower();

                if (path1 == path2)
                    return true;
            }
            else
            {
                path1 = path1.ToLower().Replace("/home/index", string.Empty).Replace("/index", string.Empty).Replace("/home", string.Empty);
                path2 = path2.ToLower().Replace("/home/index", string.Empty).Replace("/index", string.Empty).Replace("/home", string.Empty);

                if (path1 == path2)
                    return true;
            }
        }
        return false;
    }
}
