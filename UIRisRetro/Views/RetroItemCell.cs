using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UIRisRetro
{
	public class RetroItemCell : ViewCell {

		public RetroItemCell() {
			var kindLabel = new Label () {
				YAlign = TextAlignment.Center,
				XAlign = TextAlignment.Start,
			};
			kindLabel.SetBinding (Label.TextProperty, "KindDisplay");

			var descriptionLabel = new Label () {
				XAlign = TextAlignment.Start,
				YAlign = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};
			descriptionLabel.SetBinding (Label.TextProperty, "Description");

			var voteButton = new Button ();
			voteButton.SetBinding (Button.TextProperty, "Votes",BindingMode.OneWay,new VotesValueConverter());
			voteButton.Clicked += (sender, e) => {
				var retroItem = (RetroItem)BindingContext;
				retroItem.Votes++;
				App.Service.Save(retroItem);
			};

			View = new StackLayout () {

				Padding = new Thickness(20, 0, 0, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { kindLabel, descriptionLabel, voteButton }
			};
		}
	}

	public class VotesValueConverter : IValueConverter {
		#region IValueConverter implementation

		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return String.Format ("{0} votes", value);
		}

		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException ();
		}

		#endregion


	}
}

