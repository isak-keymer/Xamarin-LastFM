using System;
using System.Collections.Generic;

namespace XamarinLastfm
{
	public class Artist
	{
		public string Name { get; set; }
		public string Listeners { get; set; }
		public string Mbid { get; set; }
		public string Url { get; set; }
		public string Streamable { get; set; }
		public ImageCollection Image { get; set; }
	}
}

