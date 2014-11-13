using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinLastfm
{	
	public partial class AlbumFullInfoPage : TabbedPage
	{	
		private AlbumFullInfoViewModel _model;

		public AlbumFullInfoPage (string album, string albumMbid, string artistName)
		{
			InitializeComponent ();

			this.Title = album;

			_model = new AlbumFullInfoViewModel ();

			GetAlbum (album, albumMbid, artistName);

			this.Icon = "lastfmlogo48.png";

			NavigationPage.SetTitleIcon(this, this.Icon);
		}

		async void GetAlbum(string album, string albumMbid, string artistName)
		{
			_model = await LFMService.Instance.GetAlbumFullInfo (album, album, artistName);

			this.BindingContext = _model;
		}
	}
}

