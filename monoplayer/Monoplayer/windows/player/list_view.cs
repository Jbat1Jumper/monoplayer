// list_view.cs
// Andrés Villagrán Placencia - andres@villagranquiroz.cl
// [ Villagrán Quiroz ] Servicios informáticos
// http://www.villagranquiroz.cl
//

using System;
using Gtk;
using Monoplayer.Player;
using System.IO;
using System.Threading;
using TagLib;

namespace Monoplayer
{
	namespace windows {
		namespace player {
			public partial class list_view : Gtk.Bin {
				TreeStore store;
				Controller controller;
				int SongCount;
				
				public list_view(Controller ctrl) {
					this.Build();
					SongCount = 0;
					controller = ctrl;
					controller.MediaController_GetStore(out store, 
					                                    out SongCount);	
					tv_list.Model = store;
					
					//tv_list.AppendColumn("", new CellRendererText(), "text", 0);
					tv_list.AppendColumn("Title", new CellRendererText(), "text", 1);
					tv_list.AppendColumn("Album", new CellRendererText(), "text", 2);
					tv_list.AppendColumn("Artist", new CellRendererText(), "text", 3);
					tv_list.AppendColumn("Length", new CellRendererText(), "text", 4);
					tv_list.EnableSearch = true;
					tv_list.HeadersClickable = true;
					//tv_list.Reorderable = true;
					TreeViewColumn[] columns = tv_list.Columns;
					// Setting the columns resizable
					columns[0].Resizable = true;
					columns[1].Resizable = true;
					columns[2].Resizable = true;
					columns[3].Resizable = true;
					
					columns[0].Clicked += new EventHandler(OnClickedColumn);
					columns[1].Clicked += new EventHandler(OnClickedColumn);
					columns[2].Clicked += new EventHandler(OnClickedColumn);
					columns[3].Clicked += new EventHandler(OnClickedColumn);
					ThreadLoadSongs();
					
				}
				private void DesactivateSortIndicators() {
					TreeViewColumn[] columns = tv_list.Columns;
					columns[0].SortIndicator = false;
					columns[1].SortIndicator = false;
					columns[2].SortIndicator = false;
					columns[3].SortIndicator = false;
				}
						
				protected void OnClickedColumn (object sender, EventArgs args) {
					TreeViewColumn col = (TreeViewColumn) sender;
					
					DesactivateSortIndicators();
					col.SortIndicator = true;

					int colN = 0;
					switch(col.Title) {
						case "Title":
							colN = 1;
							break;
						case "Album":
							colN = 2;
							break;
						case "Artist":
							colN = 3;
							break;
						case "Length":
							colN = 4;
							break;
					}
					
					switch(col.SortOrder.ToString()) {
						case "Ascending":
							col.SortOrder = SortType.Descending;
							store.SetSortColumnId(colN, SortType.Descending);
							break;
						case "Descending":
							col.SortOrder = SortType.Ascending;
							store.SetSortColumnId(colN, SortType.Ascending);
							break;
						default:
							col.SortOrder = SortType.Ascending;
							store.SetSortColumnId(colN, SortType.Ascending);
							break;
					}
				}
				
				public void ThreadLoadSongs() {
					Thread ThreadLoadSongs = new Thread(new ThreadStart(LoadSongs));
					ThreadLoadSongs.Start();
					ThreadLoadSongs.Join();
				}
				public void LoadSongs() {
					controller.MediaController_GetSongs(out store, 
					                                    out SongCount);	
				}
				protected virtual void OnTvListRowActivated (object o, Gtk.RowActivatedArgs args)
				{
					TreeModel model;
					TreeIter  iter;
					
					if(tv_list.Selection.GetSelected(out model, out iter)) {
						controller.MediaPlayer_LoadFile(String.Format("{0}", model.GetValue(iter, 0)));
						controller.MediaPlayer_Play();
					}
				}
				public void NextSong() {
					TreeIter iter;
					TreePath path;
					TreeViewColumn col;
					if(tv_list.Selection.GetSelected(out iter)) {
					
						tv_list.GetCursor(out path, out col);
						
						path.Next();
						if(path.ToString() == SongCount.ToString()) {
							store.GetIterFirst(out iter);
							path = store.GetPath(iter);
						}
						tv_list.SetCursor(path, col, false);
								
					}
					if(tv_list.Selection.GetSelected(out iter)) {
						controller.MediaPlayer_LoadFile(String.Format("{0}", store.GetValue(iter, 0)));
						controller.MediaPlayer_Play();
					}
				}
				public void PrevSong() {
					TreeIter iter;
					TreePath path;
					TreeViewColumn col;
					if(tv_list.Selection.GetSelected(out iter)) {
					
						tv_list.GetCursor(out path, out col);
						
						path.Prev();
						if(path.ToString() == "-1") {
							//store.GetIterFirst(out iter);
							path.Next();
						}
						tv_list.SetCursor(path, col, false);
								
					}
					if(tv_list.Selection.GetSelected(out iter)) {
						controller.MediaPlayer_LoadFile(String.Format("{0}", store.GetValue(iter, 0)));
						controller.MediaPlayer_Play();
					}
				}
			}
		}
	}
}