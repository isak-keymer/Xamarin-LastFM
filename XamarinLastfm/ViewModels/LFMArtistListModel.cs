using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace XamarinLastfm
{
	public class LFMArtistListModel
	{
		//		private List<ArtistListViewModel> _artistList;
		//
		//		public LFMArtistListModel ()
		//		{
		//			_artistList = new List<ArtistListViewModel> ();
		//		}
		//
		//		public List<ArtistListViewModel> ArtistList {
		//			get
		//			{ 
		//				return _artistList;		
		//			}
		//
		//			set
		//			{ 
		//				if (_artistList != value)
		//				{
		//					_artistList = value;
		//					OnPropertyChanged("ArtistList");
		//				}
		//			}
		//		}
		//
		//		public event PropertyChangedEventHandler PropertyChanged;
		//
		//		protected virtual void OnPropertyChanged(string propChanged)
		//		{
		//			if (PropertyChanged != null)
		//				PropertyChanged (this, new PropertyChangedEventArgs (propChanged));
		//		}
		public LFMArtistListModel ()
		{
			ArtistList = new ObservableCollection<ArtistListViewModel> ();
		}
		public ObservableCollection<ArtistListViewModel> ArtistList { get; set;}
	}
}

