using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UIRisRetro
{
	public class RetroItemPage : ContentPage {
		public RetroItemPage() {
			//this.SetBinding (ContentPage.TitleProperty, "Retro Item");

			NavigationPage.SetHasNavigationBar (this, true);
			var nameLabel = new Label { Text = "Description" };
			var nameEntry = new Entry ();

			nameEntry.SetBinding (Entry.TextProperty, "Description");

			var positiveLabel = new Label { Text = "+" };
			var negativeLabel = new Label { Text = "-" };
			var kindEntry = new Switch ();
			kindEntry.SetBinding (Switch.IsToggledProperty, "IsPositive");

			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += async (sender, e) => {
				var retroItem = (RetroItem)BindingContext;
				await App.Service.Save(retroItem);
				await this.Navigation.PopAsync();
			};

			var deleteButton = new Button { Text = "Delete" };
			deleteButton.Clicked += async (sender, e) => {
				var retroItem = (RetroItem)BindingContext;
				await App.Service.Delete(retroItem);
				await this.Navigation.PopAsync();
			};

			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += (sender, e) => {

				this.Navigation.PopAsync();
			};

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20),
				Children = {
					nameLabel, nameEntry, 
					new StackLayout {
						Orientation = StackOrientation.Horizontal,
						Children = {
							negativeLabel,kindEntry,positiveLabel
						}
					},

					saveButton, deleteButton, cancelButton,

				}
			};
		}
	}
}

