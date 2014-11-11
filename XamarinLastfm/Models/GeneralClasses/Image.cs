using System;
using RestSharp.Deserializers;

namespace XamarinLastfm
{
	public class Image
	{
		public string Size { get; set; }
		[DeserializeAs(Name = "#text")]
		public string Value { get; set; }
	}
}

