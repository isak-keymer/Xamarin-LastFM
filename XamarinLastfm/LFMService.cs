﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

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
		private Task<IEnumerable<AlbumViewModel>> CreateAlbumViewModel(List<Album> albums)
		{
			return Task.Run(() => { 

				var albumsToViewModel = albums
					.Select (alb => new AlbumViewModel { 
						AlbumName = alb.Name, 
						ImageSource = alb.Image.FirstOrDefault(img => img.Size.Equals("small")).Value 
					}).Take(10);				

				return albumsToViewModel;
			});
		}

		private Task<ArtistFullInfoViewModel> CreateArtistViewModel (ArtistFullInfo artist, IEnumerable<AlbumViewModel> albumsToView)
		{
			return Task.Run (() => {

				var similarArtists = artist.Similar.Artist.Select (art => 
					new SimilarArtistViewModel{ Name = art.Name, ImageSource = art.Image.FirstOrDefault(img => img.Size.Equals("small")).Value  });

				var imageSource = artist.Image.FirstOrDefault(img => img.Size.Equals("medium")).Value;

				var artistToViewModel = new ArtistFullInfoViewModel {
					Name= artist.Name,
					Mbid = artist.Mbid,
					Url = artist.Url,
					ContentSummary = artist.Bio.Summary, 
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

