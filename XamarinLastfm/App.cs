using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace XamarinLastfm
{
	public class App
	{
		public static Page GetMainPage()
		{
			var navpage = new NavigationPage (new ArtistListPage ());

			navpage.BarBackgroundColor = Color.White;
			navpage.BarTextColor = Color.FromHex ("e31b23");

			return navpage;
		}
	}
}

