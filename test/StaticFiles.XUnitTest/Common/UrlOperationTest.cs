
namespace StaticFiles.XUnitTest.Common;

public class UrlOperationTest
{
    [Theory]
    [InlineData("https://www.testdomain.com/admin/home/index", "https://www.testdomain.com/Admin/home/")]
    [InlineData("https://www.testdomain.com/home/index", "https://www.testdomain.com/home/")]
    public void CheckUrlWithOutParameterTest1(string path1, string path2)
    {
        Assert.True(UrlOperation.CheckUrlWithOutParameter(path1, path2));
    }

    [Theory]
    [InlineData("https://www.testdomain.com/admin/home/Index/", "https://www.testdomain.com/home/index")]
    [InlineData("https://www.testdomain.com/admin/", "https://www.testdomain.com/home/index")]
    public void CheckUrlWithOutParameterTest2(string path1, string path2)
    {
        Assert.False(UrlOperation.CheckUrlWithOutParameter(path1, path2));
    }

    [Theory]
    [InlineData("https://www.testdomain.com/admin/home", "  ")]
    public void CheckUrlWithOutParameterTest3(string path1, string path2)
    {
        Assert.Throws<ArgumentNullException>(() => UrlOperation.CheckUrlWithOutParameter(path1, path2));
    }

    [Theory]
    [InlineData("/admin/home/index", "/Admin/home/")]
    [InlineData("/home/index", "/home/")]
    public void CheckPathWithOutParameterTest1(string path1, string path2)
    {
        Assert.True(UrlOperation.CheckPathWithOutParameter(path1, path2));
    }

    [Theory]
    [InlineData("admin/home/Index/", "/home/index")]
    [InlineData("admin/", "/home/index")]
    [InlineData("/admin/home/create", "/admin/home/")]
    public void CheckPathWithOutParameterTest2(string path1, string path2)
    {
        Assert.False(UrlOperation.CheckPathWithOutParameter(path1, path2));
    }

    [Theory]
    [InlineData("admin/home", "  ")]
    public void CheckPathWithOutParameterTest3(string path1, string path2)
    {
        Assert.Throws<ArgumentNullException>(() => UrlOperation.CheckPathWithOutParameter(path1, path2));
    }

}
