using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using System.Text;
using System.Globalization;

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

		public async Task<IEnumerable<ArtistListViewModel>> SearchArtist(string search, int? index = null)
		{
			var artists = await _repository.SearchArtist (search, index);

			var artistsToViewModel = artists.Select (artist => new ArtistListViewModel { 
				Name = artist.Name, 
				Mbid = artist.Mbid,
				Image = artist.Image.FirstOrDefault(img => img.Size.Equals("small")).Value					
			} );

			return artistsToViewModel;
		}

		public async Task<ArtistFullInfoViewModel> GetArtistFullInfo(string artistName)
		{
			var artist = await _repository.GetArtistFullInfo (artistName);
			var albums = await _repository.GetAlbumsForArtist (artistName);

			var albumsToView = await CreateAlbumViewModel (albums);
			var artistViewModel = await CreateArtistViewModel (artist, albumsToView);

			return artistViewModel;
		}

		public async Task<List<AlbumListViewModel>> SearchAlbum(string album, int? index = null)
		{
			var albums = await _repository.SearchAlbums ("believe", index);

			var albumsToListView = albums
				.Select (alb => new AlbumListViewModel { 
				Name = alb.Name, 
				Image = alb.Image.FirstOrDefault ().Value 
			});

			return albumsToListView.ToList();
		}

		public async Task<AlbumFullInfoViewModel> GetAlbumFullInfo (string albumName, string albumMbid, string artistName)
		{
			var album = await _repository.GetAlbumFullInfo(albumName, albumMbid, artistName);

			if (album.Tracks.TrackList == null) {
				album.Tracks.TrackList = new List<Track> { new Track { Name = "Track 1", Artist = new Artist { Name = "Abba" }},
					new Track { Name = "Track 2", Artist = new Artist { Name = "Abba2" }},
					new Track { Name = "Track 3", Artist = new Artist { Name = "Abba3" }},
					new Track { Name = "Track 4", Artist = new Artist { Name = "Abba4" }}};
			}

			var albumToView = new AlbumFullInfoViewModel {
				Name= album.Name,
				Id = album.Id,
				ReleaseDate = album.ReleaseDate.ToShortDateString(),
				ImageSource = album.Image.LastOrDefault().Value,
				Tracks = album.Tracks.TrackList
			};

			return albumToView;
		}
			
		private Task<IEnumerable<AlbumViewModel>> CreateAlbumViewModel(List<Album> albums)
		{
			return Task.Run(() => { 

				var albumsToViewModel = albums
					.Select (alb => new AlbumViewModel { 
						AlbumName = alb.Name, 
						Mbid = alb.Mbid,
						ImageSource = alb.Image.FirstOrDefault().Value 
					});				

				return albumsToViewModel;
			});
		}

		private Task<ArtistFullInfoViewModel> CreateArtistViewModel (ArtistFullInfo artist, IEnumerable<AlbumViewModel> albumsToView)
		{
			return Task.Run (() => {

				var similarArtists = artist.Similar.Artist.Select (art => 
					new SimilarArtistViewModel{ Name = art.Name, ImageSource = art.Image.FirstOrDefault().Value  });

				var imageSource = artist.Image.LastOrDefault().Value;

				var contentNoHTML =  Regex.Replace(artist.Bio.Summary, "<.*?>", string.Empty);
				var contentSummary = Regex.Replace(contentNoHTML, "Read more about.*", string.Empty);


				var artistToViewModel = new ArtistFullInfoViewModel {
					Name= artist.Name,
					Mbid = artist.Mbid,
					Url = artist.Url,
					ContentSummary = contentSummary.Trim(), 
					YearFormed = artist.Bio.YearFormed,
					Published = artist.Bio.Published,
					ImageSource = imageSource,
					SimilarArtists = similarArtists.ToList(),
					Albums = albumsToView.ToList()
				};

				return artistToViewModel;
			});

		}
	}
}

