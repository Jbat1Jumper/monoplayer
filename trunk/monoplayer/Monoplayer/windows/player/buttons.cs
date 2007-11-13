// buttons.cs
// Andrés Villagrán Placencia - andres@villagranquiroz.cl
// [ Villagrán Quiroz ] Servicios informáticos
// http://www.villagranquiroz.cl
//

using System;
using Gtk;
using Monoplayer.Player;

namespace Monoplayer
{
	namespace windows {

		namespace player {
			
			public partial class buttons : Gtk.Bin
			{
				private Controller controller;
				private bool playing;
				
				public buttons(Controller ctrl)
				{
					this.controller = ctrl;
					this.Build();
					playing = false; 
				}

				protected virtual void OnBtnPlayClicked (object sender, System.EventArgs e) {
					if(controller.MediaPlayer_Play())
						playing = true;
					else
						playing = false;
				}

				protected virtual void OnBtnPrevClicked (object sender, System.EventArgs e) {
					controller.View_PrevSong();
				}

				protected virtual void OnBtnNextClicked (object sender, System.EventArgs e) {
					controller.View_NextSong();
				}
				public void SetMediaInfo(string song, string length, string album, string author, string track, string CoverFilename) {
					txt_song.Markup = "<big><b>"+song+"</b> "+length+"</big>";
					txt_author.Markup = "por <b>"+author+"</b>";
					txt_album.Markup = "<big><b>"+album+"</b> "+track+"</big>";
					if(System.IO.File.Exists(controller.configManager.ConfigPath+CoverFilename)) {
						//img.Save(controller.configManager.ConfigPath+"cover.png", ImageFormat.Png);
						cover_image.File = controller.configManager.ConfigPath+CoverFilename;
					} else {
						cover_image.Pixbuf =  Gdk.Pixbuf.LoadFromResource("no-cover.png");
					}
					
				}

				protected virtual void OnEventboxButtonPressEvent (object o, Gtk.ButtonPressEventArgs args)	{
					if(args.Event.Button == 3) {
						FileChooserDialog fc =
						new FileChooserDialog("Select Image", controller.mainwindow,
						                  FileChooserAction.Open,
						                  "Cancel", ResponseType.Cancel, 
						                  "Accept", ResponseType.Accept);
					if(fc.Run() == (int)ResponseType.Accept) {
						// fc.Filename;
						//controller.configManager.DefaultPath = fc.Filename;
					}
					fc.Destroy();
					}
				}


			}
		}
	}
}