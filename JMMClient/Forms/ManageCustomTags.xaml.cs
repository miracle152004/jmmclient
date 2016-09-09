﻿using JMMClient.ViewModel;
using NLog;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace JMMClient.Forms
{
    /// <summary>
    /// Interaction logic for ManageCustomTags.xaml
    /// </summary>
    public partial class ManageCustomTags : Window
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ManageCustomTags()
        {
            InitializeComponent();

            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(AppSettings.Culture);

            btnAddCustomTag.Click += btnAddCustomTag_Click;
            btnClose.Click += new RoutedEventHandler(btnClose_Click);
        }

        void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        void btnAddCustomTag_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Wait;
                string res = "";

                if (string.IsNullOrWhiteSpace(txtTagName.Text))
                {
                    MessageBox.Show(Properties.Resources.CustomTag_EnterName, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    txtTagName.Focus();
                    return;
                }

                JMMServerBinary.Contract_CustomTag contract = new JMMServerBinary.Contract_CustomTag();
                contract.TagName = txtTagName.Text.Trim();
                contract.TagDescription = txtTagDescription.Text.Trim();


                JMMServerBinary.Contract_CustomTag_SaveResponse resp = JMMServerVM.Instance.clientBinaryHTTP.SaveCustomTag(contract);

                if (!string.IsNullOrEmpty(resp.ErrorMessage))
                {
                    MessageBox.Show(res, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    CustomTagVM ctag = new CustomTagVM(resp.CustomTag);
                    JMMServerVM.Instance.AllCustomTags.Add(ctag);
                    JMMServerVM.Instance.ViewCustomTagsAll.Refresh();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowErrorMessage(ex);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        private void CommandBinding_DeleteCustomTag(object sender, ExecutedRoutedEventArgs e)
        {
            object obj = e.Parameter;
            if (obj == null) return;

            try
            {
                this.Cursor = Cursors.Wait;
                string res = "";


                // NOTE if we are disabling an image we should also make sure it is not the default
                CustomTagVM tag = null;
                if (obj.GetType() == typeof(CustomTagVM))
                {
                    tag = (CustomTagVM)obj;
                    res = JMMServerVM.Instance.clientBinaryHTTP.DeleteCustomTag(tag.CustomTagID);
                }

                if (res.Length > 0)
                {
                    MessageBox.Show(res, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    CustomTagVM ctagToRemove = null;
                    foreach (CustomTagVM ctag in JMMServerVM.Instance.AllCustomTags)
                    {
                        if (ctag.CustomTagID == tag.CustomTagID)
                        {
                            ctagToRemove = ctag;
                            break;
                        }

                    }

                    if (ctagToRemove != null)
                    {
                        JMMServerVM.Instance.AllCustomTags.Remove(ctagToRemove);
                        JMMServerVM.Instance.ViewCustomTagsAll.Refresh();

                        //TODO: Custom Tags -  update any cached data for affected anime
                    }

                }
            }
            catch (Exception ex)
            {
                Utils.ShowErrorMessage(ex);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }
    }
}
