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
        private IUrlHelperFactory _urlHelperFactory;

        public PageGeniration(IUrlHelperFactory factory)
        {
            _urlHelperFactory = factory;
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
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");

            List<ItemForTagOfPage> listItem = new List<ItemForTagOfPage>()
            {
                new ItemForTagOfPage { Name = "Next >>" , NameTag = "a", Url = PageUrl },
                new ItemForTagOfPage { Name = "<< Back" , NameTag = "a", Url = PageUrl },
                new ItemForTagOfPage { NameTag = "a", Url = PageUrl },
            };

            if (PageCurrent > 1)
            {
                listItem[1].Page = PageCurrent - 1;
                TagBuilder tag = CreateTagWithAttributesHref(listItem[1]);
                result.InnerHtml.AppendHtml(tag);
            }
            for (int page = 1; page <= PageModel.TotalPages; page++)
            {
                if (PageCurrent >= 4 && PageCurrent <= PageModel.TotalPages - 3)
                {
                    for (int newPage = PageCurrent - 3; newPage <= PageCurrent + 3; newPage++)
                    {

                        listItem[2].Name = newPage.ToString();
                        listItem[2].Page = newPage;

                        TagBuilder newTag = CreateTagWithAttributesHref(listItem[2]);
                        result.InnerHtml.AppendHtml(newTag);
                    }
                    break;
                }
                else
                {
                    if (page > 4 )
                    {
                        if (page >= PageModel.TotalPages - 3)
                        {
                            listItem[2].Name = page.ToString();
                            listItem[2].Page = page;

                            TagBuilder lastTags = CreateTagWithAttributesHref(listItem[2]);
                            result.InnerHtml.AppendHtml(lastTags);
                        }
                        else
                        {
                            continue;
                        } 
                    }
                    else
                    {
                        listItem[2].Name = page.ToString();
                        listItem[2].Page = page;

                        TagBuilder tag = CreateTagWithAttributesHref(listItem[2]);
                        result.InnerHtml.AppendHtml(tag);

                        if (page == 4)
                        {
                            result.InnerHtml.Append(" ... ");
                        }
                    }
                }
            }
            if (PageCurrent != PageModel.TotalPages)
            {
                listItem[0].Page = PageCurrent + 1;

                TagBuilder tag = CreateTagWithAttributesHref(listItem[0]);
                result.InnerHtml.AppendHtml(tag);
            }

            output.Content.AppendHtml(result.InnerHtml);
        }

        private TagBuilder CreateTagWithAttributesHref(ItemForTagOfPage item)
        {
            TagBuilder tag = new TagBuilder(item.NameTag);
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

            if (PageCss != null && PageCurrentCss != null && item.HasCss)
            {
                tag.AddCssClass(PageCss);
                if (PageCurrent == item.Page)
                {
                    tag.AddCssClass(PageCurrentCss);
                }
            }

            PageUrlValue["pageNow"] = item.Page;
            tag.Attributes["href"] = urlHelper.Action(item.Url, PageUrlValue);
            tag.InnerHtml.Append(item.Name);
            return tag;
        }
    }
}
