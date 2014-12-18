using System;
using Xamarin.Forms;

namespace UIRisRetro
{
	public class App
	{
		public static RetroItemService Service = new AzureRetroItemService();

		public static Page GetMainPage ()
		{	
			return new NavigationPage(new RetroItemListPage ());
		}
	}
}

