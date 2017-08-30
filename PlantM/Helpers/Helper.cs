namespace PlantM.Helpers
{
    using System;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public static class Helper
    {
        public static MvcHtmlString If(this MvcHtmlString value, bool evaluation)
        {
            return evaluation ? value : MvcHtmlString.Empty;
        }

        public static MvcHtmlString Image(this HtmlHelper helper, string src, string altText, string height, string css)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", src);
            builder.MergeAttribute("alt", altText);
            builder.MergeAttribute("height", height);
            builder.MergeAttribute("class", css);
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString IconLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, string iconName, object htmlAttributes = null)
        {
            var linkMarkup = htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes).ToHtmlString();
            var iconMarkup = String.Format("<span class=\"{0}\" \"></span>", iconName);
            return new MvcHtmlString(linkMarkup.Insert(linkMarkup.IndexOf(@"</a>"), iconMarkup));
        }

    }
}