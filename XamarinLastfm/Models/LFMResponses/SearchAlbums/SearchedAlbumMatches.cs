using System;
using System.Collections.Generic;
using RestSharp.Deserializers;

namespace XamarinLastfm
{
	public class SearchedAlbumMatches
	{
		[DeserializeAs(Name = "album")]
		public List<SearchedAlbum> Albums { get; set; }
	}
}

