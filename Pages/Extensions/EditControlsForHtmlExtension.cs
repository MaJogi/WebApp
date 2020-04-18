using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages.Extensions
{
    public static class EditControlsForHtmlExtension
    {
        public static IHtmlContent EditControlsFor<TClassType, TPropertyType>(
            this IHtmlHelper<TClassType> htmlHelper, Expression<Func<TClassType,
                TPropertyType>> expression) //This'i kasutame selleks, et öelda, et see on @Html. enda oma, et seda saaks kasutada kujul @Html.EditControlsFor(insert expression)
        {
            var htmlStrings = HtmlString(htmlHelper, expression);
            return new HtmlContentBuilder(htmlStrings);
        }

        private static IList<object> HtmlString<TClassType,
            TPropertyType>(IHtmlHelper<TClassType> htmlHelper,
            Expression<Func<TClassType, TPropertyType>> expression)
        {
            return new List<object> {
                new HtmlString("<div class=\"form-group\">"),
                htmlHelper.LabelFor(expression, new {@class = "text-dark"}),
                htmlHelper.EditorFor(expression,
                    new {htmlAttributes = new {@class = "form-control"}}),
                htmlHelper.ValidationMessageFor(expression, "", new {@class = "text-danger"}),
                new HtmlString("</div>")
            };
        }
    }
}
