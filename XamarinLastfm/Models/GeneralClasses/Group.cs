using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace XamarinLastfm
{
	public class Group<K, T> : ObservableCollection<T>
	{
		public K Key { get; private set; }

		public Group(K key, IEnumerable<T> items)
		{
			Key = key;
			foreach (var item in items)
				this.Items.Add(item);
		}
	}
}

