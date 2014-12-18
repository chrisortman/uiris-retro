using System;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UIRisRetro
{
	public class InMemoryRetroItemService : RetroItemService, INotifyPropertyChanged{
		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		private List<RetroItem> _items;
		private bool _isLoggedIn;

		public InMemoryRetroItemService() {
			_items = new List<RetroItem> () {
				new RetroItem {
					Kind = RetroItemKind.Positive,
					Description = "Item 1",
				},

				new RetroItem {
					Kind = RetroItemKind.Negative,
					Description = "Item 2",
				}
			};

			_isLoggedIn = false;
		}

		public virtual Task<IEnumerable<RetroItem>> FindAllItems() {
			var t = new TaskCompletionSource<IEnumerable<RetroItem>> ();

			t.SetResult (_items.ToArray ());

			return t.Task;
		}

		public virtual Task Save(RetroItem retroItem) {

			var t = new TaskCompletionSource<bool> ();

			if (!_items.Contains (retroItem)) {
				_items.Add (retroItem);
			}
			t.SetResult (true);

			return t.Task;
		}

		public virtual Task Delete(RetroItem retroItem) {
			var t = new TaskCompletionSource<bool> ();

			if (_items.Contains (retroItem)) {
				_items.Remove (retroItem);
			}
			t.SetResult (true);

			return t.Task;
		}

		public virtual bool IsLoggedIn {
			get { return _isLoggedIn; }
			set { _isLoggedIn = value; OnPropertyChanged (); }
		}

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			var handler = PropertyChanged;
			if (handler != null) {
				handler (this, new PropertyChangedEventArgs (propertyName));
			}
		}
	}
}

