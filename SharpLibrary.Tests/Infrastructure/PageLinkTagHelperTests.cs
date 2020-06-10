using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using SharpLibrary.Infrastructure;
using SharpLibrary.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SharpLibrary.Tests.Infrastructure
{
    public class PageLinkTagHelperTests
    {
        [Fact]
        public void CanGeneratePageLinks()
        {
            Mock<IUrlHelper> urlHelper = new Mock<IUrlHelper>();
            urlHelper.SetupSequence(elm => elm.Action(It.IsAny<UrlActionContext>()))
                .Returns("Test/Page1")
                .Returns("Test/Page2")
                .Returns("Test/Page3");

            Mock<IUrlHelperFactory> urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(elm => elm.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(urlHelper.Object);

            PageLinkTagHelper helper = new PageLinkTagHelper(urlHelperFactory.Object)
            {
                PageModel = new PagingInfo
                {
                    CurrentPage = 2,
                    TotalItems = 28,
                    ItemsPerPage = 10
                },
                PageAction = "Test"
            };

            TagHelperContext ctx = new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), "");

            Mock<TagHelperContent> content = new Mock<TagHelperContent>();
            TagHelperOutput output = new TagHelperOutput("div", new TagHelperAttributeList(), (cache, encoder) => Task.FromResult(content.Object));

            helper.Process(ctx, output);
            Assert.Equal(@"<a href=""Test/Page1"">1</a>"
                + @"<a href=""Test/Page2"">2</a>"
                + @"<a href=""Test/Page3"">3</a>",
                output.Content.GetContent());
        }
    }
}
