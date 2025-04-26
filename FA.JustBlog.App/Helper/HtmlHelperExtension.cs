using System.Security.Policy;
using Application.Dto.CategoryDtos;
using Domain.Entity;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FA.JustBlog.App.Helper
{
    public static class HtmlHelperExtension
    {
        public static IHtmlContent GenTagLink
        (
            this IHtmlHelper htmlHelper,
            IEnumerable<string> tags,
            string controller = "Post",
            string action = "FindByTagName",
            object htmlAttributes = null
        )
        {
            var htmlContentBuilder = new HtmlContentBuilder();

            foreach (var tag in tags)
            {
                var tagLink = new TagBuilder("a");

                tagLink.AddCssClass("btn btn-secondary badge badge-light");

                if (htmlAttributes != null)
                {
                    var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                    tagLink.MergeAttributes(attributes);
                }

                tagLink.Attributes.Add("href", $"/{controller}/{action}/{tag}");
                tagLink.InnerHtml.Append(tag);

                htmlContentBuilder.AppendHtml(tagLink);
                htmlContentBuilder.AppendHtml(" ");
            }

            return htmlContentBuilder;
        }

        public static IHtmlContent GenCategoryLink(
            this IHtmlHelper htmlHelper,
            IEnumerable<IndexCategoryDto> categoryDtos,
            string controller = "Post",
            string action = "FindByCategory"
            )
        {
            var htmlContentBuilder = new HtmlContentBuilder();
            foreach (var c in categoryDtos)
            {
                var tagLink = new TagBuilder("a");
                tagLink.AddCssClass("btn btn-secondary badge badge-light");
                //tagLink.Attributes.Add("href", $"/{controller}/{action}/{tag}");

                tagLink.Attributes.Add("href", $"{controller}/{action}/{c.Id}");
                tagLink.InnerHtml.Append(c.CategoryName);

                htmlContentBuilder.AppendHtml(tagLink);
            }
            return htmlContentBuilder;
        }
        public static IHtmlContent GenCategoryLink(
            this IHtmlHelper htmlHelper,
            IndexCategoryDto categoryDto,
            string controller = "Post",
            string action = "FindByCategory"
            )
        {
            var htmlContentBuilder = new HtmlContentBuilder();
            var tagStrong = new TagBuilder("strong");


            var tagLink = new TagBuilder("a");
            tagLink.AddCssClass("btn btn-secondary badge badge-light");
            tagLink.Attributes.Add("href", $"/{controller}/{action}/{categoryDto.Id}");
            tagLink.InnerHtml.Append(categoryDto.CategoryName);

            tagStrong.InnerHtml.Append("Category:  ");
            tagStrong.InnerHtml.AppendHtml(tagLink);
            htmlContentBuilder.AppendHtml(tagStrong);
            return htmlContentBuilder;
        }

        public static IHtmlContent GenDate(
            this IHtmlHelper htmlHelper,
            DateTime createdDate,
            int? views = null,
            decimal? rate = null,
            string? author = null,
            string? cssClass = null
            )
        {
            var htmlContentBuilder = new HtmlContentBuilder();
            var spanTag = new TagBuilder("span");
            string s = "";
            s = author == null ? s : s + $" by {author}";

            var time = DateTime.Now - createdDate;
            if (time.Days >= 2)
            {
                s += $" {time.Days} ago";
            }
            else if (time.Days >= 1)
            {
                s += $" Yesterday at {createdDate.Hour}:{createdDate.Minute}";
            }
            else if (time.Hours >= 1)
            {
                s += $" {time.Hours} hours ago";
            }
            else if (time.Minutes > 10)
            {
                s += $" {time.Minutes} minutes ago";
            }
            else
            {
                s += $" Just now";
            }
            s = rate == null ? s : s + $" with rate {rate}";
            s = views == null ? s : s + $" by {views} view(s)";

            if (cssClass != null)
            {
                spanTag.AddCssClass(cssClass);
            }
            else
            {
                spanTag.AddCssClass("fw-light");
            }
            spanTag.InnerHtml.Append(s);

            htmlContentBuilder.AppendHtml(spanTag);
            return htmlContentBuilder;
        }
    }
}
