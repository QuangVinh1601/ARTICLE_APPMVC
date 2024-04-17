#pragma checksum "D:\NET_CORE MVC\Article_MVCAPP\Views\Shared\_PagingXTL.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "99cb552da86ea20415086b4d650598abe5e987322a388c83e816f54cb2f27e17"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__PagingXTL), @"mvc.1.0.view", @"/Views/Shared/_PagingXTL.cshtml")]
namespace AspNetCore
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\NET_CORE MVC\Article_MVCAPP\Views\_ViewImports.cshtml"
using WebAppMVC_1;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\NET_CORE MVC\Article_MVCAPP\Views\_ViewImports.cshtml"
using WebAppMVC_1.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"99cb552da86ea20415086b4d650598abe5e987322a388c83e816f54cb2f27e17", @"/Views/Shared/_PagingXTL.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"7eb920969a636b46a8aef6dafa3a6bb19e28539e24cc8828ada47410b7694130", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared__PagingXTL : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<App.Models.PagingModel>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 9 "D:\NET_CORE MVC\Article_MVCAPP\Views\Shared\_PagingXTL.cshtml"
  
    int currentPage  = Model.currentpage;
    int countPages   = Model.countpages;
    var generateUrl  = Model.generateUrl;


    
    if (currentPage > countPages) 
      currentPage = countPages;

    if (countPages <= 1) return;
    

    int? preview = null;
    int? next = null;

    if (currentPage > 1)
        preview = currentPage - 1;

    if (currentPage < countPages)
        next = currentPage + 1;

    // Các trang hiện thị trong điều hướng
    List<int> pagesRanges = new List<int>();    

        
    int delta      = 5;             // Số trang mở rộng về mỗi bên trang hiện tại     
    int remain     = delta * 2;     // Số trang hai bên trang hiện tại

    pagesRanges.Add(currentPage);
    // Các trang phát triển về hai bên trang hiện tại
    for (int i = 1; i <= delta; i++)
    {
        if (currentPage + i  <= countPages) {
            pagesRanges.Add(currentPage + i); 
            remain --;
        }
               
        if (currentPage - i >= 1) {
            pagesRanges.Insert(0, currentPage - i);
            remain --;
        }
            
    }    
    // Xử lý thêm vào các trang cho đủ remain 
    //(xảy ra ở đầu mút của khoảng trang không đủ trang chèn  vào)
    if (remain > 0) {
        if (pagesRanges[0] == 1) {
            for (int i = 1; i <= remain; i++)
            {
                if (pagesRanges.Last() + 1 <= countPages) {
                    pagesRanges.Add(pagesRanges.Last() + 1);
                }
            }
        }
        else {
            for (int i = 1; i <= remain; i++)
            {
                if (pagesRanges.First() - 1 > 1) {
                    pagesRanges.Insert(0, pagesRanges.First() - 1);
                }
            }
        }
    }


#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<ul class=\"pagination\">\r\n    <!-- Previous page link -->\r\n");
#nullable restore
#line 80 "D:\NET_CORE MVC\Article_MVCAPP\Views\Shared\_PagingXTL.cshtml"
     if (preview != null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li class=\"page-item\">\r\n            <a class=\"page-link\"");
            BeginWriteAttribute("href", " href=\"", 2212, "\"", 2240, 1);
#nullable restore
#line 83 "D:\NET_CORE MVC\Article_MVCAPP\Views\Shared\_PagingXTL.cshtml"
WriteAttributeValue("", 2219, generateUrl(preview), 2219, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Trang trước</a>\r\n        </li>\r\n");
#nullable restore
#line 85 "D:\NET_CORE MVC\Article_MVCAPP\Views\Shared\_PagingXTL.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li class=\"page-item disabled\">\r\n            <a class=\"page-link\" href=\"#\" tabindex=\"-1\" aria-disabled=\"true\">Trang trước</a>\r\n        </li>\r\n");
#nullable restore
#line 91 "D:\NET_CORE MVC\Article_MVCAPP\Views\Shared\_PagingXTL.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("      \r\n    <!-- Numbered page links -->\r\n");
#nullable restore
#line 94 "D:\NET_CORE MVC\Article_MVCAPP\Views\Shared\_PagingXTL.cshtml"
     foreach (var pageitem in pagesRanges)
    {
        if (pageitem != currentPage) {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li class=\"page-item\">\r\n                <a class=\"page-link\"");
            BeginWriteAttribute("href", " href=\"", 2660, "\"", 2689, 1);
#nullable restore
#line 98 "D:\NET_CORE MVC\Article_MVCAPP\Views\Shared\_PagingXTL.cshtml"
WriteAttributeValue("", 2667, generateUrl(pageitem), 2667, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                    ");
#nullable restore
#line 99 "D:\NET_CORE MVC\Article_MVCAPP\Views\Shared\_PagingXTL.cshtml"
               Write(pageitem);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </a>\r\n            </li>\r\n");
#nullable restore
#line 102 "D:\NET_CORE MVC\Article_MVCAPP\Views\Shared\_PagingXTL.cshtml"
        }   
        else 
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li class=\"page-item active\" aria-current=\"page\">\r\n                <a class=\"page-link\" href=\"#\">");
#nullable restore
#line 106 "D:\NET_CORE MVC\Article_MVCAPP\Views\Shared\_PagingXTL.cshtml"
                                         Write(pageitem);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <span class=\"sr-only\">(current)</span></a>\r\n            </li>\r\n");
#nullable restore
#line 108 "D:\NET_CORE MVC\Article_MVCAPP\Views\Shared\_PagingXTL.cshtml"
        }
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n    <!-- Next page link -->\r\n");
#nullable restore
#line 113 "D:\NET_CORE MVC\Article_MVCAPP\Views\Shared\_PagingXTL.cshtml"
     if (next != null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li class=\"page-item\">\r\n            <a class=\"page-link\"");
            BeginWriteAttribute("href", " href=\"", 3133, "\"", 3158, 1);
#nullable restore
#line 116 "D:\NET_CORE MVC\Article_MVCAPP\Views\Shared\_PagingXTL.cshtml"
WriteAttributeValue("", 3140, generateUrl(next), 3140, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Trang sau</a>\r\n        </li>\r\n");
#nullable restore
#line 118 "D:\NET_CORE MVC\Article_MVCAPP\Views\Shared\_PagingXTL.cshtml"
    }
    else 
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li class=\"page-item disabled\">\r\n            <a class=\"page-link\" href=\"#\" tabindex=\"-1\" aria-disabled=\"true\">Trang sau</a>\r\n        </li>\r\n");
#nullable restore
#line 124 "D:\NET_CORE MVC\Article_MVCAPP\Views\Shared\_PagingXTL.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</ul>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<App.Models.PagingModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
