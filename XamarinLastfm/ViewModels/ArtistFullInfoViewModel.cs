﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XamarinLastfm
{
	public class ArtistFullInfoViewModel : INotifyPropertyChanged
	{
		private string _name;
		public string Mbid { get; set; }
		public string Url { get; set; }
		public string ContentSummary { get; set; }
		public int YearFormed { get; set; }
		public string Published { get; set; }
		public ImageSource ImageSource { get; set; }
		public IEnumerable<ViewModel> Albums { get; set; }
		public IEnumerable<ViewModel> SimilarArtists { get; set; }

		public string Name {
			get { 
				return _name; 
			}
			set {
				if (value != _name) {
					_name = value;
					NotifyPropertyChanged ();
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

