using System;
using System.Collections.Generic;
using RestSharp.Deserializers;

namespace XamarinLastfm
{
	public class TopAlbums
	{
		[DeserializeAs(Name = "album")]
		public List<ArtistTopAlbum> Albums { get; set; }
	}
}

