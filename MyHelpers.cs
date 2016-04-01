using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Pastaci
{
    public static class HtmlHelpers
    {
        public static string URLStringBuilder(string a)
        {

            a = a.ToLower().Replace(" ", "-").Replace("ş", "s").Replace("ğ", "g").Replace("ı", "i").Replace("ç", "c").Replace("ü", "u").Replace("ö", "o");
            char[] ch = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'i', 'z', 'x', 'c', 'v', 'b', 'n', 'm', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-', '_' };

            for (int i = 0; i < a.Length; i++)
            {
                bool bl = true;
                for (int k = 0; k < ch.Length; k++)
                {
                    if (a[i] == ch[k])
                    {
                        bl = false;
                        break;
                    }
                }
                if (bl)
                {
                    a = a.Remove(i, 1);
                }
            }

            return a.Trim('-');
        }
        public static IHtmlString URLBuilder(this HtmlHelper helper, string a)
        {
            return new HtmlString(URLStringBuilder(a));
        }

        public static string removeHTML(string htmlDocument)
        {
            return Regex.Replace(htmlDocument, @"<[^>]*>", String.Empty);
        }

        public static IHtmlString removeHTML(this HtmlHelper helper, string a)
        {
            return new HtmlString(removeHTML(a));
        }

        /// <summary>  
        /// Creates an Html helper for an Image  
        /// </summary>   
        /// <param name="helper"></param>   
        /// <param name="src"></param>   
        /// <param name="altText"></param>   
        /// <returns></returns>   
        public static MvcHtmlString Image(this HtmlHelper helper, string src, string altText, string cssClass)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", src);
            builder.MergeAttribute("alt", altText);
            builder.MergeAttribute("class", cssClass);
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        //create an action link that can display html   

        public static MvcHtmlString ActionLinkHtml(this AjaxHelper ajaxHelper, string linkText, string actionName,
    string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var repID = Guid.NewGuid().ToString();
            var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));
        }

        //create an action link that can be clicked to sort and has a sorting icon (this is meant to be used to create column headers)   

        public static MvcHtmlString ActionLinkSortable(this HtmlHelper helper, string linkText, string actionName,
    string sortField, string currentSort, object currentDesc)
        {
            bool desc = (currentDesc == null) ? false : Convert.ToBoolean(currentDesc);
            //get link route values   
            var routeValues = new System.Web.Routing.RouteValueDictionary();
            routeValues.Add("id", sortField);
            routeValues.Add("desc", (currentSort == sortField) && !desc);
            //build the tag   
            if (currentSort == sortField) linkText = string.Format("{0} <span class='badge'><span class='glyphicon glyphicon-sort-by-attributes{1}'></span></span>", linkText, (desc) ? "-alt" : "");
            TagBuilder tagBuilder = new TagBuilder("a");
            tagBuilder.InnerHtml = linkText;
            //add url to the link   
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(actionName, routeValues);
            tagBuilder.MergeAttribute("href", url);
            //put it all together   
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }

        //custom html helper to output a nicely-formatted address element   

//////        public static MvcHtmlString DisplayAddressFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
//////    System.Linq.Expressions.Expression<Func<TModel, TProperty>> expression, bool isEditable = false,
//////    object htmlAttributes = null)
//////        {
//////            var valueGetter = expression.Compile();
//////            var model = valueGetter(helper.ViewData.Model) as InGaugeService.Address;
//////            var sb = new List<string>();
//////            if (model != null)
//////            {
//////                if (!string.IsNullOrEmpty(model.AddressLine1)) sb.Add(model.AddressLine1);
//////                if (!string.IsNullOrEmpty(model.AddressLine2)) sb.Add(model.AddressLine2);
//////                if (!string.IsNullOrEmpty(model.AddressLine3)) sb.Add(model.AddressLine3);
//////                if (!string.IsNullOrEmpty(model.City) || !string.IsNullOrEmpty(model.StateRegion) ||
//////    !string.IsNullOrEmpty(model.PostalCode))
//////                    sb.Add(string.Format("{0}, {1} {2}", model.City,
//////model.StateRegion, model.PostalCode));
//////                if (model.IsoCountry != null) sb.Add(model.IsoCountry.CountryName);
//////                if (model.Latitude != null || model.Longitude != null)
//////                    sb.Add(string.Format("{0}, {1}",
//////model.Latitude, model.Longitude));
//////            }

//////            var delimeter = (isEditable) ? Environment.NewLine : "<br />";
//////            var addr = (isEditable) ? new TagBuilder("textarea") : new TagBuilder("address");
//////            addr.MergeAttributes(new System.Web.Routing.RouteValueDictionary(htmlAttributes));
//////            addr.InnerHtml = string.Join(delimeter, sb.ToArray());
//////            return MvcHtmlString.Create(addr.ToString());
//////        }



        public static string Label(string target, string text)
        {
            return String.Format("<label for='{0}'>{1}</label>", target, text);
        }

        //Submit Button Helper   
        public static MvcHtmlString SubmitButton(this HtmlHelper helper, string buttonText)
        {
            string str = "<input type=\"submit\" value=\"" + buttonText + "\" />";
            return new MvcHtmlString(str);
        }

        
        

    }
}