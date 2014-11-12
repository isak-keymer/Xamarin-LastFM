using System;
using Xamarin.Forms;

namespace XamarinLastfm
{
	public class ArtistListViewModel
	{
		public ImageSource Image { get; set; }
		public string Name { get; set; }
		public string Mbid { get; set; }

		public override bool Equals (object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			ArtistListViewModel artist = (ArtistListViewModel)obj;

			return (this.Name == artist.Name ) && (this.Mbid == artist.Mbid);
		}

		public override int GetHashCode ()
		{
			int hash = 13;
			hash = (hash * 7) + Name.GetHashCode();
			hash = (hash * 7) + Mbid.GetHashCode();

			return hash;
		}
	}
}

