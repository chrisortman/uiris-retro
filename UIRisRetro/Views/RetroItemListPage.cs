using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UIRisRetro
{
	public class RetroItemListPage : ContentPage {

		ListView _listView;

		public RetroItemListPage() {
			Title = "Retro Items";

			_listView = new ListView ();
			_listView.ItemTemplate = new DataTemplate (typeof(RetroItemCell));
			_listView.ItemSelected += (sender, e) => {
				var retroItem = (RetroItem)e.SelectedItem;
				var todoPage = new RetroItemPage();
				todoPage.BindingContext = retroItem;
				Navigation.PushAsync(todoPage);
			};		

			var	tbi = new ToolbarItem("Add", null, () => {
				var retroItem = new RetroItem();
				var todoPage = new RetroItemPage();
				todoPage.BindingContext = retroItem;
				Navigation.PushAsync(todoPage);
			}, 0, 0);

			ToolbarItems.Add (tbi);

			var loginButton = new Button () {
				Text = "Login",
			};
			loginButton.Clicked += async (sender, e) => {
				await Navigation.PushModalAsync(new LoginPage());
			};

			loginButton.BindingContext = App.Service;
			loginButton.SetBinding (Button.IsVisibleProperty,"IsLoggedIn",BindingMode.OneWay,new OppositeValueConverter());

			var layout = new StackLayout () {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					_listView,
					loginButton,
				}
			};

			Content = layout;

		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();

			_listView.ItemsSource = await App.Service.FindAllItems ();
		}
	}

	public class OppositeValueConverter : IValueConverter {
		#region IValueConverter implementation

		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			bool v = (bool)value;
			return !v;
		}

		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException ();
		}

		#endregion


	}
}

