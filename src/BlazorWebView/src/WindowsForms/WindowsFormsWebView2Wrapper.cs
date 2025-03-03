// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebView.WebView2;
using Microsoft.Web.WebView2.Core;
using WebView2Control = Microsoft.Web.WebView2.WinForms.WebView2;

namespace Microsoft.AspNetCore.Components.WebView.WindowsForms
{
    internal class WindowsFormsWebView2Wrapper : IWebView2Wrapper
    {
        private readonly WindowsFormsCoreWebView2Wrapper _coreWebView2Wrapper;

        public WindowsFormsWebView2Wrapper(WebView2Control webView2)
        {
            if (webView2 is null)
            {
                throw new ArgumentNullException(nameof(webView2));
            }

            WebView2 = webView2;
            _coreWebView2Wrapper = new WindowsFormsCoreWebView2Wrapper(this);
        }

        public ICoreWebView2Wrapper CoreWebView2 => _coreWebView2Wrapper;

        public Uri Source
        {
            get => WebView2.Source;
            set => WebView2.Source = value;
        }

        public WebView2Control WebView2 { get; }

        public CoreWebView2Environment Environment { get; set; }

        public async Task CreateEnvironmentAsync()
        {
            Environment = await CoreWebView2Environment.CreateAsync();
        }

        public Task EnsureCoreWebView2Async()
        {
            return WebView2.EnsureCoreWebView2Async(Environment);
        }
    }
}
