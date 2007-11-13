// Menu.cs
// Andrés Villagrán Placencia - andres@villagranquiroz.cl
// [ Villagrán Quiroz ] Servicios informáticos
// http://www.villagranquiroz.cl
//

using System;
using Gtk;
using Monoplayer.Player;
using Monoplayer.windows.dialogs;

namespace Monoplayer
{
	namespace windows {
	
		public class Menus {
			private MenuBar menuBar;
			private Controller controller;
			
			public Menus(Controller ctrl)  {
				controller = ctrl;
				CreateMenuBar();
				MusicMenu();
				HelpMenu();
				menuBar.ShowAll();
				
			}
			public MenuBar menubar {
				get {
					return menuBar;
				}
			}
			private void MusicMenu() {
				MenuItem Music = new MenuItem("Music");
				Menu music = new Menu();
				Music.Submenu = music;
				menuBar.Append(Music);

				ImageMenuItem AddDir = new ImageMenuItem("Add Directory");
				AddDir.Image = new Image(Stock.Add, IconSize.Menu);
				AddDir.Activated += new EventHandler(AddDirActivated);
				music.Append(AddDir);
				
				ImageMenuItem CleanDB = new ImageMenuItem("Clean Database");
				CleanDB.Image = new Image(Stock.Clear, IconSize.Menu);
				CleanDB.Activated += new EventHandler(CleanDBActivated);
				music.Append(CleanDB);
				
				music.Append(new SeparatorMenuItem());
				
				ImageMenuItem Preferences = new ImageMenuItem("Preferences");
				Preferences.Image = new Image(Stock.Preferences, IconSize.Menu);
				Preferences.Activated += new EventHandler(PreferencesActivated);
				music.Append(Preferences);
				
				music.Append(new SeparatorMenuItem());
				
				ImageMenuItem Exit = new ImageMenuItem("Quit");
				Exit.Image = new Image(Stock.Quit, IconSize.Menu);
				Exit.Activated += new EventHandler(QuitActivated);
				music.Append(Exit);
				
				
			}
			private void HelpMenu() {
				MenuItem Help = new MenuItem("Help");
				Menu help = new Menu();
				Help.Submenu = help;
				menuBar.Append(Help);
				
				ImageMenuItem about = new ImageMenuItem("About");
				about.Image = new Image(Stock.Help, IconSize.Menu);
				about.Activated += new EventHandler(aboutActivated);
				help.Append(about);
			}
			private void CreateMenuBar() {
				menuBar = new MenuBar();
			}
			// Starts the Event Handlers
			protected void AddDirActivated(object o, System.EventArgs args) {
				FileChooserDialog fc =
						new FileChooserDialog("Select directory of Music to Add", controller.mainwindow,
						                  FileChooserAction.SelectFolder,
						                  "Cancel", ResponseType.Cancel, 
						                  "Accept", ResponseType.Accept);
					if(fc.Run() == (int)ResponseType.Accept) {
						controller.mediaDataBase.FindMp3Files(fc.Filename);
					    controller.View_ThreadLoadSongs();
					}
					fc.Destroy();
			}
			protected void CleanDBActivated(object o, System.EventArgs args) {
				Dialog dlg = new Dialog("You will delete the database, are you sure?", controller.mainwindow,
				                        DialogFlags.Modal);
				dlg.AddButton("Yes, clean the database", ResponseType.Accept);
				dlg.AddButton("No", ResponseType.Cancel);
				if(dlg.Run() == (int)ResponseType.Accept) {
					controller.database.Ejecutar("DELETE FROM Songs");
					controller.View_ThreadLoadSongs();
				}
				dlg.Destroy();
			}
			protected void PreferencesActivated(object o, System.EventArgs args) {
				new Preferences(controller);
			}
			protected void QuitActivated(object o, System.EventArgs args) {
				controller.MainQuit();
			}
			protected void aboutActivated(object o, System.EventArgs args) {
				
				new About();
			}
		}
	}
}