using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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
			CreateModelAndSetBinding ();
		}

		void CreateModelAndSetBinding ()
		{
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
			await SearchAsync (sender, args);
		}

		async Task SearchAsync (object sender, EventArgs args)
		{
			_model.IsLoading = true;

			var viewModel = await GetSearchResult ();
			await BindViewModel (viewModel);

			btnLoadMoreResults.IsVisible = true;
			_model.IsLoading = false;
		}

		async void ItemTapped(object sender, ItemTappedEventArgs args)
		{
		 	await ItemTappedAsync (sender, args);
		}

		async Task ItemTappedAsync (object sender, ItemTappedEventArgs args)
		{
			_model.IsLoading = true;

			await NavigateToFullInfoPage (args);

			_model.IsLoading = false;
		}

		async void GetMoreSearchResults (object sender, EventArgs args)
		{
			await GetMoreSearchResultsAsync (sender, args);
		}

		async Task GetMoreSearchResultsAsync (object sender, EventArgs args)
		{
			_model.IsLoading = true;

			var searchResult = await LFMService.Instance.Search (searchTerm, 5, ++_startIndex);

			await BindViewModel (searchResult);

			_model.IsLoading = false;
		}

		// additional methods

		async Task<IEnumerable<ViewModel>> GetSearchResult()
		{
			_startIndex = 1;
			_model.SearchResult.Clear ();

			searchTerm = btnSearch.Text;
			btnLoadMoreResults.IsVisible = false;

			var searchResult = await LFMService.Instance.Search (searchTerm, 5, _startIndex );
			return searchResult;
		}

		async Task BindViewModel (IEnumerable<ViewModel> searchResult)
		{
			_model.GroupedItems.Clear ();

			foreach (var item in searchResult) {
				if (!_model.SearchResult.Any(vm => vm.Name.Equals(item.Name) && vm.Mbid == item.Mbid)) {
					_model.SearchResult.Add (item);
				}
			}

			var groupedViewModel = await GroupViewModel(_model.SearchResult);

			foreach (var item in groupedViewModel) {
				_model.GroupedItems.Add (item);
			}
		}

		async Task NavigateToFullInfoPage(ItemTappedEventArgs args)
		{
			var item = (ViewModel)args.Item;

			if (!item.Name.Equals("No artist matches")) {
				if (item.Type.Equals ("Artist")) 
				{
					var page = new ArtistFullInfoPage (item.Name);
					await Navigation.PushAsync (page);
				} 
				if (!item.Name.Equals("No album matches")) {
					if (item.Type.Equals ("Album")) 
					{
						var page = new AlbumFullInfoPage (item.Name, item.Mbid, item.Artist);
						await Navigation.PushAsync (page);
					}
				}
			}
		}	

		Task<IEnumerable<Group<string, ViewModel>>> GroupViewModel(ObservableCollection<ViewModel> items)
		{
			return Task.Run (() => {

				var sortedItems = items
					.GroupBy (vm => vm.Type)
					.Select (vm => new Group<string, ViewModel> (vm.Key, vm));
				return sortedItems;
			});
		}
	}
}

