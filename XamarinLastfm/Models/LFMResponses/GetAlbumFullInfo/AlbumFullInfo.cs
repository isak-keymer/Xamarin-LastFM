using System;
using System.Collections.Generic;

namespace XamarinLastfm
{
	public class AlbumFullInfo : Album
	{
		public string Artist { get; set; }
		public string Id { get; set; }
		public DateTime ReleaseDate { get; set; }
		public Tracks Tracks { get; set; }
	}
}

