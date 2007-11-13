using System;
using Gtk;
using Monoplayer.windows;
using Monoplayer.Player;

namespace Monoplayer {
	
	namespace windows {
		
		public partial class MainWindow: Gtk.Window {
			protected Controller controller;
			
			public MainWindow (Controller ctrl): base (Gtk.WindowType.Toplevel) {
				Build ();
				this.controller = ctrl;
				menus_box.Add(controller.menu.menubar);
				centro.Add(controller.GetButtons());
				controller.SetMainWindow(this);
				controller.ViewsController("list_view");
			}
	
			protected void OnDeleteEvent (object sender, DeleteEventArgs a) {
				controller.MainQuit();
				a.RetVal = true;
			}
			public void UpdateTime(string curr, string tot) {
				time.Markup = curr+" / "+tot;
			}
			public void UpdateProgress(float prog) {
				progress.Fraction = prog;
			}
			public VBox GetCentro() {
				return centro;
			}
		}
	}
}