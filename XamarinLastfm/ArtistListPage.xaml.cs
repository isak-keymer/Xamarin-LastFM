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
		//private int _startIndex = 0;

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

			var search = btnSearch.Text;
			var artists = await LFMService.Instance.SearchArtist (search);

			if (artists != null) {
				_model.ArtistList = new ObservableCollection<ArtistListViewModel>(artists);
			}
		}

		async void ItemTapped(object sender, ItemTappedEventArgs args)
		{
			var artist = (ArtistListViewModel)args.Item;
			var fullinfoPage = new ArtistFullInfoPage (artist.Name);
			await Navigation.PushAsync(fullinfoPage);
		}

		void ScrollItemAppering(object sender, ItemVisibilityEventArgs args)
		{
			var item = (ArtistListViewModel)args.Item;

			if (item.Mbid == _model.ArtistList.Last().Mbid) {
				// Här vill vi lägga till fler sökresultat sen för en "endlessscroll"
			}
		}

	}
}

