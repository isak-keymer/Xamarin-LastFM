using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace XamarinLastfm
{	
	public partial class ArtistFullInfoPage : TabbedPage
	{	
		private ArtistFullInfoViewModel _model;

		public ArtistFullInfoPage (string artistName)
		{
			InitializeComponent ();

			this.Title = artistName;

			_model = new ArtistFullInfoViewModel ();

			GetArtist (artistName);
		}

		async void GetArtist(string artistName)
		{
			_model = await LFMService.Instance.GetArtistFullInfo (artistName);
			this.BindingContext = _model;

		}

		async void UrlButtonClicked (object sender, EventArgs args)
		{
			var webView = new ArtistWebView(_model.Url);
			await Navigation.PushAsync(webView);
		}

		async void ItemTapped(object sender, ItemTappedEventArgs args)
		{
			var artist = (SimilarArtistViewModel)args.Item;
			var fullinfoPage = new ArtistFullInfoPage (artist.Name);
			await Navigation.PushAsync(fullinfoPage);
		}
	}
}

