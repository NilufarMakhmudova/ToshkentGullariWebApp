using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToshkentGullari.Controllers
{
    public class SelectListsController
    {
        public static IEnumerable<SelectListItem> GetCategoryList()
        {
            IList<SelectListItem> categories = new List<SelectListItem>
              {
                 new SelectListItem() {Text="Question", Value="Question"},
                 new SelectListItem() { Text="Comment", Value="Comment"}};
            return categories;
        }
    }
}