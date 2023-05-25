
using Microsoft.AspNetCore.Mvc;

namespace MAP.AspNetCore.StaticFiles.ViewComponents;

[ViewComponent(Name = "UploadForm")]
public class UploadFormViewComponent: ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
