#pragma checksum "D:\NET_CORE MVC\Article_MVCAPP\Views\Shared\_Alert.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8e8336b028ef36371adba9f166e753361a2027d5de0f957d06a9c2211d577bb2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__Alert), @"mvc.1.0.view", @"/Views/Shared/_Alert.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"8e8336b028ef36371adba9f166e753361a2027d5de0f957d06a9c2211d577bb2", @"/Views/Shared/_Alert.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"7eb920969a636b46a8aef6dafa3a6bb19e28539e24cc8828ada47410b7694130", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared__Alert : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\NET_CORE MVC\Article_MVCAPP\Views\Shared\_Alert.cshtml"
  
    string statusMessage = TempData["StatusMessage"] as string;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\NET_CORE MVC\Article_MVCAPP\Views\Shared\_Alert.cshtml"
 if (!string.IsNullOrEmpty(statusMessage))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"alert alert-warning alert-dismissible fade show\" role=\"alert\">\r\n        <p>");
#nullable restore
#line 7 "D:\NET_CORE MVC\Article_MVCAPP\Views\Shared\_Alert.cshtml"
      Write(statusMessage);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n        <button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">\r\n            <span aria-hidden=\"true\">&times;</span>\r\n        </button>\r\n    </div>\r\n");
#nullable restore
#line 12 "D:\NET_CORE MVC\Article_MVCAPP\Views\Shared\_Alert.cshtml"
}

#line default
#line hidden
#nullable disable
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