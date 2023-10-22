
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
    public void CheckPathWithOutParameterTest1(string path1, string path2)
    {
        Assert.True(UrlOperation.CheckPathWithOutParameter(path1, path2));
    }

    [Theory]
    [InlineData("admin/home/Index/", "/home/index")]
    [InlineData("admin/", "/home/index")]
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
