using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using StorePractice.Models;
using StorePractice.Models.SqlModels;

namespace StorePractice.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageGeniration : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PageGeniration(IUrlHelperFactory factory)
        {
            urlHelperFactory = factory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public Page PageModel { get; set; }
        public Dictionary<string, object> PageUrlValue { get; set; } = new Dictionary<string, object>();
        public string PageUrl { get; set; }
        public string PageCss { get; set; }
        public int PageCurrent { get; set; }
        public string PageCurrentCss { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");
            for (int page = 1; page <= PageModel.TotalPages; page++)
            {
                if (page > 4 && page < PageModel.TotalPages - 3)
                {
                    result.InnerHtml.Append(".");
                }
                else
                {
                    TagBuilder tag = new TagBuilder("a");
                    PageUrlValue["pageNow"] = page;
                    tag.Attributes["href"] = urlHelper.Action(PageUrl, PageUrlValue);

                    if (PageCurrentCss != null && page == PageCurrent)
                    {
                        tag.AddCssClass(PageCurrentCss);
                    }

                    if (PageCss != null)
                    {
                        tag.AddCssClass(PageCss);
                    }

                    tag.InnerHtml.Append(page.ToString());
                    result.InnerHtml.AppendHtml(tag);
                }
            }
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
