using System;
using Microsoft.WindowsAzure.MobileServices;

namespace UIRisRetro
{
	public class AzureRetroItemService : InMemoryRetroItemService
	{
		public static MobileServiceClient MobileService = new MobileServiceClient(
			"https://uiris-retro.azure-mobile.net/",
			"qsWecGOYytDennfVYUbsQwTWmQlesy51"
		);

		public AzureRetroItemService ()
		{
		}
	}
}

