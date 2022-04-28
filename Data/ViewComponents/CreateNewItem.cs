using Microsoft.AspNetCore.Mvc;

namespace Movie_01.Views.Shared.Components.CreateNewItem
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