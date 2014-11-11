using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinLastfm
{	
	public partial class ArtistFullInfoPage : ContentPage
	{	
		private ArtistFullInfoViewModel _model;

		public ArtistFullInfoPage (string artistName)
		{
			InitializeComponent ();

			this.Title = artistName;

			_model = new ArtistFullInfoViewModel { Name = "Shakira", Mbid = "007", 
				ContentSummary = "Worlds most famous female latin singer",
				YearFormed = 2001, Published = DateTime.Now.AddYears (-5),
				ImageSourceList = new List<ImageSource> () { ImageSource.FromUri(new Uri("http://userserve-ak.last.fm/serve/500/38524543/Shakira.png"))}
			};

			this.BindingContext = _model;
		}
	}
}

