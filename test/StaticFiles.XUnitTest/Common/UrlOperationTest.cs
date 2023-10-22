
namespace StaticFiles.XUnitTest.Common;

public class UrlOperationTest
{
    [Theory]
    [InlineData("/admin/home/index", "/Admin/home/")]
    [InlineData("/home/index", "/home/")]
    public void CheckUrlWithOutParameterTest(string path1, string path2)
    {
        Assert.True(UrlOperation.CheckUrlWithOutParameter(path1, path2));
    }

    [Theory]
    [InlineData("/admin/home/index", "/Admin/home/")]
    [InlineData("/home/index", "/home/")]
    public void CheckPathTest1(string path1, string path2)
    {
        Assert.True(UrlOperation.CheckPath(path1, path2));
    }

    [Theory]
    [InlineData("admin/home/Index/", "/home/index")]
    [InlineData("admin", "/home/index")]
    public void CheckPathTest2(string path1, string path2)
    {
        Assert.Throws<ArgumentException>(() => UrlOperation.CheckPath(path1, path2));
    }

    [Theory]
    [InlineData("admin/home", "  ")]
    public void CheckPathTest3(string path1, string path2)
    {
        Assert.Throws<ArgumentNullException>(() => UrlOperation.CheckPath(path1, path2));
    }

}
