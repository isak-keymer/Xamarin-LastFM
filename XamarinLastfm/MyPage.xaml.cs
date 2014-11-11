using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using RestSharp;

namespace XamarinLastfm
{	
	public partial class MyPage : ContentPage
	{	
		public MyPage ()
		{
			InitializeComponent ();
		}

		async void OnButtonClicked(object sender, EventArgs args)
		{
			var artists = await LFMService.Instance.SearchArtist ("C");
			var firstArtist = artists.FirstOrDefault ();
			lblResult.Text = firstArtist.Name;
		}



		// Skal-kod för att navigera till hemsida, beroende på hur länken ser ut.
//		async void GoToPageButtonClicked(object sender, EventArgs args)
//		{
//			var url = _storedArtist.Url;
//			var webView = new ArtistWebView(url);
//			await Navigation.PushAsync(webView);
//		}

		// code for navigating to url
		//			hyperLink.Clicked += async (sender, e) => {
		//				var url = hyperLink.Value;
		//				var webView = new ArtistWebView(url);
		//				await Navigation.PushAsync(webView);
		//			};
	}
}

