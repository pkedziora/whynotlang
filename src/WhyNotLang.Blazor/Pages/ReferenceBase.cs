﻿using System.Resources;
using Markdig;
using Microsoft.AspNetCore.Components;
using WhyNotLang.EmbeddedResources.Reader;

namespace WhyNotLang.Blazor.Pages
{
    public class ReferenceBase : ComponentBase
    {
        [Inject] WhyNotLang.EmbeddedResources.Reader.IResourceReader SampleReader { get; set; }

        public MarkupString Content { get; set; }

        protected override void OnInitialized()
        {
            var reference = SampleReader.ReadReference();
            var html = Markdown.ToHtml(reference, new MarkdownPipelineBuilder().UseAdvancedExtensions().Build());
            Content = new MarkupString(html);
        }
    }
}
