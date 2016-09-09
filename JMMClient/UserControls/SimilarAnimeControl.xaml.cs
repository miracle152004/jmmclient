﻿using JMMClient.ViewModel;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;



namespace JMMClient.UserControls
{
    /// <summary>
    /// Interaction logic for SimilarAnimeControl.xaml
    /// </summary>
    public partial class SimilarAnimeControl : UserControl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private BackgroundWorker missingDataWorker = new BackgroundWorker();

        public static readonly DependencyProperty DataExistsProperty = DependencyProperty.Register("DataExists",
            typeof(bool), typeof(SimilarAnimeControl), new UIPropertyMetadata(false, null));

        public bool DataExists
        {
            get { return (bool)GetValue(DataExistsProperty); }
            set { SetValue(DataExistsProperty, value); }
        }

        public static readonly DependencyProperty DataMissingProperty = DependencyProperty.Register("DataMissing",
            typeof(bool), typeof(SimilarAnimeControl), new UIPropertyMetadata(false, null));

        public bool DataMissing
        {
            get { return (bool)GetValue(DataMissingProperty); }
            set { SetValue(DataMissingProperty, value); }
        }

        public string SeriesName
        {
            get
            {
                if (this.DataContext == null) { return ""; }
                AnimeSeriesVM series = (AnimeSeriesVM)this.DataContext;
                return series.SeriesName + " " + Properties.Resources.NoSimilarAnime;
            }
        }

        public ObservableCollection<AniDB_Anime_SimilarVM> SimilarAnimeLinks { get; set; }

        public SimilarAnimeControl()
        {
            InitializeComponent();

            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(AppSettings.Culture);

            SimilarAnimeLinks = new ObservableCollection<AniDB_Anime_SimilarVM>();

            this.DataContextChanged += new DependencyPropertyChangedEventHandler(SimilarAnimeControl_DataContextChanged);

            btnGetSimMissingInfo.Click += new RoutedEventHandler(btnGetSimMissingInfo_Click);

            missingDataWorker.DoWork += new DoWorkEventHandler(missingDataWorker_DoWork);
            missingDataWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(missingDataWorker_RunWorkerCompleted);
        }

        void missingDataWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Window wdw = Window.GetWindow(this);
            wdw.Cursor = Cursors.Arrow;
            wdw.IsEnabled = true;
        }

        void missingDataWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                foreach (AniDB_Anime_SimilarVM sim in SimilarAnimeLinks)
                {
                    if (sim.AnimeInfoNotExists)
                    {
                        string result = JMMServerVM.Instance.clientBinaryHTTP.UpdateAnimeData(sim.SimilarAnimeID);
                        if (string.IsNullOrEmpty(result))
                        {
                            JMMServerBinary.Contract_AniDBAnime animeContract = JMMServerVM.Instance.clientBinaryHTTP.GetAnime(sim.SimilarAnimeID);
                            System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Send, (Action)delegate ()
                            {
                                sim.PopulateAnime(animeContract);
                            });
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Utils.ShowErrorMessage(ex);
            }
        }

        void SimilarAnimeControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
        }

        public void RefreshData()
        {
            try
            {
                AniDB_AnimeVM anime = null;

                AnimeSeriesVM animeSeries = (AnimeSeriesVM)this.DataContext;
                if (animeSeries == null)
                {
                    SimilarAnimeLinks.Clear();
                    return;
                }
                SimilarAnimeLinks.Clear();
                anime = animeSeries.AniDB_Anime;

                if (anime == null) return;


                List<JMMServerBinary.Contract_AniDB_Anime_Similar> links = JMMServerVM.Instance.clientBinaryHTTP.GetSimilarAnimeLinks(anime.AnimeID,
                    JMMServerVM.Instance.CurrentUser.JMMUserID.Value);


                List<AniDB_Anime_SimilarVM> tempList = new List<AniDB_Anime_SimilarVM>();
                foreach (JMMServerBinary.Contract_AniDB_Anime_Similar link in links)
                {
                    AniDB_Anime_SimilarVM sim = new AniDB_Anime_SimilarVM();
                    sim.Populate(link);
                    tempList.Add(sim);
                }

                List<SortPropOrFieldAndDirection> sortCriteria = new List<SortPropOrFieldAndDirection>();
                sortCriteria.Add(new SortPropOrFieldAndDirection("ApprovalPercentage", true, SortType.eDoubleOrFloat));
                tempList = Sorting.MultiSort<AniDB_Anime_SimilarVM>(tempList, sortCriteria);

                foreach (AniDB_Anime_SimilarVM sim in tempList)
                    SimilarAnimeLinks.Add(sim);

                if (SimilarAnimeLinks.Count == 0)
                {
                    DataExists = false;
                    DataMissing = true;
                }
                else
                {
                    DataExists = true;
                    DataMissing = false;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowErrorMessage(ex);
            }
        }

        public void GetMissingSimilarData()
        {
            Window wdw = Window.GetWindow(this);
            wdw.Cursor = Cursors.Wait;
            wdw.IsEnabled = false;

            missingDataWorker.RunWorkerAsync();
        }

        void btnGetSimMissingInfo_Click(object sender, RoutedEventArgs e)
        {
            GetMissingSimilarData();
        }
    }
}
