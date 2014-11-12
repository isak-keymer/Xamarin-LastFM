using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;

namespace XamarinLastfm
{	
	public partial class ArtistListPage : ContentPage
	{	
		private LFMArtistListModel _model;
		private int _startIndex = 0;
		private string searchTerm;

		public ArtistListPage ()
		{
			InitializeComponent ();

			_model = new LFMArtistListModel ();

			this.Title = "Artist Search";
			this.BindingContext = _model;
		}

		async void OnSearchButtonClicked(object sender, EventArgs args)
		{
			_model.ArtistList.Clear ();
			_startIndex = 1;

			searchTerm = btnSearch.Text;
			var artists = await LFMService.Instance.SearchArtist (searchTerm, _startIndex);

			foreach (var artist in artists) {
				_model.ArtistList.Add (artist);
			}
		}

		async void ItemTapped(object sender, ItemTappedEventArgs args)
		{
			var artist = (ArtistListViewModel)args.Item;
			var fullinfoPage = new ArtistFullInfoPage (artist.Name);
			await Navigation.PushAsync(fullinfoPage);
		}

		async void ScrollItemAppearing(object sender, ItemVisibilityEventArgs args)
		{
			var item = (ArtistListViewModel)args.Item;

			if (item.Equals(_model.ArtistList.Last())) 
			{
				var artists = await LFMService.Instance.SearchArtist (searchTerm, ++_startIndex);

				foreach (var artist in artists) {
					_model.ArtistList.Add (artist);
				}
			}
		}
	}
}

