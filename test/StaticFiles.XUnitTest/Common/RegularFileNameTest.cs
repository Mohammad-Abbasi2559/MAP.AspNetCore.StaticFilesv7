using System.Text.RegularExpressions;

namespace StaticFiles.XUnitTest.Common;

public class RegularFileNameTest
{
    private static readonly Regex AdditionalSpace = new("\\s+");

    private static Regex RegexWithOutExtension(string fileName) => new(fileName[..fileName.LastIndexOf('.')] + @"\S+");

    [Theory]
    [InlineData("test.txt")]
    [InlineData("test.png")]
    [InlineData("test.mkv")]
    public void SetNameWithOutExtensionTest(string fileName) => Assert.Matches(RegexWithOutExtension(fileName), RegularFileName.SetNameWithOutExtension(fileName, out string? ext));

    [Theory]
    [InlineData(" sfdff  _ fsf. svvs.txt")]
    public void SetNameWithOutExtensionTest2(string fileName) => Assert.Matches(RegexWithOutExtension(AdditionalSpace.Replace(fileName, string.Empty)), RegularFileName.SetNameWithOutExtension(fileName, out string? ext));
}