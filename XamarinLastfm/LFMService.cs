using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using System.Text;
using System.Globalization;
using System.Net;

namespace XamarinLastfm
{
	public class LFMService
	{
		private static LFMService instance;
		private IMusicRepository _repository;

		private LFMService() {

			_repository = MusicFactory.GetMusicData (MusicData.LFMWebService);
		}

		public static LFMService Instance
		{
			get 
			{
				if (instance == null)
				{
					instance = new LFMService();
				}
				return instance;
			}
		}

		public async Task<IEnumerable<ViewModel>> Search(string search, int numberOfResults, int? page = null)
		{
			var artists = await _repository.SearchArtists (search, numberOfResults, page);
			var albums = await _repository.SearchAlbums (search, numberOfResults, page);

			var artistsToViewModel = await CreateArtistListViewModel (artists);
			var albumsToViewModel = await CreateSearchedAlbumListViewModel (albums);

			var viewModelList = artistsToViewModel.Concat(albumsToViewModel);

			return viewModelList;
		}

		public async Task<ArtistFullInfoViewModel> GetArtistFullInfo(string artistName)
		{
			var artist = await _repository.GetArtistFullInfo (artistName);
			var albums = await _repository.GetAlbumsForArtist (artistName);

			var albumsToViewModel = await CreateAlbumListForArtistViewModel(albums);
			var artistToViewModel = await CreateArtistFullViewModel (artist, albumsToViewModel);

			return artistToViewModel;
		}

		public async Task<AlbumFullInfoViewModel> GetAlbumFullInfo (string albumName, string albumMbid, string artistName)
		{
			var album = await _repository.GetAlbumFullInfo(albumName, albumMbid, artistName);

			var albumToView =  new AlbumFullInfoViewModel {
				Name= album.Name,
				Id = album.Id,
				ReleaseDate = album.ReleaseDate.ToShortDateString(),
				ImageSource = album.Image.LastOrDefault().Value,
				Tracks = album.Tracks.TrackList
			};

			return albumToView;
		}

		// Additional methods
		private async Task<IEnumerable<ViewModel>> CreateArtistListViewModel(IEnumerable<Artist> artists)
		{
				if (artists != null) {
					var viewModel = artists
						.Select (artist => new ViewModel { 
							Name = artist.Name, 
							Mbid = artist.Mbid,
							ImageSource = artist.Image.FirstOrDefault().Value,
							Type = "Artist"
						});	

					return viewModel;
				}
			var emptyViewModel = await CreateEmptyViewModel("No artist matches", "Artist");
			return emptyViewModel;
		}

		private async Task<IEnumerable<ViewModel>> CreateAlbumListForArtistViewModel(IEnumerable<ArtistTopAlbum> albums)
		{
			if (albums != null) {
					var viewModel = albums
						.Select (album => new ViewModel { 
							Name = album.Name, 
							Mbid = album.Mbid,
							ImageSource = album.Image.FirstOrDefault().Value,
							Type = "Album",
							Artist = album.Artist.Name
						});	

					return viewModel;
				}

			var emptyViewModel = await CreateEmptyViewModel("No albums for this artist", "Album");
			return emptyViewModel;
		}

		private async Task<IEnumerable<ViewModel>> CreateSearchedAlbumListViewModel(IEnumerable<SearchedAlbum> albums)
		{
			if (albums != null) {

				var viewModel = albums
					.Select (album => new ViewModel { 
						Name = album.Name, 
						Mbid = album.Mbid,
						ImageSource = album.Image.FirstOrDefault().Value,
						Type = "Album",
						Artist = album.Artist
					});	

				return viewModel;	
			}			

			var emptyViewModel = await CreateEmptyViewModel("No album matches", "Album");
			return emptyViewModel;
		}

		private async Task<ArtistFullInfoViewModel> CreateArtistFullViewModel (ArtistFullInfo artist, IEnumerable<ViewModel> albums)
		{
			var similarArtists = await CreateArtistListViewModel (artist.Similar.Artist);
			var imageSource = artist.Image.LastOrDefault().Value;
			var content = await FilterContent (artist.Bio.Summary);

			var artistToViewModel = new ArtistFullInfoViewModel {
				Name= artist.Name,
				Mbid = artist.Mbid,
				Url = artist.Url,
				ContentSummary = content, 
				YearFormed = artist.Bio.YearFormed,
				Published = artist.Bio.Published.ToShortDateString(),
				ImageSource = imageSource,
				SimilarArtists = similarArtists,
				Albums = albums
			};

			return artistToViewModel;
		}

		private Task<IEnumerable<ViewModel>> CreateEmptyViewModel (string message, string type)
		{
			return Task.Run (() => {

				ViewModel viewModel = new ArtistListViewModel { Name = message, Type = type  };
				IEnumerable<ViewModel> emptyViewModel = new[]{ viewModel };

				return emptyViewModel;
			});
		}

		private Task<string> FilterContent (string summary)
		{
			return Task.Run (() => {

				var contentNoHTML = Regex.Replace (summary, "<.*?>", string.Empty);
				var contentSummary = Regex.Replace (contentNoHTML, "Read more about.*", string.Empty);
				contentSummary.Trim ();

				var decoded = WebUtility.HtmlDecode (contentSummary);

				return decoded.Trim ();
			});
		}
	}
}

