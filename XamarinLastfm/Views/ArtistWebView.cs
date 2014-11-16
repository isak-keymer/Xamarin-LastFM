using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinLastfm
{	
	public partial class ArtistWebView : ContentPage
	{	
		public ArtistWebView (string url)
		{			WebView webView = new WebView
			{
				Source = new UrlWebViewSource
				{
					Url = url,
				},
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			this.Content = new StackLayout
			{
				Children =
				{
					webView
				}
			};
		}
	}
}

