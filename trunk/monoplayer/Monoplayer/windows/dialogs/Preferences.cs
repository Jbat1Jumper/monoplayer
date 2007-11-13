// Preferences.cs
// Andrés Villagrán Placencia - andres@villagranquiroz.cl
// [ Villagrán Quiroz ] Servicios informáticos
// http://www.villagranquiroz.cl
//

using System;
using Gtk;
using Monoplayer.Player;

namespace Monoplayer {
	namespace windows {
		namespace dialogs {
			public partial class Preferences : Window {
				private Controller controller;
				
				public Preferences(Controller ctrl) : base(Gtk.WindowType.Toplevel) {
					this.Build();
					controller = ctrl;
					filepath.Text = controller.configManager.DefaultPath;
				}

				protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args) {
					this.Destroy();
				}

				protected virtual void OnBtnScanClicked (object sender, System.EventArgs e) {
					controller.database.Ejecutar("DELETE FROM Songs");					
					controller.mediaDataBase.FindMp3Files(filepath.Text);
					controller.View_ThreadLoadSongs();
					this.Destroy();
				}

				protected virtual void OnSelectDirClicked (object sender, System.EventArgs e) {
					
					FileChooserDialog fc =
						new FileChooserDialog("Select directory of Music", this,
						                  FileChooserAction.SelectFolder,
						                  "Cancel", ResponseType.Cancel, 
						                  "Accept", ResponseType.Accept);
					if(fc.Run() == (int)ResponseType.Accept) {
						filepath.Text = fc.Filename;
						controller.configManager.DefaultPath = fc.Filename;
					}
					fc.Destroy();
				}
			}
		}
	}
}