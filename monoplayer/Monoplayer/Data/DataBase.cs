using Mono.Data.SqliteClient;
using System;
using System.Data;
using System.Collections;

namespace Monoplayer {
	namespace Data {
		public class DataBase {
			string 			database;
			SqliteConnection 	dbcon;
			SqliteCommand 		dbcmd;
	
			public DataBase() {
				this.database 		= "URI=file:database.db,version=3";
				this.dbcon 		= new SqliteConnection(database);
				dbcon.Open();		
			}
	
			public IDataReader LeeDatos(string SQL) {
				dbcmd = (SqliteCommand) dbcon.CreateCommand();
				dbcmd.CommandText = SQL;
				
				return dbcmd.ExecuteReader();
			}
			public bool Ejecutar(string SQL) {
				//Console.WriteLine("SQL: "+SQL);
				try {
					dbcmd = (SqliteCommand) dbcon.CreateCommand();
					dbcmd.CommandText = SQL;
					dbcmd.ExecuteReader();
					return true;
				} catch(Exception e) {
					Console.WriteLine(e);
					return false;
				}		
			}
			public ArrayList ToArrayList(IDataReader rd) {
				ArrayList arreglo = new ArrayList();
				//Console.WriteLine("Depth: "+rd.Depth);
				//Console.WriteLine("FieldCount:"+rd.FieldCount);
			
				string[] datos;
				
				while(rd.Read()) {
					datos = new string[rd.FieldCount]; 
					for(int x=0; x<rd.FieldCount; x++) {
						datos[x] = rd.GetString(x);
					}
					arreglo.Add(datos);
				}
				return arreglo;
			}
			public bool Insert(string[,] datos, string tabla) {

				string SQL_inicio, SQL_fin, SQL;
			
				SQL_inicio = "INSERT INTO '"+tabla+"' (";

				SQL_fin = "VALUES (";
			
				Console.WriteLine("Tabla: "+tabla);
				for(int x=0; x < (datos.Length/datos.Rank); x++) {
					if(x==((datos.Length/datos.Rank)-1)) {
						SQL_inicio += "'"+datos[x, 0].Replace("'", "\'")+"') ";
						SQL_fin += "'"+datos[x, 1].Replace("'", "\'")+"'); ";
					} else {
						SQL_inicio += "'"+datos[x, 0].Replace("'", "\'")+"', ";
						SQL_fin += "'"+datos[x, 1].Replace("'", "\'")+"', ";
					}
				} 
				Console.WriteLine(SQL_inicio+SQL_fin);
			
				SQL = SQL_inicio+SQL_fin;
			
				return this.Ejecutar(SQL);
			}
			public void Cerrar() {
				dbcmd.Dispose();
				dbcmd = null;
				dbcon.Close();
				dbcon = null;
			}
		}
	}
}