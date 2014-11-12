using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace XamarinLastfm
{	
	public partial class ArtistFullInfoPage : ContentPage
	{	
		private ArtistFullInfoViewModel _model;

		public ArtistFullInfoPage (string artistName)
		{
			InitializeComponent ();

			this.Title = artistName;

			_model = new ArtistFullInfoViewModel ();
			GetArtist (artistName);

//			_model = new ArtistFullInfoViewModel { Name = "Shakira", Mbid = "007", 
//				ContentSummary = "Worlds most famous female latin singer",
//				YearFormed = 2001, Published = DateTime.Now.AddYears (-5),
//				ImageSourceList = new List<ImageSource> () { ImageSource.FromUri(new Uri("http://userserve-ak.last.fm/serve/500/38524543/Shakira.png"))}
//			};


		}

		async private void GetArtist(string artistName)
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

