using System;
using System.Collections.Generic;
using RestSharp.Deserializers;

namespace XamarinLastfm
{
	public class Tracks
	{
		[DeserializeAs(Name = "track")]
		public List<Track> TrackList { get; set; }
	}
}

