using System.Text.RegularExpressions;

namespace StaticFiles.XUnitTest.Common;

public class RegularFileNameTest
{
    private static readonly Regex AdditionalSpace = new("\\s+");

    private static Regex RegexWithOutExtension(string fileName) => new(fileName[..fileName.LastIndexOf('.')] + @"\S+");

    private static Regex RegexWithExtension(string fileName) => new(fileName[..fileName.LastIndexOf('.')] + @"\S+" + fileName[fileName.LastIndexOf('.')..]);


    [Theory]
    [InlineData("test.txt")]
    [InlineData("test.png")]
    [InlineData("test.mkv")]
    public void SetNameWithOutExtensionTest(string fileName) => Assert.Matches(RegexWithOutExtension(fileName), RegularFileName.SetNameWithOutExtension(fileName, out string? ext));

    [Theory]
    [InlineData(" sfdff  _ fsf. svvs.txt")]
    public void SetNameWithOutExtensionTest2(string fileName) => Assert.Matches(RegexWithOutExtension(AdditionalSpace.Replace(fileName, string.Empty)), RegularFileName.SetNameWithOutExtension(fileName, out string? ext));

    [Theory]
    [InlineData("test.txt")]
    [InlineData(" sfdff  _ fsf. svvs.txt")]
    public void SetNameWithExtensionTest(string fileName) => Assert.Matches(RegexWithExtension(AdditionalSpace.Replace(fileName, string.Empty)), RegularFileName.SetNameWithExtension(fileName));

    [Theory]
    [InlineData("test.txt")]
    [InlineData(" sfdff  _ fsf. svvs.txt")]
    public void RemoveGuidFromNameWithOutExtensionTest(string fileName)
    {
        string indicator = RegularFileName.SetNameWithExtension(fileName);
        string removeGuid = RegularFileName.RemoveGuidFromNameWithOutExtension(indicator, out string ext);

        Assert.True(ext.Contains('.'));
        Assert.Matches(RegexWithOutExtension(AdditionalSpace.Replace(fileName, string.Empty)), removeGuid);
    }
}