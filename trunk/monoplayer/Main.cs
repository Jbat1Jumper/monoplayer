// Proyect Started on 01-11-2007
// Monoplayer - Media player for the masses
// -------------------------
// Created by: Andrés Villagrán Placencia
// E-Mail: andres@villagranquiroz.cl
// Blog: http://andres.villagranquiroz.cl
// -------------------------
// Villagrán & Quiroz - Servicios Informáticos
// http://www.villagranquiroz.cl
using System;
using Gtk;
using Monoplayer.Player;

namespace Monoplayer
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			Controller controller = new Controller();
			windows.MainWindow win = new windows.MainWindow (controller);
			win.Show ();
			Application.Run();
		}
	}
}