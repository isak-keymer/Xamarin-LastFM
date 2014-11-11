using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

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

		public async Task<IEnumerable<ArtistListViewModel>> SearchArtist(string search)
		{
			var artists = await _repository.SearchArtist (search);

			var artistsToViewModel = artists.Select (artist => new ArtistListViewModel { Name = artist.Name, Mbid = artist.Mbid } );

			return artistsToViewModel;
		}

		public async Task<ArtistFullInfoViewModel> GetArtistFullInfo(string artistName)
		{
			var artist = await _repository.GetArtistFullInfo (artistName);
			var albums = await _repository.GetAlbumsForArtist (artistName);

			var albumsToViewModel = albums
				.Select (alb => new AlbumViewModel { 
					AlbumName = alb.Name, 
					Image = alb.Image.FirstOrDefault(i => i.Size.Equals("small")) })
				.Take(10);

			var artistToViewModel = new ArtistFullInfoViewModel {
				Name= artist.Name,
				Mbid = artist.Mbid,
				Url = artist.Url,
				ContentSummary = artist.Bio.Summary, 
				YearFormed = artist.Bio.YearFormed,
				Published = artist.Bio.Published,

//				ImageSourceList = artist.Image.FirstOrDefault(i => i.Size.Equals("small")),
				Albums = albumsToViewModel.ToList()
			};
			return artistToViewModel;
		}
	}
}

