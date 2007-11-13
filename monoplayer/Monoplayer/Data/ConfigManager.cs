// ConfigManager.cs
// Andrés Villagrán Placencia - andres@villagranquiroz.cl
// [ Villagrán Quiroz ] Servicios informáticos
// http://www.villagranquiroz.cl
//

using System;
using System.Collections;
using Monoplayer.Player;
using Monoplayer.Data;

namespace Monoplayer {
	namespace Data {
	
		public class ConfigManager {
			private Controller controller;
			private string defaultPath;
			private DataBase db;
			private string configPath;
			
			public ConfigManager(Controller ctrl) {
				controller = ctrl;
				db = controller.database;
				LoadConfig();
				SetConfigPath();
			}
			/// <summary>
			/// Create the config directory (Cover image, plugins, etc)
			/// </summary>
			private void SetConfigPath() {
				// Another idea for getting user path?
				string username = Mono.Unix.UnixEnvironment.UserName;
				Console.WriteLine("Username: "+username);
				string configPath = "/home/"+username+"/.config/Monoplayer/";
				if(!System.IO.Directory.Exists(configPath)) {
					Console.WriteLine("Config Path dont exist, Creating ["+configPath+"]");
					System.IO.Directory.CreateDirectory(configPath);
				}
				this.configPath = configPath;
			}
			/// <value>
			/// Return the path where are saving files and configs
			/// </value>
			public string ConfigPath {
				get {
					return configPath;
				}
			}
			/// <value>
			/// The default MEDIA path
			/// </value>
			public string DefaultPath {
				get {
					LoadConfig();
					return defaultPath;
				}
				set {
					db.Ejecutar("UPDATE Config SET value = '"+value+"' WHERE name = 'DefaultPath';");
					LoadConfig();
				}
			}
			/// <summary>
			/// Load Configs from database
			/// </summary>
			private void LoadConfig() {
				try {
					ArrayList data = 
						db.ToArrayList(db.LeeDatos("SELECT value FROM Config WHERE name = 'DefaultPath' LIMIT 1;"));
					foreach(string[] path in data) {
						Console.WriteLine("Default Path: "+path[0]);
						defaultPath = path[0];
					}
					
				} catch(Exception) {
					Console.WriteLine("DefaultPath its not defined");
				}
			}
		}
	}
}