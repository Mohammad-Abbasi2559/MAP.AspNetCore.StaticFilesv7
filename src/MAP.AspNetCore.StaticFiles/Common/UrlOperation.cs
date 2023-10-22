
namespace MAP.AspNetCore.StaticFiles;

public static class UrlOperation
{

    /// <summary>
    /// This method remove slash if url ends with slash
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private static string RemoveLastSlash(string url) => url.EndsWith("/") ? url.Remove(url.Length - 1, 1) : url;

    private static string RemoveSlashAtFirst(string path) => path.StartsWith("/") ? path.Remove(0, 1) : path;

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
        string? lastData = url.Split("/").LastOrDefault();
        string removeParameter = string.Empty;
        if (lastData!.Contains('?'))
        {
            string[] questionSplit = lastData.Split('?');
            for (int i = 1; i < questionSplit.Length; i++) removeParameter += "?" + questionSplit[i];
        }
        return !string.IsNullOrWhiteSpace(removeParameter) ? lastData.Replace(removeParameter, string.Empty) : url;
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

        string[] urlSplit1 = RemoveParameter(RemoveLastSlash(url1)).ToLower().Replace("https://", string.Empty).Replace("http://", string.Empty).Replace("www.", string.Empty).Split("/");
        string[] urlSplit2 = RemoveParameter(RemoveLastSlash(url2)).ToLower().Replace("https://", string.Empty).Replace("http://", string.Empty).Replace("www.", string.Empty).Split("/");

        if (FileContentType.TryContentType(urlSplit1.Last()) || FileContentType.TryContentType(urlSplit2.Last())) return url1 == url2;
        else
        {
            List<string> checkUrl1 = new();
            List<string> checkUrl2 = new();

            for (int i = 0; i < urlSplit1.Length; i++) if (urlSplit1[i] != "home" && urlSplit1[i] != "index" && !string.IsNullOrEmpty(urlSplit1[i])) checkUrl1.Add(urlSplit1[i]);
            for (int i = 0; i < urlSplit2.Length; i++) if (urlSplit2[i] != "home" && urlSplit2[i] != "index" && !string.IsNullOrEmpty(urlSplit2[i])) checkUrl2.Add(urlSplit2[i]);

            if (checkUrl1.Count == checkUrl2.Count)
            {
                for (int i = 0; i < checkUrl1.Count; i++) if (checkUrl1[i] != checkUrl2[i]) return false;
            }
            else
            {
                if (checkUrl1.Count == checkUrl2.Count + 1 && checkUrl2.Count > 1)
                {
                    for(int i = 0; i < checkUrl2.Count; i++) if (checkUrl1[i] != checkUrl2[i]) return false;
                }
                else if (checkUrl1.Count == checkUrl2.Count + 1 && urlSplit1.Length > 2) return checkUrl1[1] == urlSplit1.LastOrDefault();
                else if(checkUrl1.Count + 1 == checkUrl2.Count && checkUrl1.Count > 1)
                {
                    for(int i = 0; i < checkUrl1.Count; i++) if (checkUrl1[i] != checkUrl2[i]) return false;
                }
                else if (checkUrl2.Count == checkUrl1.Count + 1 && urlSplit2.Length > 2) return checkUrl2[1] == urlSplit2.LastOrDefault();
                else return false;
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
    public static bool CheckPathWithOutParameter(string path1, string path2)
    {
        if (string.IsNullOrWhiteSpace(path1) || string.IsNullOrWhiteSpace(path2)) throw new ArgumentNullException("path1 or path2", "url is empty");
        if (!path1.Contains('/') || !path2.Contains('/')) throw new ArgumentException("path not correct");

        string[] pathSplit1 = RemoveParameter(RemoveSlashAtFirst(RemoveLastSlash(path1))).ToLower().Split("/");
        string[] pathSplit2 = RemoveParameter(RemoveSlashAtFirst(RemoveLastSlash(path2))).ToLower().Split("/");

        if (FileContentType.TryContentType(pathSplit1.Last()) || FileContentType.TryContentType(pathSplit2.Last())) return path1 == path2;
        else
        {
            List<string> checkPath1 = new();
            List<string> checkPath2 = new();

            for (int i = 0; i < pathSplit1.Length; i++) if (!(pathSplit1[i] == "home") && !(pathSplit1[i] == "index") && !string.IsNullOrEmpty(pathSplit1[i])) checkPath1.Add(pathSplit1[i]);
            for (int i = 0; i < pathSplit2.Length; i++) if (!(pathSplit2[i] == "home") && !(pathSplit2[i] == "index") && !string.IsNullOrEmpty(pathSplit2[i])) checkPath2.Add(pathSplit2[i]);

            if (checkPath1.Count == checkPath2.Count)
            {
                for(int i = 0; i < checkPath1.Count; i++) if (checkPath1[i] != checkPath2[i]) return false;
            }
            else
            {
                if (checkPath1.Count == checkPath2.Count + 1 && checkPath2.Count > 0)
                {
                    for(int i = 0; i < checkPath2.Count; i++) if (checkPath1[i] != checkPath2[i]) return false;
                }
                else if (checkPath1.Count == checkPath2.Count + 1 && pathSplit1.Length > 1) return checkPath1[0] == pathSplit1.LastOrDefault();
                else if(checkPath1.Count + 1 == checkPath2.Count && checkPath1.Count > 0)
                {
                    for(int i = 0; i < checkPath1.Count; i++) if (checkPath1[i] != checkPath2[i]) return false;
                }
                else if (checkPath2.Count == checkPath1.Count + 1 && pathSplit2.Length > 1) return checkPath2[0] == pathSplit2.LastOrDefault();
                else return false;
            }
        }

        return true;
    }
}
