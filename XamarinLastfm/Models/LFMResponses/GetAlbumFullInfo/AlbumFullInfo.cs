using System;
using System.Collections.Generic;

namespace XamarinLastfm
{
	public class AlbumFullInfo : Album
	{
		public string Id { get; set; }
		public DateTime ReleaseDate { get; set; }
		public Tracks Tracks { get; set; }
		public string PlayCount { get; set; }
	}
}

