using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Collections.Generic;

namespace XamarinLastfm
{
	public class AlbumFullInfoViewModel
	{
		private string _name;
		public string Id { get; set; }
		public string ReleaseDate { get; set; }
		public ImageSource ImageSource { get; set; }
		public List<Track> Tracks { get; set; }

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

