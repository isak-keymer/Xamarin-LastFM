using System;
using System.Collections.Generic;

namespace XamarinLastfm
{
	public class Artist : Response
	{
		public string Listeners { get; set; }
		public string Streamable { get; set; }
		public SimilarArtists Similar { get; set; }
	}
}

