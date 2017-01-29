﻿using System;
using System.ComponentModel;
using System.IO;
using Shoko.Commons.Notification;
using Shoko.Models.Enums;
using Shoko.Desktop.ImageDownload;
using Shoko.Desktop.Utilities;
using Shoko.Desktop.ViewModel.Helpers;
using Shoko.Models.Server;

// ReSharper disable InconsistentNaming

namespace Shoko.Desktop.ViewModel.Server
{
    public class VM_Trakt_ImagePoster : Trakt_ImagePoster, INotifyPropertyChanged, INotifyPropertyChangedExt
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }

        public string FullImagePathPlain
        {
            get
            {
                // typical url
                // http://vicmackey.trakt.tv/images/seasons/3228-1.jpg
                // http://vicmackey.trakt.tv/images/posters/1130.jpg

                if (string.IsNullOrEmpty(ImageURL)) return "";

                int pos = ImageURL.IndexOf(@"images/", StringComparison.Ordinal);
                if (pos <= 0) return "";

                string relativePath = ImageURL.Substring(pos + 7, ImageURL.Length - pos - 7);
                relativePath = relativePath.Replace("/", @"\");

                string filename = Path.Combine(Utils.GetTraktImagePath(), relativePath);

                return filename;
            }
        }

        public string FullImagePath
        {
            get
            {
                if (!File.Exists(FullImagePathPlain))
                {
                    ImageDownloadRequest req = new ImageDownloadRequest(ImageEntityType.Trakt_Poster, this, false);
                    MainWindow.imageHelper.DownloadImage(req);
                    if (File.Exists(FullImagePathPlain)) return FullImagePathPlain;
                }

                return FullImagePathPlain;
            }
        }

        public string FullThumbnailPath => FullImagePath;

        public bool IsImageEnabled
        {
            get { return base.Enabled == 1; }
            set { base.Enabled = value ? 1 : 0; }
        }
        public new int Enabled
        {
            get { return base.Enabled; }
            set { base.Enabled = this.SetField(base.Enabled, value, () => Enabled, () => IsImageEnabled); }
        }
        private bool isImageDefault;
        public bool IsImageDefault
        {
            get { return isImageDefault; }
            set { isImageDefault = this.SetField(isImageDefault, value); }
        }

    }
}
