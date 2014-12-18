using System;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace UIRisRetro
{
	public enum RetroItemKind {
		Positive,
		Negative
	}

	public class RetroItem : INotifyPropertyChanged {
		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		RetroItemKind _kind;
		string _description;
		int _votes;

		public string Id { get; set; }

		public RetroItemKind Kind {
			get {
				return _kind;
			}
			set {
				_kind = value;
				OnPropertyChanged ();
				OnPropertyChanged ("KindDisplay");
				OnPropertyChanged ("IsPositive");
			}
		}

		public string KindDisplay {
			get {
				if (Kind == RetroItemKind.Positive) {
					return "+";
				} else {
					return "-";
				}
			}
		}

		public bool IsPositive {
			get {
				return Kind == RetroItemKind.Positive;
			}
			set {
				Kind = value ? RetroItemKind.Positive : RetroItemKind.Negative;
			}
		}

		public string Description {
			get {
				return _description;
			}
			set {
				_description = value;
				OnPropertyChanged ();
			}
		}

		public int Votes {
			get {
				return _votes;
			}
			set {
				_votes = value;
				OnPropertyChanged ();
			}
		}

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			var handler = PropertyChanged;
			if(handler != null) {
				handler(this,new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}

