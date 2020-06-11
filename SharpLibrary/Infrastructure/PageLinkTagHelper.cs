using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SharpLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SharpLibrary.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory _urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory factory)
        {
            _urlHelperFactory = factory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PagingInfo PageModel { get; set; }
        public string PageAction { get; set; }
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");
            if (PageModel.HasPreviousPage)
            {
                result.InnerHtml.AppendHtml(CreateTag(urlHelper, "a", new[] { new Tuple<string, string, string> ("href", PageAction, (PageModel.CurrentPage - 1).ToString()) }, PageClassesEnabled, PageClass + " " + PageClassNormal, "Предыдущая"));
            }
            for (int i = 1; i <= Math.Min(3, PageModel.CurrentPage-1); ++i)
            {
                result.InnerHtml.AppendHtml(CreateTag(urlHelper, "a", new[] { new Tuple<string, string, string>("href", PageAction, i.ToString()) }, PageClassesEnabled, PageClass + " " + PageClassNormal, i.ToString()));
            }
            if (PageModel.CurrentPage > 4)
            {
                result.InnerHtml.AppendHtml(CreateTag(urlHelper, "a", new Tuple<string, string, string>[] { }, PageClassesEnabled, PageClass + " " + PageClassNormal, "..."));
            }
            result.InnerHtml.AppendHtml(CreateTag(urlHelper, "a", new[] { new Tuple<string, string, string>("href", PageAction, PageModel.CurrentPage.ToString()) }, PageClassesEnabled, PageClass + " " + PageClassSelected, PageModel.CurrentPage.ToString()));
            if (PageModel.TotalPages - PageModel.CurrentPage > 3)
            {
                result.InnerHtml.AppendHtml(CreateTag(urlHelper, "a", new Tuple<string, string, string>[] { }, PageClassesEnabled, PageClass + " " + PageClassNormal, "..."));
            }
            for (int i = Math.Max(PageModel.CurrentPage + 1, PageModel.TotalPages - 2); i <= PageModel.TotalPages; ++i)
            {
                result.InnerHtml.AppendHtml(CreateTag(urlHelper, "a", new[] { new Tuple<string, string, string>("href", PageAction, i.ToString()) }, PageClassesEnabled, PageClass + " " + PageClassNormal, i.ToString()));
            }
            if (PageModel.HasNextPage)
            {
                result.InnerHtml.AppendHtml(CreateTag(urlHelper, "a", new[] { new Tuple<string, string, string>("href", PageAction, (PageModel.CurrentPage + 1).ToString()) }, PageClassesEnabled, PageClass + " " + PageClassNormal, "Следующая"));
            }
            output.Content.AppendHtml(result.InnerHtml);
        }

        private TagBuilder CreateTag(IUrlHelper urlHelper, string tag, Tuple<string, string, string>[] attributes, bool isCssEnable, string cssClasses, string innerHtml)
        {
            TagBuilder result = new TagBuilder(tag);
            foreach (var (attribute, action, pageIndex) in attributes)
            {
                result.Attributes[attribute] = urlHelper.Action(action, new { page = pageIndex });
            }
            if (isCssEnable)
            {
                result.AddCssClass(cssClasses);
            }
            result.InnerHtml.Append(innerHtml);
            return result;
        }
    }
}
