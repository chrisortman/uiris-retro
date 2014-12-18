using System;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UIRisRetro
{

	public interface RetroItemService {
		bool IsLoggedIn {get;set;}
		Task<IEnumerable<RetroItem>> FindAllItems ();
		Task Save (RetroItem retroItem);
		Task Delete (RetroItem retroItem);

	}
	
}
