using System;
using System.Windows.Controls;
using Atemoya.Classes.Models.Application;

namespace Atemoya.Controls.Tabs {

    public partial class GamertagContainer : UserControl {

        public GamertagContainer() {

            InitializeComponent();

            DataContext = new GamertagModel();

        }

        protected override void OnInitialized(EventArgs e) {
            base.OnInitialized(e);
            try {

            } catch (Exception ex) {
                throw ex;
            }
        }

    }

}