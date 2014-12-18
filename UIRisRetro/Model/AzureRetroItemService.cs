using System;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

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
			if (!MobileService.SyncContext.IsInitialized) {
				var store = new MobileServiceSQLiteStore ("uirisretro.db");
				store.DefineTable<RetroItem> ();
				await MobileService.SyncContext.InitializeAsync (store);
			}
		}

		protected IMobileServiceSyncTable<RetroItem> RetroItems {
			get { return MobileService.GetSyncTable<RetroItem> (); }
		}

		public override async System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<RetroItem>> FindAllItems ()
		{
			await EnsureInitialized ();

			try {
				await RetroItems.PullAsync ("allItems",RetroItems.CreateQuery());
			} catch (MobileServicePushFailedException e) {
				//swallow should probalby log
				System.Diagnostics.Debug.WriteLine (e.ToString ());
			}
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

