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
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-value-")]
        public Dictionary<string, object> PageUrlValue { get; set; } = new Dictionary<string, object>();
        public string PageUrl { get; set; }
        public string PageCss { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");
            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                PageUrlValue["pageNow"] = i;
                tag.Attributes["href"] = urlHelper.Action(PageUrl, PageUrlValue);

                if (PageCss != null )
                {
                    tag.AddCssClass(PageCss);
                }

                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
