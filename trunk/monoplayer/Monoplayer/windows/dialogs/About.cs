// About.cs
// Andrés Villagrán Placencia - andres@villagranquiroz.cl
// [ Villagrán Quiroz ] Servicios informáticos
// http://www.villagranquiroz.cl
//

using System;

namespace Monoplayer {
	namespace windows {
		namespace dialogs {
	
			public partial class About : Gtk.Dialog {
		
				public About()
				{
					this.Build();
				}

				protected virtual void OnButtonCloseClicked (object sender, System.EventArgs e)
				{
					this.Destroy();
				}
			}
		}
	}
}