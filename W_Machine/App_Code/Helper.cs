using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace W_Machine.App_Code
{
    public static class Helper
    {
        public static HtmlString CreateList(this IHtmlHelper html, string items)
        {
            return new HtmlString(items);
        }
    }
}
