using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Android.Widget;

namespace XamarinLastfm
{	
	public partial class ArtistListPage : ContentPage
	{	
		private LFMArtistListModel _model;
		private int _startIndex = 0;
		private string searchTerm;

		private int previousListCount  = 0;
		private int itemsVisibleThreshold = 2;

		public ArtistListPage ()
		{
			InitializeComponent ();

			_model = new LFMArtistListModel ();
			_model.IsLoading = true;

			this.Title = "Artist Search";
			this.BindingContext = _model;

			_model.IsLoading = false;

			this.Icon = "lastfmlogo48.png";

			NavigationPage.SetTitleIcon(this, this.Icon);

		}

		async void OnSearchButtonClicked(object sender, EventArgs args)
		{
			_model.IsLoading = true;

			await SearchForArtistAndBindViewModel ();

			_model.IsLoading = false;
		}

		async void ItemTapped(object sender, ItemTappedEventArgs args)
		{
			_model.IsLoading = true;

			await ShowFullInfoArtistPage (args);

			_model.IsLoading = false;
		}

		async void ScrollItemAppearing(object sender, ItemVisibilityEventArgs args)
		{
			var artist = (ArtistListViewModel)args.Item;

			var currentListCount = _model.ArtistList.Count;
			var artistIndex = _model.ArtistList.IndexOf(artist);

			if (currentListCount > previousListCount && (artistIndex + itemsVisibleThreshold) >= currentListCount) 
			{
				previousListCount = currentListCount;
				await Get10MoreSearchResults ();
			}	
		}

		// additional methods
		async Task SearchForArtistAndBindViewModel()
		{
			_startIndex = 1;
			previousListCount = 0;
			_model.ArtistList.Clear ();

			searchTerm = btnSearch.Text;

			var artists = await LFMService.Instance.SearchArtist (searchTerm, _startIndex);

			foreach (var artist in artists) {
				_model.ArtistList.Add (artist);
			}
		}

		async Task ShowFullInfoArtistPage(ItemTappedEventArgs args)
		{
			var artist = (ArtistListViewModel)args.Item;
			var fullinfoPage = new ArtistFullInfoPage (artist.Name);
			await Navigation.PushAsync(fullinfoPage);
		}

		async Task Get10MoreSearchResults ()
		{
			var artists = await LFMService.Instance.SearchArtist (searchTerm, ++_startIndex);

			foreach (var artist in artists) {
				_model.ArtistList.Add (artist);
			}
		}
	}
}

