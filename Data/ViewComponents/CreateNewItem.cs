using Microsoft.AspNetCore.Mvc;

namespace MovieManager.Views.Shared.Components.CreateNewItem
{
    [ViewComponent]
    public class CreateNewItem : ViewComponent
    {
        public CreateNewItem()
        {
        }

        public IViewComponentResult Invoke(string controller)
        {
            return View("", controller);
        }
    }
}