﻿using System;
using System.ComponentModel;

namespace JMMClient.ViewModel
{
    public class CrossRef_AniDB_TraktVMV2 : INotifyPropertyChanged
    {
        public int CrossRef_AniDB_TraktV2ID { get; set; }
        public int AnimeID { get; set; }
        public int AniDBStartEpisodeType { get; set; }
        public int AniDBStartEpisodeNumber { get; set; }
        public string TraktID { get; set; }
        public int TraktSeasonNumber { get; set; }
        public int TraktStartEpisodeNumber { get; set; }
        public string TraktTitle { get; set; }
        public int CrossRefSource { get; set; }


        public string Username { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                var args = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, args);
            }
        }

        private string showURL = string.Empty;
        public string ShowURL
        {
            get { return showURL; }
            set
            {
                showURL = value;
                NotifyPropertyChanged("ShowURL");
            }
        }

        private string anidbURL = string.Empty;
        public string AniDBURL
        {
            get { return anidbURL; }
            set
            {
                anidbURL = value;
                NotifyPropertyChanged("AniDBURL");
            }
        }

        public string AniDBStartEpisodeTypeString
        {
            get
            {
                return EnumTranslator.EpisodeTypeTranslated((EpisodeType)AniDBStartEpisodeType);
            }
        }

        public string AniDBStartEpisodeNumberString
        {
            get
            {
                return string.Format("# {0}", AniDBStartEpisodeNumber);
            }
        }

        public string TraktSeasonNumberString
        {
            get
            {
                return string.Format("S{0}", TraktSeasonNumber);
            }
        }

        public string TraktStartEpisodeNumberString
        {
            get
            {
                return string.Format("EP# {0}", TraktStartEpisodeNumber);
            }
        }

        private string isAdminApprovedImage = @"/Images/placeholder.png";
        public string IsAdminApprovedImage
        {
            get { return isAdminApprovedImage; }
            set
            {
                isAdminApprovedImage = value;
                NotifyPropertyChanged("IsAdminApprovedImage");
            }
        }

        private void SetAdminApprovedImage()
        {
            if (IsAdminApproved == 1)
                IsAdminApprovedImage = @"/Images/16_tick.png";
            else
                IsAdminApprovedImage = @"/Images/placeholder.png";
        }

        private int isAdminApproved = 0;
        public int IsAdminApproved
        {
            get { return isAdminApproved; }
            set
            {
                isAdminApproved = value;
                NotifyPropertyChanged("IsAdminApproved");

                IsAdminApprovedBool = isAdminApproved == 1;
                IsNotAdminApprovedBool = !IsAdminApprovedBool;

                SetAdminApprovedImage();
            }
        }

        private bool isAdminApprovedBool = false;
        public bool IsAdminApprovedBool
        {
            get { return isAdminApprovedBool; }
            set
            {
                isAdminApprovedBool = value;
                NotifyPropertyChanged("IsAdminApprovedBool");
            }
        }

        private bool isNotAdminApprovedBool = true;
        public bool IsNotAdminApprovedBool
        {
            get { return isNotAdminApprovedBool; }
            set
            {
                isNotAdminApprovedBool = value;
                NotifyPropertyChanged("IsNotAdminApprovedBool");
            }
        }

        public CrossRef_AniDB_TraktVMV2()
        {
        }

        public CrossRef_AniDB_TraktVMV2(JMMServerBinary.Contract_CrossRef_AniDB_TraktV2 contract)
        {
            this.AnimeID = contract.AnimeID;
            this.AniDBStartEpisodeType = contract.AniDBStartEpisodeType;
            this.AniDBStartEpisodeNumber = contract.AniDBStartEpisodeNumber;
            this.TraktID = contract.TraktID;
            this.CrossRef_AniDB_TraktV2ID = contract.CrossRef_AniDB_TraktV2ID;
            this.TraktSeasonNumber = contract.TraktSeasonNumber;
            this.TraktStartEpisodeNumber = contract.TraktStartEpisodeNumber;
            this.CrossRefSource = contract.CrossRefSource;
            this.TraktTitle = contract.TraktTitle;
            this.IsAdminApproved = 0;

            ShowURL = string.Format(Constants.URLS.Trakt_Series, TraktID);
            AniDBURL = string.Format(Constants.URLS.AniDB_Series, AnimeID);
        }

        public CrossRef_AniDB_TraktVMV2(JMMServerBinary.Contract_Azure_CrossRef_AniDB_Trakt contract)
        {
            this.CrossRef_AniDB_TraktV2ID = contract.CrossRef_AniDB_TraktId.Value;
            this.AnimeID = contract.AnimeID;
            this.AniDBStartEpisodeType = contract.AniDBStartEpisodeType;
            this.AniDBStartEpisodeNumber = contract.AniDBStartEpisodeNumber;
            this.TraktID = contract.TraktID;
            this.TraktSeasonNumber = contract.TraktSeasonNumber;
            this.TraktStartEpisodeNumber = contract.TraktStartEpisodeNumber;
            this.CrossRefSource = contract.CrossRefSource;
            this.TraktTitle = contract.TraktTitle;
            this.IsAdminApproved = contract.IsAdminApproved;
            this.Username = contract.Username;
            this.IsAdminApproved = contract.IsAdminApproved;

            ShowURL = string.Format(Constants.URLS.Trakt_Series, TraktID);
            AniDBURL = string.Format(Constants.URLS.AniDB_Series, AnimeID);
        }

        public override string ToString()
        {
            return string.Format("{0} = {1}   AniDB # {2}:{3}   TvDB {4}:{5}", AnimeID, TraktID, AniDBStartEpisodeType, AniDBStartEpisodeNumber, TraktSeasonNumber, TraktStartEpisodeNumber);
        }
    }
}
