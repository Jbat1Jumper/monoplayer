// playing_info.cs
// Andrés Villagrán Placencia - andres@villagranquiroz.cl
// [ Villagrán Quiroz ] Servicios informáticos
// http://www.villagranquiroz.cl
//

using System;
using System.Drawing;
using System.Drawing.Imaging;
using Monoplayer.Player;
using Monoplayer.windows;
using Monoplayer.windows.player;
using Monoplayer.Data;
using Gtk;
using TagLib;
using GLib;

namespace Monoplayer
{
	namespace Player {
	
		public class Controller
		{
			private string song;
			private string album;
			private string length;
			private string author;
			private string track;
			private string CoverFilename;
			private int duration_min, duration_sec;
			private int cTime, min, sec;
			// Monoplayer internal objects
			private MediaPlayer player;
			private buttons btns;
			private MainWindow mainWindow;
			private Menus menus;
			private MediaController mediaController;
			private MediaDataBase mediaDB;
			private DataBase dataBase;
			private ConfigManager configmgr;
			private Gtk.VBox centro;
			private list_view view;

			
			public Controller() {
				cTime = 0; min = 0; sec = 0;
				// Creating Monoplayer modules
				player = new MediaPlayer(this);
				btns = new buttons(this);
				mediaController = new MediaController(this);
				dataBase = new DataBase();
				mediaDB = new MediaDataBase(this);
				menus = new Menus(this);
				configmgr = new ConfigManager(this);
				
			}
			public MediaPlayer GetPlayer() {
				return player;
			}
			public buttons GetButtons() {
				return btns;
			}
			// Gets - Sets
			public MediaDataBase mediaDataBase {
				get {
					return mediaDB;
				}
			}
			public DataBase database {
				get {
					return dataBase;
				}
			}
			public Menus menu {
				get {
					return menus;
				}
			}
			public MainWindow mainwindow {
				get {
					return mainWindow;
				}
			}
			public ConfigManager configManager {
				get {
					return configmgr;
				}
			}
			public void SetMainWindow(MainWindow mw) {
				mainWindow = mw;
				centro = mainWindow.GetCentro();
				GLib.Timeout.Add(1000, TimeHandler);
				GLib.Timeout.Add(1000, ProgressHandler);
			}
			public bool MediaPlayer_Play() {
				if(player.Play()) {
					return true;
				}
				else {
					return false;
				}
			}
			public bool MediaPlayer_Pause() {
				if(player.Pause()) {
					return true;
				} else {
					return false;
				}
			}
			public bool MediaPlayer_LoadFile(string file) {
				if(player.LoadFile(file)) {		 
					return true;
				} else
					return false;
			}
			
			public void MediaPlayer_GetTags() {
				TagLib.File file = player.GetTags();
				song = file.Tag.Title;
				album = file.Tag.Album;
				author = file.Tag.FirstPerformer;
				track = file.Tag.Track+"/"+file.Tag.TrackCount;
				duration_sec = file.Properties.Duration.Seconds;
				duration_min = file.Properties.Duration.Minutes;
				try {
					System.IO.File.Delete(configManager.ConfigPath+"cover.png");
					System.IO.File.Delete(configManager.ConfigPath+"cover.jpg");
					System.IO.File.Delete(configManager.ConfigPath+"cover.gif");
				} catch (Exception) {}
			
				foreach(TagLib.IPicture p in file.Tag.Pictures) {
					Console.WriteLine("Picture:"+p.MimeType.ToString());
					switch(p.MimeType.ToString()) {
						case "image/jpg":
							CoverFilename = "cover.jpg";
							break;
						case "image/gif":
							CoverFilename = "cover.gif";
							break;
						case "image/png":
							CoverFilename = "cover.png";
							break;
					}
					System.IO.FileStream pic = System.IO.File.Create(configManager.ConfigPath+CoverFilename);
					foreach(byte bit in p.Data.Data) {
						pic.WriteByte(bit);
					}
					pic.Close();
				}
				if(song == null) song = "[Desconocido]";
				if(album == null) album = "[Desconocido]";
				if(author == null) author = "[Desconocido]";
			}
			public TagLib.File MediaPlayer_GetTagFile(string File) {
				return player.GetTagFile(File);
			}
			public void MediaController_GetStore(out TreeStore store, out int SongCount) {
				TreeStore instore;
				int inSongCount;
				mediaController.GetStore(out instore, out inSongCount);
				store = instore;
				SongCount = inSongCount; 
			}
			public void MediaController_GetSongs(out TreeStore store, out int SongCount) {
				TreeStore instore;
				int inSongCount;
				mediaController.GetSongs(out instore, out inSongCount);
				store = instore;
				SongCount = inSongCount; 
			}
			public void View_NextSong() {
				view.NextSong();
			}
			public void View_PrevSong() {
				view.PrevSong();
			}
			public void View_ThreadLoadSongs() {
				view.ThreadLoadSongs();
			}
			public void buttons_SetMediaInfo() {
				btns.SetMediaInfo(song, length, album, author, track, CoverFilename);
			}
			private bool TimeHandler() {
				if(player.isPlaying() == true) {
					cTime += 1;
					
					if(cTime > 59) {
						min +=1;
						cTime = 0;
						sec = 0;
					}
					else {
						sec += 1;
					}
					mainWindow.UpdateTime(String.Format("{0:00}:{1:00}", min, sec), 
					                      String.Format("{0:00}:{1:00}", duration_min, duration_sec));
				}
				return true;
			}
			private bool ProgressHandler() {
				if(player.isPlaying() == true) {
					int cur = (min*60)+sec; // Calculating current time in seconds
					int tot = (duration_min*60)+duration_sec; // Calculating duration (song) time in seconds
					float progress = ((cur*100)/tot);
					progress = progress/100;
					mainWindow.UpdateProgress(progress);
				}
				return true;
			}
			public void ResetTimer() {
				cTime = 0;
				min = 0;
				sec = 0;
			}
			public void ViewsController(string LoadView) {
				try { centro.Children[1].Destroy(); } catch(Exception){};
				switch(LoadView) {
					case "list_view":
						view = new list_view(this);
						centro.Add(view);
						break;
				}
			}
			public void MainQuit() {
				player.QuitLoop();
				dataBase.Cerrar();
				Application.Quit ();			
			}
		}
	}
}
