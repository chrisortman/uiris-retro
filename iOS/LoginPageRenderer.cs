using System;
using Microsoft.WindowsAzure.MobileServices;

[assembly: Xamarin.Forms.ExportRenderer(typeof(UIRisRetro.LoginPage),typeof(UIRisRetro.iOS.LoginPageRenderer))]

namespace UIRisRetro.iOS
{
	public class LoginPageRenderer : Xamarin.Forms.Platform.iOS.PageRenderer
	{
		public LoginPageRenderer ()
		{
		}

		public override async void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			if (!App.Service.IsLoggedIn) {

				if (App.Service is AzureRetroItemService) {
					var user = await AzureRetroItemService.MobileService.LoginAsync (this, MobileServiceAuthenticationProvider.MicrosoftAccount);
					if (user != null) {
						App.Service.IsLoggedIn = true;
						AzureRetroItemService.MobileService.CurrentUser = user;
					}
				}

				if (this.PresentingViewController != null) {
					this.PresentingViewController.DismissViewController (animated, null);

				}
			}

		}
	}
}

