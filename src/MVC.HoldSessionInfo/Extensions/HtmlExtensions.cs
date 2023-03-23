using Microsoft.AspNetCore.Html;
using System.Text.Encodings.Web;

namespace MVC.HoldSessionInfo.Extensions;

public static class HtmlExtensions
{
    public static string ToHtmlString(this IHtmlContent content)
    {
        if (content == null)
            return null;

        using var writer = new StringWriter();
        content.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }
}
