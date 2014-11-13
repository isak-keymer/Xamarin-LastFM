using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace XamarinLastfm
{
	public class LFMArtistListModel : INotifyPropertyChanged
	{			
		public LFMArtistListModel ()
		{
			ArtistList = new ObservableCollection<ArtistListViewModel> ();
		}
		public ObservableCollection<ArtistListViewModel> ArtistList { get; set;}

		private bool _isLoading;

		public bool IsLoading 
		{
			get { return _isLoading; }
		
			set { 
				_isLoading = value;
				OnPropertyChanged ("IsLoading");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propChanged)
		{
			if (PropertyChanged != null)
				PropertyChanged (this, new PropertyChangedEventArgs (propChanged));
		}
	}
}



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