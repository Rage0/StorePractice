using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StorePractice.Models.SqlModels;

namespace StorePractice.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-chars")]
    public class PageFilterForCategory : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PageFilterForCategory(IUrlHelperFactory urlHelper)
        {
            urlHelperFactory = urlHelper;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public List<string> PageChars { get; set; }
        public string PageCharUrl { get; set; }
        public string PageCharCss { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper url = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");
            for (int i = 0; i < PageChars.Count(); i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.Attributes["href"] = url.Action(PageCharUrl, new { charFilter = PageChars[i] });

                if (PageCharCss != null)
                {
                    tag.AddCssClass(PageCharCss);
                }

                tag.InnerHtml.Append(PageChars[i]);
                result.InnerHtml.AppendHtml(tag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
