using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XamarinLastfm
{
	public interface IMusicRepository
	{
		Task<IEnumerable<Artist>> SearchArtist(string artistToSearch, int? page = null);
		Task<ArtistFullInfo> GetArtistFullInfo(string artistName);
		Task<List<Album>> GetAlbumsForArtist (string artistName);
	}
}

