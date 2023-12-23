using Microsoft.AspNetCore.Mvc;

namespace Connectify.ViewsComponent
{
    public class NavbarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync() {
            return await Task.FromResult((IViewComponentResult)View(HttpContext.User));
        }
    }
}
