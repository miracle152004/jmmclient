﻿using System.ComponentModel;
using System.IO;
using Shoko.Commons.Extensions;
using Shoko.Commons.Notification;
using Shoko.Commons.Utils;
using Shoko.Desktop.ViewModel.Helpers;
using Shoko.Desktop.ViewModel.Server;
// ReSharper disable InconsistentNaming

namespace Shoko.Desktop.ViewModel
{
    public class VM_AVDump : INotifyPropertyChangedExt
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }

        public int ObjectType => 5;

        private string fullPath = "";
        public string FullPath
        {
            get { return fullPath; }
            set
            {
                this.SetField(()=>fullPath,value);
            }
        }

        private long fileSize;
        public long FileSize
        {
            get { return fileSize; }
            set
            {
                this.SetField(()=>fileSize,value);
            }
        }

        private string eD2KDump = "";
        public string ED2KDump
        {
            get { return eD2KDump; }
            set
            {
                this.SetField(()=>eD2KDump,value);
            }
        }

        private string aVDumpFullResult = "";
        public string AVDumpFullResult
        {
            get { return aVDumpFullResult; }
            set
            {
                this.SetField(()=>aVDumpFullResult,value);
            }
        }

        private bool hasBeenDumped;
        public bool HasBeenDumped
        {
            get { return hasBeenDumped; }
            set
            {
                this.SetField(()=>hasBeenDumped,value);
            }
        }

        private bool isBeingDumped;
        public bool IsBeingDumped
        {
            get { return isBeingDumped; }
            set
            {
                this.SetField(()=>isBeingDumped,value);
            }
        }

        private string dumpStatus = "";
        public string DumpStatus
        {
            get { return dumpStatus; }
            set
            {
                this.SetField(()=>dumpStatus,value);
            }
        }

        private VM_VideoLocal videoLocal;
        public VM_VideoLocal VideoLocal
        {
            get { return videoLocal; }
            set
            {
                this.SetField(()=>videoLocal,value);
            }
        }


        public string FileName => Path.GetFileName(FullPath);

        public bool FileIsAvailable => File.Exists(FullPath);

        public bool FileIsNotAvailable => !File.Exists(FullPath);

        public string FormattedFileSize => Formatting.FormatFileSize(FileSize);

        public VM_AVDump()
        {
        }

        public VM_AVDump(VM_VideoLocal vid)
        {
            FullPath = vid.GetLocalFileSystemFullPath();
            FileSize = vid.FileSize;
            ED2KDump = "";
            AVDumpFullResult = "";
            HasBeenDumped = false;
            IsBeingDumped = false;
            DumpStatus = "To be processed";
            VideoLocal = vid;
        }
    }
}
