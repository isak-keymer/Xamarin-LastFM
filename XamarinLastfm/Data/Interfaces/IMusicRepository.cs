using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XamarinLastfm
{
	public interface IMusicRepository
	{
		Task<IEnumerable<Artist>> SearchArtists(string artistToSearch, int numberOfResults, int? page = null);
		Task<ArtistFullInfo> GetArtistFullInfo(string artistName);
		Task<IEnumerable<ArtistTopAlbum>> GetAlbumsForArtist (string artistName);
		Task<IEnumerable<SearchedAlbum>> SearchAlbums (string album, int numberOfResults, int? page = null);
		Task<AlbumFullInfo> GetAlbumFullInfo (string album, string albumMbid, string artistName);
	}
}

