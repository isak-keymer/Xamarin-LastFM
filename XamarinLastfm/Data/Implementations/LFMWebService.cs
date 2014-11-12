using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Deserializers;

namespace XamarinLastfm
{
	public class LFMWebService : IMusicRepository
	{
		public async Task<IEnumerable<Artist>> SearchArtist (string artistToSearch, int? page = null)
		{
			var request = await CreateRestRequest ("artist.search", "artist", artistToSearch, "10", page); 
			var response = await ReceiveRestResponse<ArtistSearchResponse> (request);
			var artists = response.Results.Artistmatches.Artists;
			return artists;
		}

		public async Task<ArtistFullInfo> GetArtistFullInfo (string artistName)
		{
			var request = await CreateRestRequest ("artist.getInfo", "artist", artistName ); 
			var response = await ReceiveRestResponse<ArtistGetInfoResponse> (request);
			var artist = response.Artist;

			return artist;
		}

		public async Task<List<Album>> GetAlbumsForArtist (string artistName)
		{
			var request = await CreateRestRequest ("artist.gettopalbums", "artist", artistName); 
			var response = await ReceiveRestResponse<AlbumSearchResponse> (request);
			var albums = response.TopAlbums.Albums;

			return albums;
		}

		// Method for creating request and add parameters to the request
		public Task<RestRequest> CreateRestRequest(string LFMMethod, string requestedType, string requestParam, string limit = "", int? page = null  )
		{
			return Task.Run (() => 
				{
					var request = new RestRequest ();
					request.AddParameter("method", LFMMethod);
					request.AddParameter(requestedType, requestParam);
					request.AddParameter("api_key",  LFMConfig.LFMKey );
					request.AddParameter("format", "json");

					if (!string.IsNullOrEmpty(limit)) {
						request.AddParameter("limit", limit);
					}
					if (page != null) {
						request.AddParameter("page", page.ToString());
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

