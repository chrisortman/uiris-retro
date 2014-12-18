using System;
using Xamarin.Forms;

namespace UIRisRetro
{
	public class App
	{
		public static RetroItemService Service = new InMemoryRetroItemService();

		public static Page GetMainPage ()
		{	
			return new NavigationPage(new RetroItemListPage ());
		}
	}
}

