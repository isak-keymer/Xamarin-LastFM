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
			SearchResult = new ObservableCollection<ViewModel> ();
			GroupedItems = new ObservableCollection<Group<string, ViewModel>> ();
		}

		public ObservableCollection<Group<string,ViewModel>> GroupedItems { get; set;} 

		public ObservableCollection<ViewModel> SearchResult { get; set;}

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