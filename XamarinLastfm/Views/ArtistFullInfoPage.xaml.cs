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
			CreateModelAndSetBinding (artistName);
		}

		async void CreateModelAndSetBinding (string artistName)
		{
			this.Title = artistName;
			this.Icon = "lastfmlogo48.png";
			NavigationPage.SetTitleIcon(this, this.Icon);

			_model = new ArtistFullInfoViewModel ();
			_model = await LFMService.Instance.GetArtistFullInfo (artistName);
			this.BindingContext = _model;
		}

		async void UrlButtonClicked (object sender, EventArgs args)
		{
			await UrlButtonAsync (sender, args);
		}	

		async void SimilarArtistItemTapped(object sender, ItemTappedEventArgs args)
		{
			await SimilarArtistTappedAsync (sender, args);
		}

		async void AlbumItemTapped(object sender, ItemTappedEventArgs args)
		{
			await AlbumTappedAsync (sender, args);
		}


		// Async event methods

		async Task UrlButtonAsync (object sender, EventArgs args)
		{
			var webView = new ArtistWebView(_model.Url);
			await Navigation.PushAsync(webView);
		}

		async Task SimilarArtistTappedAsync (object sender, ItemTappedEventArgs args)
		{
			var artist = (ViewModel)args.Item;

			if (!artist.Name.Equals("No artist matches")) {
				var fullinfoPage = new ArtistFullInfoPage (artist.Name);
				await Navigation.PushAsync(fullinfoPage);
				} 
		}

		async Task AlbumTappedAsync (object sender, ItemTappedEventArgs args)
		{
			var album = (ViewModel)args.Item;
		
			if (!album.Name.Equals("No albums for this artist")) {
				var fullinfoPage = new AlbumFullInfoPage (album.Name, album.Mbid, album.Artist);
				await Navigation.PushAsync(fullinfoPage);
				}
		}
	}
}

