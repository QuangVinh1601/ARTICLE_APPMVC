#pragma checksum "D:\NET_CORE MVC\Article_MVCAPP\Views\First\ViewProduct3.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "64d0b8d6dee24a9f7c81698787196d51cc33659d0ccedde3890b00b8d661e981"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_First_ViewProduct3), @"mvc.1.0.view", @"/Views/First/ViewProduct3.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"64d0b8d6dee24a9f7c81698787196d51cc33659d0ccedde3890b00b8d661e981", @"/Views/First/ViewProduct3.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"7eb920969a636b46a8aef6dafa3a6bb19e28539e24cc8828ada47410b7694130", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_First_ViewProduct3 : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\NET_CORE MVC\Article_MVCAPP\Views\First\ViewProduct3.cshtml"
  
    Layout = "_Layout";
    var product = this.ViewBag.Product as ProductModel;

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"card bg-primary text-white\">\r\n    <h1 class=\"card-header\"> ");
#nullable restore
#line 6 "D:\NET_CORE MVC\Article_MVCAPP\Views\First\ViewProduct3.cshtml"
                        Write(product.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n    <div class=\"card-body\">\r\n        <p class=\"card-text\">");
#nullable restore
#line 8 "D:\NET_CORE MVC\Article_MVCAPP\Views\First\ViewProduct3.cshtml"
                        Write(product.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n    </div>\r\n</div>\r\n<hr />\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
