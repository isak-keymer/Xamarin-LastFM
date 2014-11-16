using System;
using System.Collections.Generic;

namespace XamarinLastfm
{
	public class Track
	{
		public string Name { get; set; }
		//public int Duration { get; set; }
		public string Url { get; set; }
		public Artist Artist { get; set; }

		public string NameToView { get { return string.Format ("{0} - {1}", this.Artist.Name, this.Name); } }
	}

}

