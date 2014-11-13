using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XamarinLastfm
{
	public class TestData : IMusicRepository
	{

		private List<Artist> _artists = new List<Artist>();

		public TestData ()
		{
			_artists.Add (
				new Artist()
				{
					Name = "Christopher Arlekvist",
					Mbid = "Hej",
					Url = "http://www.lastfm.se/music/Shakira?ac=shakira",
//					Bio = new Biography
//					{
//						Summary = "Älskvärd artist",
//						Content = "Han började sin karriär i Södertälje och det visade sig snabbt att Christopher hade talang. Redan som 8åring jobba han som dansbandssångare i Rågsved",
//						YearFormed = 1982,
//						Published = DateTime.Now.AddYears(-30)
//					},
					Image = new ImageCollection()
					{
						new Image{ Size = "100", Value = "naket.jpg"}
					}
				}
			);

			_artists.Add (
				new Artist()
				{
					Name = "The Hellacopters",
					Mbid = "Hej",
					Url = "http://www.lastfm.se/music/The+Hellacopters?ac=hellacop",
//					Bio = new Biography
//					{
//						Summary = "Ett svenskt rockband",
//						Content = "The Hellacopters var ett svenskt rockband som bildades 1994. När Hellacopters bildades var de väldigt inspirerade av det sena 1960- och 1970-talets protopunk/garage rock n’ roll och framförallt av banden The Stooges och MC5. Inte bara musiken utan även nästan hela scenshowen kommer från dessa rockband. The Hellacopters sägs tillsammans med det norska bandet Gluecifer vara bandet som fört tillbaka garagerock n’ rollen/protopunken till Sverige. Gluecifer och The Hellacopters samarbetade mycket under de tidiga åren och har släppt två splittalbum tillsammans.",
//						YearFormed = 1994,
//						Published = DateTime.Now.AddYears(-10)
//					},
					Image = new ImageCollection()
					{
						new Image{ Size = "100", Value = "http://userserve-ak.last.fm/serve/_/281694/The+Hellacopters.jpg"}
					}
				}
			);
		}
			
		public async Task<IEnumerable<Artist>> SearchArtist (string artistToSearch, int? page = null)
		{
			return await Task.Run(() =>
				{
					var artists = _artists.FindAll(artist => artist.Name.Contains(artistToSearch));

					return artists;
				});
		}

		public async Task<ArtistFullInfo> GetArtistFullInfo (string artistName)
		{
			return await Task.Run(() =>
				{
					List<Artist> artists = _artists.FindAll(artist => artist.Name.Contains(artistName));

					return new ArtistFullInfo();
				});
		}

		public  Task<List<Album>> GetAlbumsForArtist (string artistName)
		{
			throw new NotImplementedException ();
		}

		public  Task<List<SearchedAlbum>> SearchAlbums (string album, int? page = null)
		{
			throw new NotImplementedException ();
		}

		public  Task<AlbumFullInfo> GetAlbumFullInfo (string album, string albumMbid, string artistName)
		{
			throw new NotImplementedException ();
		}

	}
}