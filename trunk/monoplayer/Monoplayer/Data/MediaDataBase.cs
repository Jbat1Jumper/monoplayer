// MediaDataBase.cs
// Andrés Villagrán Placencia - andres@villagranquiroz.cl
// [ Villagrán Quiroz ] Servicios informáticos
// http://www.villagranquiroz.cl
//

using System;
using System.Collections;
using System.IO;
using Monoplayer.Player;
using Monoplayer.Data;

namespace Monoplayer {
	namespace Data {
	
		public class MediaDataBase {
			private ArrayList Songlist;
			private Controller controller;
			private DataBase db;
			
			public MediaDataBase(Controller ctrl) {
				controller = ctrl;
				Songlist = new ArrayList();
				db = controller.database;
			}
			
			public void FindMp3Files(string path) {
				foreach (string foundDir in Directory.GetDirectories(@path)) {
					Console.WriteLine("Dir: {0}", foundDir);
					FindMp3Files(foundDir);
				}
				foreach (string strFileName in Directory.GetFiles(@path, "*.mp3")) {
					Console.WriteLine("\t"+strFileName);
					TagLib.File file = controller.MediaPlayer_GetTagFile(strFileName);
					string title;
					if(file.Tag.Title == "") {
						string[] split = strFileName.Split('/');
						title = split[split.Length-1];
					} else title = file.Tag.Title;
					string[,] datos = {
						{"path", strFileName}, 
						{"title", title}, 
						{"album", file.Tag.Album}, 
						{"artist", file.Tag.FirstPerformer},
						{"length", String.Format("{0:00}:{1:00}", file.Properties.Duration.Minutes,
							                         file.Properties.Duration.Seconds)}
					};
					db.Insert(datos, "Songs");
				}
			} 
			private void ReadDbSongs() {
				Songlist = db.ToArrayList(db.LeeDatos("SELECT * FROM Songs"));
			}
			public ArrayList SongList {
				get {
					ReadDbSongs();
					return Songlist;
				}
				set {
					Songlist = value;
				}
			}
			
		}
	}
}
