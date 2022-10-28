namespace Dragonfly.UmbracoHelpers
{
    using Dragonfly.NetHelpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Umbraco.Cms.Core.Strings;

    public static class IHtmlEncodedStringExtensions
    {
        public static bool IsNullOrEmpty(this IHtmlEncodedString HtmlString, bool EmptyParagraphsIsNull)
        {
            if (HtmlString == null)
                return true;
            string Html = HtmlString.ToString();
            if (string.IsNullOrWhiteSpace(Html))
                return true;
            return EmptyParagraphsIsNull && string.IsNullOrWhiteSpace(Html.RemoveAllParagraphTags(false).Trim());
        }
    }
}
