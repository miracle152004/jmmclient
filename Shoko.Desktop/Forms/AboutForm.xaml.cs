﻿using System.Windows;

namespace Shoko.Desktop.Forms
{
    /// <summary>
    /// Interaction logic for AboutForm.xaml
    /// </summary>
    public partial class AboutForm : Window
    {
        public AboutForm()
        {
            InitializeComponent();

            btnUpdates.Click += new RoutedEventHandler(btnUpdates_Click);
            cbUpdateChannel.Items.Add("Stable");
            cbUpdateChannel.Items.Add("Beta");
            cbUpdateChannel.Items.Add("Alpha");
            cbUpdateChannel.Text = AppSettings.UpdateChannel;
        }

        void btnUpdates_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mdw = Owner as MainWindow;
            if (mdw == null) return;

            Close();
            mdw.CheckForUpdates(true);
        }

        private void cbUpdateChannel_DropDownClosed(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbUpdateChannel.Text))
                AppSettings.UpdateChannel = cbUpdateChannel.Text;
        }

        private void cbUpdateChannel_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(cbUpdateChannel.Text))
                AppSettings.UpdateChannel = cbUpdateChannel.Text;
        }
    }
}
