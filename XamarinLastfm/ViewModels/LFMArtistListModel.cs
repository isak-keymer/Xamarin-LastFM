using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace XamarinLastfm
{
	public class LFMArtistListModel : INotifyPropertyChanged
	{
		private List<ArtistListViewModel> _artistList;
		
		public LFMArtistListModel ()
		{
			_artistList = new List<ArtistListViewModel> ();
		}

		public List<ArtistListViewModel> ArtistList 
		{
			get
			{ 
				return _artistList;		
			}

			set
			{ 
				if (_artistList != value)
				{
					_artistList = value;
					NotifyPropertyChanged("ArtistList");
				}
			}
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		private void NotifyPropertyChanged ([CallerMemberName] string propertyName = "")
		{
			if (PropertyChanged != null) {
				PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
			}
		}
	}
}

