using System;
using System.Collections.Generic;
using RestSharp.Deserializers;

namespace XamarinLastfm
{
	public class ArtistMatches
	{
		[DeserializeAs(Name = "artist")]
		public List<Artist> Artists { get; set; }
	}


}

