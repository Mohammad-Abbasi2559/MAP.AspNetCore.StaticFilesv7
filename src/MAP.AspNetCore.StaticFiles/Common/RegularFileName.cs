using System.Text;
using System.Text.RegularExpressions;

namespace MAP.AspNetCore.StaticFiles;

public static class RegularFileName
{
    /// <summary>
    /// Set Regex for find Space in string
    /// </summary>
    /// <returns></returns>
    private static readonly Regex AdditionalSpace = new("\\s+");

    /// <summary>
    /// This method creates a unique name for your file and short your file name to dont exception url
    /// This method set file name with out Extension 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string SetNameWithOutExtension(string name, out string? extension, bool SetEnDigits = false)
    {
        string indicator = FileExtension.TryGetFileExtension(name, out string? exc) ? name[..name.LastIndexOf('.')] : name; //? If filename is with extension remove extension 

        indicator = SetEnDigits ? ToEnDigits(indicator) : indicator; //? Change Persian digits and Arabic digits to English digits

        indicator = AdditionalSpace.Replace(indicator, string.Empty); //? Remove white space from string

        extension = exc;

        return indicator.Length > 50 ? indicator[..50] + "_" + Guid.NewGuid().ToString() : indicator + "_" + Guid.NewGuid().ToString(); //? Set the length of uniqe name
    }

    /// <summary>
    /// This method creates a unique name for your file and short your file name to dont exception url
    /// This method replace  "_" and "." from name to "-"    
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string SetNameWithExtension(string name, bool SetEnDigits = false)
    {
        string extension = name[name.LastIndexOf('.')..];

        string indicator = FileContentType.TryContentType(name) ? name : throw new ArgumentOutOfRangeException(nameof(name)); //? If filename is with extension remove extension 

        indicator = SetEnDigits ? ToEnDigits(indicator) : indicator; //? Change Persian digits and Arabic digits to English digits

        indicator = AdditionalSpace.Replace(indicator, string.Empty); //? Remove white space from string

        return indicator.Length > 50 ? indicator[..50] + "_" + Guid.NewGuid().ToString() + extension : indicator + "_" + Guid.NewGuid().ToString() + extension; //? Set the length of uniqe name
    }

    /// <summary>
    /// This method creates a unique name for your file and short your file name to dont exception url
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string SetNameWithExtension(string name, string extension, bool SetEnDigits = false)
    {
        extension = extension.StartsWith('.') ? extension : "." + extension;

        string indicator = FileContentType.TryContentType(name + extension) ? name : throw new ArgumentException("{0} is not true" + nameof(extension)); //? Change Some specifed character

        indicator = SetEnDigits ? ToEnDigits(indicator) : indicator; //? Change Persian digits and Arabic digits to English digits

        indicator = AdditionalSpace.Replace(indicator, string.Empty); //? Remove white space from string

        return indicator.Length > 50 ? indicator[..50] + "_" + Guid.NewGuid().ToString() + extension : indicator + "_" + Guid.NewGuid().ToString() + extension; //? Set the length of uniqe name
    }

    /// <summary>
    /// This method remove uniqe code from file name and remove extension
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string RemoveGuidFromNameWithOutExtension(string name, out string extension)
    {
        extension = name[name.LastIndexOf('.')..];
        return name[..name.LastIndexOf('_')];
    }

    /// <summary>
    /// This method remove uniqe code from file name 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string RemoveGuidFromNameWithExtension(string name) => name[..(name.LastIndexOf('_') - 1)] + name[name.LastIndexOf('.')..];

    /// <summary>
    /// This method change Persian/Arabic number to English number
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string ToEnDigits(string input)
    {
        StringBuilder builder = new();
        return builder.Append(input).Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9").ToString();
    }
}