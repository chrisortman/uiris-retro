using System;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

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

		protected async Task EnsureInitialized() {
				
		}

		protected IMobileServiceTable<RetroItem> RetroItems {
			get { return MobileService.GetTable<RetroItem> (); }
		}

		public override async System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<RetroItem>> FindAllItems ()
		{
			await EnsureInitialized ();

			var items = await RetroItems.CreateQuery ().ToEnumerableAsync();
			return items;
		}

		public override async System.Threading.Tasks.Task Save (RetroItem retroItem)
		{
			await EnsureInitialized ();
			if (String.IsNullOrWhiteSpace (retroItem.Id)) {
				await RetroItems.InsertAsync (retroItem);
			} else {
				await RetroItems.UpdateAsync (retroItem);
			}
		}

		public override async System.Threading.Tasks.Task Delete (RetroItem retroItem)
		{
			await EnsureInitialized ();
			await RetroItems.DeleteAsync (retroItem);
		}
	}
}

