using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Deserializers;

namespace XamarinLastfm
{
	public class LFMWebService : IMusicRepository
	{
		public async Task<IEnumerable<Artist>> SearchArtists (string artistToSearch, int numberOfResults, int? page = null)
		{
			var request = await CreateRestRequest ("artist.search", "artist", artistToSearch, numberOfResults, page); 
			var response = await ReceiveRestResponse<ArtistSearchResponse> (request);
			if (response != null) {
				var artists = response.Results.Artistmatches.Artists;
				return artists;
			}
			return null;

		}

		public async Task<IEnumerable<SearchedAlbum>> SearchAlbums (string album, int numberOfResults, int? page = null)
		{
			var request = await CreateRestRequest ("album.search", "album", album, numberOfResults, page ); 
			var response = await ReceiveRestResponse<AlbumSearchResult> (request);

			if (response != null) {
				var albums = response.Results.AlbumMatches.Albums;
				return albums;
			}
			return null;
		}

		public async Task<ArtistFullInfo> GetArtistFullInfo (string artistName)
		{
			var request = await CreateRestRequest ("artist.getInfo", "artist", artistName ); 
			var response = await ReceiveRestResponse<ArtistGetInfoResponse> (request);

			if (response != null) {
				var artist = response.Artist;
				return artist;
			}
			return null;
		}

		public async Task<IEnumerable<ArtistTopAlbum>> GetAlbumsForArtist (string artistName)
		{
			var request = await CreateRestRequest ("artist.gettopalbums", "artist", artistName); 
			var response = await ReceiveRestResponse<AlbumSearchResponse> (request);		

			if (response != null) {
				var albums = response.TopAlbums.Albums;
				return albums;
			}
			return null;
		}

		public async Task<AlbumFullInfo> GetAlbumFullInfo (string albumName, string albumMbid, string artistName)
		{
			var request = await CreateRestRequest ("album.getInfo", "album", albumName, null, null, albumMbid, artistName ); 
			var response = await ReceiveRestResponse<AlbumGetInfoResponse> (request);
		
			if (response != null) {
				var album = response.Album;
				return album;
			}
			return null;
		}

		// Method for creating request and add parameters to the request
		public Task<RestRequest> CreateRestRequest(string LFMMethod, string requestedType, string requestParam, 
			int? limit = null, int? page = null, string mbid = "", string artistName = ""  )
		{
			return Task.Run (() => 
				{
					var request = new RestRequest ();
					request.AddParameter("method", LFMMethod);
					request.AddParameter(requestedType, requestParam);
					request.AddParameter("api_key",  LFMConfig.LFMKey );
					request.AddParameter("format", "json");

					if (limit != null) {
						request.AddParameter("limit", limit);
					}
					if (page != null) {
						request.AddParameter("page", page.ToString());
					}
					if (!string.IsNullOrEmpty(mbid)) {
						request.AddParameter("mbid", mbid);
					}
					if (!string.IsNullOrEmpty(artistName)) {
						request.AddParameter("artist", artistName);
					}

					return request;
				});
		}

		// Generic method for receiving response from API
		public Task<T> ReceiveRestResponse<T>(RestRequest request) where T : new()
		{
			var client = new RestClient (LFMConfig.LFMBaseUrl);
			var tcs = new TaskCompletionSource<T>();

			client.ExecuteAsync<T> (request, (response) => { 
				tcs.SetResult(response.Data);
			});
			return tcs.Task;
		}
	}
}

