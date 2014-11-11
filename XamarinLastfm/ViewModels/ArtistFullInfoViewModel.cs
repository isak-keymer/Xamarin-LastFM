using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinLastfm
{
	public class ArtistFullInfoViewModel
	{
		public string Name { get; set; }
		public string Mbid { get; set; }
		public string Url { get; set; }
		public string ContentSummary { get; set; }
		public int YearFormed { get; set; }
		public DateTime Published { get; set; }
		public ImageSource ImageSource { get; set; }
		public List<AlbumViewModel> Albums { get; set; }
		public List<string> SimilarArtists { get; set; }
	}
}

