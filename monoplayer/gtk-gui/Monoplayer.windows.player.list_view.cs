// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.42
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Monoplayer.windows.player {
    
    
    public partial class list_view {
        
        private Gtk.ScrolledWindow scrolledwindow1;
        
        private Gtk.TreeView tv_list;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget Monoplayer.windows.player.list_view
            Stetic.BinContainer.Attach(this);
            this.WidthRequest = 400;
            this.HeightRequest = 400;
            this.Name = "Monoplayer.windows.player.list_view";
            // Container child Monoplayer.windows.player.list_view.Gtk.Container+ContainerChild
            this.scrolledwindow1 = new Gtk.ScrolledWindow();
            this.scrolledwindow1.CanFocus = true;
            this.scrolledwindow1.Name = "scrolledwindow1";
            this.scrolledwindow1.VscrollbarPolicy = ((Gtk.PolicyType)(1));
            this.scrolledwindow1.HscrollbarPolicy = ((Gtk.PolicyType)(1));
            this.scrolledwindow1.ShadowType = ((Gtk.ShadowType)(1));
            // Container child scrolledwindow1.Gtk.Container+ContainerChild
            this.tv_list = new Gtk.TreeView();
            this.tv_list.CanFocus = true;
            this.tv_list.Name = "tv_list";
            this.tv_list.RulesHint = true;
            this.tv_list.SearchColumn = 1;
            this.scrolledwindow1.Add(this.tv_list);
            this.Add(this.scrolledwindow1);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.Show();
            this.tv_list.RowActivated += new Gtk.RowActivatedHandler(this.OnTvListRowActivated);
        }
    }
}
