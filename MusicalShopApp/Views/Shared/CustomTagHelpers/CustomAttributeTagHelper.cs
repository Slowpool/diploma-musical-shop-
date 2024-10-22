using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MusicalShopApp.Views.Shared.CustomTagHelpers;

[HtmlTargetElement(Attributes = "custom-attributes")]
public class CustomAttributeTagHelper : TagHelper
{
#warning whaaat
    public Dictionary<string, string> CustomAttributes { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (CustomAttributes != null)
            foreach (var pair in CustomAttributes)
                if (!output.Attributes.ContainsName(pair.Key))
                    output.Attributes.Add(pair.Key, pair.Value);
    }
}
