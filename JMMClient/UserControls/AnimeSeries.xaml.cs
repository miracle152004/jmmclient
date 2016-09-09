﻿using JMMClient.Forms;
using JMMClient.ViewModel;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace JMMClient.UserControls
{
    /// <summary>
    /// Interaction logic for AnimeSeries.xaml
    /// </summary>
    public partial class AnimeSeries : UserControl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ContextMenu playlistMenu;

        public static readonly DependencyProperty AllPostersProperty = DependencyProperty.Register("AllPosters",
            typeof(List<PosterContainer>), typeof(AnimeSeries), new UIPropertyMetadata(null, null));

        public List<PosterContainer> AllPosters
        {
            get { return (List<PosterContainer>)GetValue(AllPostersProperty); }
            set { SetValue(AllPostersProperty, value); }
        }

        public static readonly DependencyProperty AllFanartsProperty = DependencyProperty.Register("AllFanarts",
            typeof(List<FanartContainer>), typeof(AnimeSeries), new UIPropertyMetadata(null, null));

        public List<FanartContainer> AllFanarts
        {
            get { return (List<FanartContainer>)GetValue(AllFanartsProperty); }
            set { SetValue(AllFanartsProperty, value); }
        }

        public static readonly DependencyProperty SeriesTvDBWideBannersProperty = DependencyProperty.Register("SeriesTvDBWideBanners",
            typeof(List<TvDB_ImageWideBannerVM>), typeof(AnimeSeries), new UIPropertyMetadata(null, null));

        public List<TvDB_ImageWideBannerVM> SeriesTvDBWideBanners
        {
            get { return (List<TvDB_ImageWideBannerVM>)GetValue(SeriesTvDBWideBannersProperty); }
            set { SetValue(SeriesTvDBWideBannersProperty, value); }
        }

        public static readonly DependencyProperty ImageListBoxWidthProperty = DependencyProperty.Register("ImageListBoxWidth",
            typeof(double), typeof(AnimeSeries), new UIPropertyMetadata((double)300, null));

        public double ImageListBoxWidth
        {
            get { return (double)GetValue(ImageListBoxWidthProperty); }
            set { SetValue(ImageListBoxWidthProperty, value); }
        }

        public static readonly DependencyProperty ImageListBoxWidthFullProperty = DependencyProperty.Register("ImageListBoxWidthFull",
            typeof(double), typeof(AnimeSeries), new UIPropertyMetadata((double)300, null));

        public double ImageListBoxWidthFull
        {
            get { return (double)GetValue(ImageListBoxWidthFullProperty); }
            set { SetValue(ImageListBoxWidthFullProperty, value); }
        }

        public static readonly DependencyProperty SeriesPos_TvDBLinksProperty = DependencyProperty.Register("SeriesPos_TvDBLinks",
            typeof(int), typeof(AnimeSeries), new UIPropertyMetadata((int)6, null));

        public int SeriesPos_TvDBLinks
        {
            get { return (int)GetValue(SeriesPos_TvDBLinksProperty); }
            set { SetValue(SeriesPos_TvDBLinksProperty, value); }
        }

        public static readonly DependencyProperty SeriesPos_PlayNextEpisodeProperty = DependencyProperty.Register("SeriesPos_PlayNextEpisode",
            typeof(int), typeof(AnimeSeries), new UIPropertyMetadata((int)6, null));

        public int SeriesPos_PlayNextEpisode
        {
            get { return (int)GetValue(SeriesPos_PlayNextEpisodeProperty); }
            set { SetValue(SeriesPos_PlayNextEpisodeProperty, value); }
        }

        public static readonly DependencyProperty SeriesPos_TitlesProperty = DependencyProperty.Register("SeriesPos_Titles",
            typeof(int), typeof(AnimeSeries), new UIPropertyMetadata((int)6, null));

        public int SeriesPos_Titles
        {
            get { return (int)GetValue(SeriesPos_TitlesProperty); }
            set { SetValue(SeriesPos_TitlesProperty, value); }
        }

        public static readonly DependencyProperty SeriesPos_TagsProperty = DependencyProperty.Register("SeriesPos_Tags",
            typeof(int), typeof(AnimeSeries), new UIPropertyMetadata((int)6, null));

        public int SeriesPos_Tags
        {
            get { return (int)GetValue(SeriesPos_TagsProperty); }
            set { SetValue(SeriesPos_TagsProperty, value); }
        }

        public static readonly DependencyProperty SeriesPos_CustomTagsProperty = DependencyProperty.Register("SeriesPos_CustomTags",
            typeof(int), typeof(AnimeSeries), new UIPropertyMetadata((int)7, null));

        public int SeriesPos_CustomTags
        {
            get { return (int)GetValue(SeriesPos_CustomTagsProperty); }
            set { SetValue(SeriesPos_CustomTagsProperty, value); }
        }

        public static readonly DependencyProperty TruncatedDescriptionProperty = DependencyProperty.Register("TruncatedDescription",
            typeof(bool), typeof(AnimeSeries), new UIPropertyMetadata(true, null));

        public bool TruncatedDescription
        {
            get { return (bool)GetValue(TruncatedDescriptionProperty); }
            set { SetValue(TruncatedDescriptionProperty, value); }
        }

        public static readonly DependencyProperty FullDescriptionProperty = DependencyProperty.Register("FullDescription",
            typeof(bool), typeof(AnimeSeries), new UIPropertyMetadata(false, null));

        public bool FullDescription
        {
            get { return (bool)GetValue(FullDescriptionProperty); }
            set { SetValue(FullDescriptionProperty, value); }
        }

        public static readonly DependencyProperty PreviousOverrideNameProperty = DependencyProperty.Register("PreviousOverrideName",
            typeof(string), typeof(AnimeSeries), new UIPropertyMetadata("", null));

        public string PreviousOverrideName
        {
            get { return (string)GetValue(PreviousOverrideNameProperty); }
            set { SetValue(PreviousOverrideNameProperty, value); }
        }

        public AnimeSeries()
        {
            InitializeComponent();

            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(AppSettings.Culture);

            playlistMenu = new ContextMenu();

            cRating.OnRatingValueChangedEvent += new RatingControl.RatingValueChangedHandler(cRating_OnRatingValueChangedEvent);

            this.Loaded += new RoutedEventHandler(AnimeSeries_Loaded);
            btnAnimeGroupShow.Click += new RoutedEventHandler(btnAnimeGroupShow_Click);
            btnTvDBLinks.Click += new RoutedEventHandler(btnTvDBLinks_Click);
            //btnPlayNextEpisode.Click += new RoutedEventHandler(btnPlayNextEpisode_Click);
            
            btnSwitchView.Click += BtnSwitchView_Click;

            this.DataContextChanged += new DependencyPropertyChangedEventHandler(AnimeSeries_DataContextChanged);

            tabContainer.SelectionChanged += new SelectionChangedEventHandler(tabContainer_SelectionChanged);

            btnUpdateAniDBInfo.Click += new RoutedEventHandler(btnUpdateAniDBInfo_Click);
            this.LayoutUpdated += new EventHandler(AnimeSeries_LayoutUpdated);

            btnEditSeries.Click += new RoutedEventHandler(btnEditSeries_Click);
            btnEditSeriesFinish.Click += new RoutedEventHandler(btnEditSeriesFinish_Click);

            btnAnimeTitles.Click += new RoutedEventHandler(btnAnimeTitles_Click);
            btnAnimeTags.Click += new RoutedEventHandler(btnAnimeTags_Click);
            btnCustomTags.Click += new RoutedEventHandler(btnCustomTags_Click);
            btnAddCustomTag.Click += new RoutedEventHandler(btnAddCustomTag_Click);
            btnManageTags.Click += new RoutedEventHandler(btnManageTags_Click);

            btnPlaylistAdd.ContextMenu = playlistMenu;
            btnPlaylistAdd.Click += new RoutedEventHandler(btnPlaylistAdd_Click);

            btnRandomEpisode.Click += new RoutedEventHandler(btnRandomEpisode_Click);

            chkSerNameOverride.Click += new RoutedEventHandler(chkSerNameOverride_Click);
            btnSelectOverrideName.Click += new RoutedEventHandler(btnSelectOverrideName_Click);

            MainWindow.videoHandler.VideoWatchedEvent += new VideoPlayers.VideoHandler.VideoWatchedEventHandler(videoHandler_VideoWatchedEvent);

            SetSeriesWidgetOrder();

            this.PreviewMouseWheel += AnimeSeries_PreviewMouseWheel;
        }

        private void BtnSwitchView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AppSettings.DisplaySeriesSimple = true;
                // check if this control is part of the series container
                DependencyObject parentObject = VisualTreeHelper.GetParent(this);
                while (parentObject != null)
                {
                    parentObject = VisualTreeHelper.GetParent(parentObject);
                    AnimeSeriesContainerControl containerCtrl = parentObject as AnimeSeriesContainerControl;
                    if (containerCtrl != null)
                    {
                        // show the simple view
                        AnimeSeriesVM ser = this.DataContext as AnimeSeriesVM;
                        if (ser == null) return;

                        AnimeSeriesSimplifiedControl seriesControl = new AnimeSeriesSimplifiedControl();
                        seriesControl.DataContext = ser;

                        containerCtrl.DataContext = seriesControl;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowErrorMessage(ex);
            }
        }

        private void AnimeSeries_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {
                foreach (ScrollViewer sv in Utils.GetScrollViewers(this))
                    sv.ScrollToVerticalOffset(sv.VerticalOffset - e.Delta / 3);
            }
            catch { }
        }

        void videoHandler_VideoWatchedEvent(VideoPlayers.VideoWatchedEventArgs ev)
        {
            if (MainWindow.CurrentMainTabIndex == MainWindow.TAB_MAIN_Collection || MainWindow.CurrentMainTabIndex == MainWindow.TAB_MAIN_Pinned)
                ShowNextEpisode();
        }

        void btnSelectOverrideName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window wdw = Window.GetWindow(this);

                wdw.Cursor = Cursors.Wait;

                AnimeSeriesVM ser = this.DataContext as AnimeSeriesVM;
                if (ser == null) return;

                SelectAniDBTitleForm frm = new SelectAniDBTitleForm();
                frm.Owner = wdw;
                frm.Init(ser.AniDB_Anime.Detail.AnimeTitles);
                wdw.Cursor = Cursors.Arrow;
                bool? result = frm.ShowDialog();
                if (result.Value)
                {
                    // update info
                    txtSeriesName.Text = frm.SelectedTitle;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowErrorMessage(ex);
            }
        }


        void chkSerNameOverride_Click(object sender, RoutedEventArgs e)
        {
            AnimeSeriesVM ser = this.DataContext as AnimeSeriesVM;
            if (ser == null) return;

            ser.IsSeriesNameOverridden = chkSerNameOverride.IsChecked.Value;
            ser.IsSeriesNameNotOverridden = !chkSerNameOverride.IsChecked.Value;

            EvaluateEditing();
        }

        private void EvaluateEditing()
        {
            AnimeSeriesVM ser = this.DataContext as AnimeSeriesVM;
            if (ser == null) return;

            if (ser.IsSeriesNameOverridden)
            {
                txtSeriesName.Text = ser.SeriesNameOverride;
                txtSeriesName.IsEnabled = true;
                chkSerNameOverride.IsChecked = true;
                btnSelectOverrideName.IsEnabled = true;
            }
            else
            {
                txtSeriesName.Text = ser.SeriesName;
                txtSeriesName.IsEnabled = false;
                chkSerNameOverride.IsChecked = false;
                btnSelectOverrideName.IsEnabled = false;
            }

        }

        void btnRandomEpisode_Click(object sender, RoutedEventArgs e)
        {
            AnimeSeriesVM ser = this.DataContext as AnimeSeriesVM;
            if (ser == null) return;

            MainWindow mainwdw = (MainWindow)Window.GetWindow(this);

            RandomEpisodeForm frm = new RandomEpisodeForm();
            frm.Owner = Window.GetWindow(this); ;
            frm.Init(RandomSeriesEpisodeLevel.Series, ser);
            bool? result = frm.ShowDialog();
        }

        void btnPlaylistAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<JMMServerBinary.Contract_Playlist> rawPlaylists = JMMServerVM.Instance.clientBinaryHTTP.GetAllPlaylists();
                PlaylistMenuCommand cmd = null;

                // get all playlists
                playlistMenu.Items.Clear();

                MenuItem itemSeries = new MenuItem();
                itemSeries.Header = Properties.Resources.Anime_AddSeries;
                itemSeries.Click += new RoutedEventHandler(playlistMenuItem_Click);
                playlistMenu.Items.Add(itemSeries);

                Separator sep = new Separator();

                MenuItem itemSeriesNew = new MenuItem();
                itemSeriesNew.Header = Properties.Resources.Anime_NewPlaylists;
                itemSeriesNew.Click += new RoutedEventHandler(playlistMenuItem_Click);
                cmd = new PlaylistMenuCommand(PlaylistItemType.Series, -1); // new playlist
                itemSeriesNew.CommandParameter = cmd;
                itemSeries.Items.Add(itemSeriesNew);
                itemSeries.Items.Add(sep);

                foreach (JMMServerBinary.Contract_Playlist contract in rawPlaylists)
                {
                    MenuItem itemSeriesPL = new MenuItem();
                    itemSeriesPL.Header = contract.PlaylistName;
                    itemSeriesPL.Click += new RoutedEventHandler(playlistMenuItem_Click);
                    cmd = new PlaylistMenuCommand(PlaylistItemType.Series, contract.PlaylistID.Value);
                    itemSeriesPL.CommandParameter = cmd;
                    itemSeries.Items.Add(itemSeriesPL);
                }

                MenuItem itemAllEpisodes = new MenuItem();
                itemAllEpisodes.Header = Properties.Resources.Anime_AddAllEpisodes;
                playlistMenu.Items.Add(itemAllEpisodes);

                Separator sep2 = new Separator();

                MenuItem itemAllEpisodesNew = new MenuItem();
                itemAllEpisodesNew.Header = Properties.Resources.Anime_NewPlaylists;
                itemAllEpisodesNew.Click += new RoutedEventHandler(playlistMenuItem_Click);
                cmd = new PlaylistMenuCommand(PlaylistItemType.AllEpisodes, -1); // new playlist
                itemAllEpisodesNew.CommandParameter = cmd;
                itemAllEpisodes.Items.Add(itemAllEpisodesNew);
                itemAllEpisodes.Items.Add(sep2);

                foreach (JMMServerBinary.Contract_Playlist contract in rawPlaylists)
                {
                    MenuItem itemSeriesPL = new MenuItem();
                    itemSeriesPL.Header = contract.PlaylistName;
                    itemSeriesPL.Click += new RoutedEventHandler(playlistMenuItem_Click);
                    cmd = new PlaylistMenuCommand(PlaylistItemType.AllEpisodes, contract.PlaylistID.Value);
                    itemSeriesPL.CommandParameter = cmd;
                    itemAllEpisodes.Items.Add(itemSeriesPL);
                }


                MenuItem itemUnwatchedEpisodes = new MenuItem();
                itemUnwatchedEpisodes.Header = Properties.Resources.Anime_AddUnwatched;
                playlistMenu.Items.Add(itemUnwatchedEpisodes);

                Separator sep3 = new Separator();

                MenuItem itemUnwatchedEpisodesNew = new MenuItem();
                itemUnwatchedEpisodesNew.Header = Properties.Resources.Anime_NewPlaylists;
                itemUnwatchedEpisodesNew.Click += new RoutedEventHandler(playlistMenuItem_Click);
                cmd = new PlaylistMenuCommand(PlaylistItemType.UnwatchedEpisodes, -1); // new playlist
                itemUnwatchedEpisodesNew.CommandParameter = cmd;
                itemUnwatchedEpisodes.Items.Add(itemUnwatchedEpisodesNew);
                itemUnwatchedEpisodes.Items.Add(sep3);

                foreach (JMMServerBinary.Contract_Playlist contract in rawPlaylists)
                {
                    MenuItem itemSeriesPL = new MenuItem();
                    itemSeriesPL.Header = contract.PlaylistName;
                    itemSeriesPL.Click += new RoutedEventHandler(playlistMenuItem_Click);
                    cmd = new PlaylistMenuCommand(PlaylistItemType.UnwatchedEpisodes, contract.PlaylistID.Value);
                    itemSeriesPL.CommandParameter = cmd;
                    itemUnwatchedEpisodes.Items.Add(itemSeriesPL);
                }



                playlistMenu.PlacementTarget = this;
                playlistMenu.IsOpen = true;
            }
            catch (Exception ex)
            {
                Utils.ShowErrorMessage(ex);
            }
        }

        void playlistMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MenuItem item = e.Source as MenuItem;
                MenuItem itemSender = sender as MenuItem;

                if (item == null || itemSender == null) return;
                if (!item.Header.ToString().Equals(itemSender.Header.ToString())) return;

                if (item != null && item.CommandParameter != null)
                {
                    PlaylistMenuCommand cmd = item.CommandParameter as PlaylistMenuCommand;
                    Debug.Write(Properties.Resources.Anime_PlaylistMenu + " " + cmd.ToString() + Environment.NewLine);

                    AnimeSeriesVM ser = this.DataContext as AnimeSeriesVM;
                    if (ser == null) return;

                    // get the playlist
                    PlaylistVM pl = null;
                    if (cmd.PlaylistID < 0)
                    {
                        pl = PlaylistHelperVM.CreatePlaylist(Window.GetWindow(this));
                        if (pl == null) return;
                    }
                    else
                    {
                        JMMServerBinary.Contract_Playlist plContract = JMMServerVM.Instance.clientBinaryHTTP.GetPlaylist(cmd.PlaylistID);
                        if (plContract == null)
                        {
                            MessageBox.Show(Properties.Resources.Anime_PlaylistMissing, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        pl = new PlaylistVM(plContract);
                    }

                    this.Cursor = Cursors.Wait;

                    // get the items to add to the playlist
                    if (cmd.AddType == PlaylistItemType.Series)
                    {
                        pl.AddSeries(ser.AnimeSeriesID.Value);
                    }
                    else
                    {
                        List<AnimeEpisodeVM> eps = ser.AllEpisodes;

                        List<SortPropOrFieldAndDirection> sortCriteria = new List<SortPropOrFieldAndDirection>();
                        sortCriteria.Add(new SortPropOrFieldAndDirection("EpisodeType", false, JMMClient.SortType.eInteger));
                        sortCriteria.Add(new SortPropOrFieldAndDirection("EpisodeNumber", false, JMMClient.SortType.eInteger));
                        sortCriteria.Add(new SortPropOrFieldAndDirection("AniDB_AirDateWithDefault", false, JMMClient.SortType.eDateTime));
                        eps = Sorting.MultiSort<AnimeEpisodeVM>(eps, sortCriteria);

                        if (cmd.AddType == PlaylistItemType.AllEpisodes)
                        {

                            foreach (AnimeEpisodeVM ep in eps)
                            {
                                if (ep.EpisodeTypeEnum == EpisodeType.Episode || ep.EpisodeTypeEnum == EpisodeType.Special)
                                    pl.AddEpisode(ep.AnimeEpisodeID);
                            }
                        }

                        if (cmd.AddType == PlaylistItemType.UnwatchedEpisodes)
                        {
                            foreach (AnimeEpisodeVM ep in eps)
                            {
                                if (!ep.Watched && (ep.EpisodeTypeEnum == EpisodeType.Episode || ep.EpisodeTypeEnum == EpisodeType.Special))
                                    pl.AddEpisode(ep.AnimeEpisodeID);
                            }
                        }
                    }

                    pl.Save();

                    PlaylistHelperVM.Instance.RefreshData();

                    this.Cursor = Cursors.Arrow;

                }
            }
            catch (Exception ex)
            {
                Utils.ShowErrorMessage(ex);
            }
        }


        private void Handle_Click(object sender, MouseButtonEventArgs e)
        {
            string tag = ((TextBlock)sender).Tag.ToString();

            if (tag.Equals("txtDescription", StringComparison.InvariantCultureIgnoreCase))
            {
                TruncatedDescription = !TruncatedDescription;
                FullDescription = !FullDescription;
            }
        }

        void btnEditSeriesFinish_Click(object sender, RoutedEventArgs e)
        {
            AnimeSeriesVM ser = this.DataContext as AnimeSeriesVM;
            if (ser == null) return;

            ser.IsBeingEdited = false;
            ser.IsReadOnly = true;

            if (ser.IsSeriesNameOverridden && string.IsNullOrEmpty(txtSeriesName.Text))
            {
                ser.IsSeriesNameOverridden = false;
                ser.IsSeriesNameNotOverridden = true;
            }

            string oldName = "";
            if (!string.IsNullOrEmpty(ser.SeriesNameOverride))
                oldName = ser.SeriesNameOverride;

            string newName = txtSeriesName.Text.Trim();
            if (!ser.IsSeriesNameOverridden)
                newName = "";

	        if (!oldName.Equals(newName))
	        {
		        // override name has changes so lets save to db
		        ser.SeriesNameOverride = newName;

		        ser.SetSeriesNames();

		        ser.Save();

		        // prompt to change parent group name
		        MessageBoxResult res = MessageBox.Show(Properties.Resources.Anime_RenameParent,
			        Properties.Resources.Confirm, MessageBoxButton.YesNo, MessageBoxImage.Question);
		        if (res == MessageBoxResult.Yes)
		        {
			        AnimeGroupVM thisGrp = MainListHelperVM.Instance.AllGroupsDictionary[ser.AnimeGroupID];
			        if (thisGrp != null)
			        {
				        thisGrp.GroupName = ser.SeriesName;
				        thisGrp.SortName = ser.SeriesName;
				        thisGrp.Save();
			        }
		        }
	        }
	        else
	        {
		        ser.SetSeriesNames();
		        ser.Save();
	        }

            //MainListHelperVM.Instance.RefreshGroupsSeriesData();

            EvaluateEditing();
        }

        void btnEditSeries_Click(object sender, RoutedEventArgs e)
        {
            AnimeSeriesVM ser = this.DataContext as AnimeSeriesVM;
            if (ser == null) return;

            if (string.IsNullOrEmpty(ser.SeriesNameOverride))
                PreviousOverrideName = "";
            else
                PreviousOverrideName = ser.SeriesNameOverride;

            ser.IsBeingEdited = true;
            ser.IsReadOnly = false;
        }

        private void SetSeriesWidgetOrder()
        {

            SeriesPos_PlayNextEpisode = UserSettingsVM.Instance.GetSeriesWidgetPosition(SeriesWidgets.PlayNextEpisode) + 4;
            SeriesPos_TvDBLinks = UserSettingsVM.Instance.GetSeriesWidgetPosition(SeriesWidgets.TvDBLinks) + 4;
            SeriesPos_Titles = UserSettingsVM.Instance.GetSeriesWidgetPosition(SeriesWidgets.Titles) + 4;
            SeriesPos_Tags = UserSettingsVM.Instance.GetSeriesWidgetPosition(SeriesWidgets.Tags) + 4;
            SeriesPos_CustomTags = UserSettingsVM.Instance.GetSeriesWidgetPosition(SeriesWidgets.CustomTags) + 4;
        }



        void AnimeSeries_LayoutUpdated(object sender, EventArgs e)
        {
        }

        private void CommandBinding_MoveUpWidget(object sender, ExecutedRoutedEventArgs e)
        {
            object obj = e.Parameter;
            if (obj == null) return;

            SeriesWidgets swid = (SeriesWidgets)int.Parse(obj.ToString());

            UserSettingsVM.Instance.MoveUpSeriesWidget(swid);
            SetSeriesWidgetOrder();
        }

        private void CommandBinding_MoveDownWidget(object sender, ExecutedRoutedEventArgs e)
        {
            object obj = e.Parameter;
            if (obj == null) return;

            SeriesWidgets swid = (SeriesWidgets)int.Parse(obj.ToString());

            UserSettingsVM.Instance.MoveDownSeriesWidget(swid);
            SetSeriesWidgetOrder();
        }

        private void CommandBinding_ViewImage(object sender, ExecutedRoutedEventArgs e)
        {
            object obj = e.Parameter;
            if (obj == null) return;

            try
            {
                if (obj.GetType() == typeof(PosterContainer))
                {
                    PosterContainer poster = (PosterContainer)obj;
                    Utils.OpenFile(poster.FullImagePath);
                }

                if (obj.GetType() == typeof(FanartContainer))
                {
                    FanartContainer fanart = (FanartContainer)obj;
                    Utils.OpenFile(fanart.FullImagePath);
                }

                /*
				if (obj.GetType() == typeof(TvDB_ImagePosterVM))
				{
					TvDB_ImagePosterVM poster = (TvDB_ImagePosterVM)obj;
					Utils.OpenFile(poster.FullImagePath);
				}

				if (obj.GetType() == typeof(TvDB_ImageFanartVM))
				{
					TvDB_ImageFanartVM fanart = (TvDB_ImageFanartVM)obj;
					Utils.OpenFile(fanart.FullImagePath);
				}*/

                if (obj.GetType() == typeof(TvDB_ImageWideBannerVM))
                {
                    TvDB_ImageWideBannerVM banner = (TvDB_ImageWideBannerVM)obj;
                    Utils.OpenFile(banner.FullImagePath);
                }
            }
            catch (Exception ex)
            {
                Utils.ShowErrorMessage(ex);
            }
        }

        private void CommandBinding_DisableImage(object sender, ExecutedRoutedEventArgs e)
        {
            object obj = e.Parameter;
            if (obj == null) return;

            EnableDisableImage(false, obj);
        }

        private void CommandBinding_EnableImage(object sender, ExecutedRoutedEventArgs e)
        {
            object obj = e.Parameter;
            if (obj == null) return;

            EnableDisableImage(true, obj);
        }

        private void EnableDisableImage(bool enabled, object img)
        {
            if (!JMMServerVM.Instance.ServerOnline) return;

            try
            {
                AnimeSeriesVM ser = this.DataContext as AnimeSeriesVM;
                if (ser == null) return;

                this.Cursor = Cursors.Wait;
                string res = "";


                // NOTE if we are disabling an image we should also make sure it is not the default

                if (img.GetType() == typeof(PosterContainer))
                {
                    PosterContainer poster = (PosterContainer)img;

                    if (!enabled && poster.IsImageDefault)
                        SetDefaultImage(false, img, false);

                    switch (poster.ImageType)
                    {
                        case ImageEntityType.TvDB_Cover:
                            TvDB_ImagePosterVM tvPoster = poster.PosterObject as TvDB_ImagePosterVM;
                            res = JMMServerVM.Instance.clientBinaryHTTP.EnableDisableImage(enabled, tvPoster.TvDB_ImagePosterID, (int)ImageEntityType.TvDB_Cover);
                            tvPoster.Enabled = enabled ? 1 : 0;
                            tvPoster.IsImageEnabled = enabled;
                            tvPoster.IsImageDisabled = !enabled;
                            break;

                        case ImageEntityType.Trakt_Poster:
                            Trakt_ImagePosterVM traktPoster = poster.PosterObject as Trakt_ImagePosterVM;
                            res = JMMServerVM.Instance.clientBinaryHTTP.EnableDisableImage(enabled, traktPoster.Trakt_ImagePosterID, (int)ImageEntityType.Trakt_Poster);
                            traktPoster.Enabled = enabled ? 1 : 0;
                            traktPoster.IsImageEnabled = enabled;
                            traktPoster.IsImageDisabled = !enabled;
                            break;

                        case ImageEntityType.AniDB_Cover:
                            AniDB_AnimeVM anime = poster.PosterObject as AniDB_AnimeVM;
                            res = JMMServerVM.Instance.clientBinaryHTTP.EnableDisableImage(enabled, anime.AnimeID, (int)ImageEntityType.AniDB_Cover);
                            anime.ImageEnabled = enabled ? 1 : 0;
                            anime.IsImageEnabled = enabled;
                            anime.IsImageDisabled = !enabled;
                            break;

                        case ImageEntityType.MovieDB_Poster:
                            MovieDB_PosterVM moviePoster = poster.PosterObject as MovieDB_PosterVM;
                            res = JMMServerVM.Instance.clientBinaryHTTP.EnableDisableImage(enabled, moviePoster.MovieDB_PosterID, (int)ImageEntityType.MovieDB_Poster);
                            moviePoster.Enabled = enabled ? 1 : 0;
                            moviePoster.IsImageEnabled = enabled;
                            moviePoster.IsImageDisabled = !enabled;
                            break;
                    }
                    poster.IsImageEnabled = enabled;
                    poster.IsImageDisabled = !enabled;
                }

                if (img.GetType() == typeof(FanartContainer))
                {
                    FanartContainer fanart = (FanartContainer)img;

                    if (!enabled && fanart.IsImageDefault)
                        SetDefaultImage(false, img, false);

                    switch (fanart.ImageType)
                    {
                        case ImageEntityType.TvDB_FanArt:
                            TvDB_ImageFanartVM tvFanart = fanart.FanartObject as TvDB_ImageFanartVM;
                            res = JMMServerVM.Instance.clientBinaryHTTP.EnableDisableImage(enabled, tvFanart.TvDB_ImageFanartID, (int)ImageEntityType.TvDB_FanArt);
                            tvFanart.Enabled = enabled ? 1 : 0;
                            tvFanart.IsImageEnabled = enabled;
                            tvFanart.IsImageDisabled = !enabled;
                            break;

                        case ImageEntityType.Trakt_Fanart:
                            Trakt_ImageFanartVM traktFanart = fanart.FanartObject as Trakt_ImageFanartVM;
                            res = JMMServerVM.Instance.clientBinaryHTTP.EnableDisableImage(enabled, traktFanart.Trakt_ImageFanartID, (int)ImageEntityType.Trakt_Fanart);
                            traktFanart.Enabled = enabled ? 1 : 0;
                            traktFanart.IsImageEnabled = enabled;
                            traktFanart.IsImageDisabled = !enabled;
                            break;

                        case ImageEntityType.MovieDB_FanArt:
                            MovieDB_FanartVM movieFanart = fanart.FanartObject as MovieDB_FanartVM;
                            res = JMMServerVM.Instance.clientBinaryHTTP.EnableDisableImage(enabled, movieFanart.MovieDB_FanartID, (int)ImageEntityType.MovieDB_FanArt);
                            movieFanart.Enabled = enabled ? 1 : 0;
                            movieFanart.IsImageEnabled = enabled;
                            movieFanart.IsImageDisabled = !enabled;
                            break;
                    }
                    fanart.IsImageEnabled = enabled;
                    fanart.IsImageDisabled = !enabled;
                }

                if (img.GetType() == typeof(TvDB_ImageWideBannerVM))
                {
                    TvDB_ImageWideBannerVM banner = (TvDB_ImageWideBannerVM)img;

                    if (!enabled && banner.IsImageDefault)
                        SetDefaultImage(false, img, false);

                    res = JMMServerVM.Instance.clientBinaryHTTP.EnableDisableImage(enabled, banner.TvDB_ImageWideBannerID, (int)ImageEntityType.TvDB_Banner);
                    banner.Enabled = enabled ? 1 : 0;
                    banner.IsImageEnabled = enabled;
                    banner.IsImageDisabled = !enabled;
                }

                if (res.Length > 0)
                {
                    MessageBox.Show(res, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MainListHelperVM.Instance.UpdateHeirarchy(ser);
                    MainListHelperVM.Instance.UpdateAnime(ser.AniDB_ID);
                    RefreshImagesData();
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

        private void CommandBinding_SetDefaultImageON(object sender, ExecutedRoutedEventArgs e)
        {
            object obj = e.Parameter;
            if (obj == null) return;

            SetDefaultImage(true, obj, true);
        }

        private void CommandBinding_SetDefaultImageOFF(object sender, ExecutedRoutedEventArgs e)
        {
            object obj = e.Parameter;
            if (obj == null) return;

            SetDefaultImage(false, obj, true);
        }

        private void SetDefaultImage(bool isDefault, object img, bool refreshData)
        {
            if (!JMMServerVM.Instance.ServerOnline) return;

            try
            {
                AnimeSeriesVM ser = this.DataContext as AnimeSeriesVM;
                if (ser == null) return;

                this.Cursor = Cursors.Wait;
                string res = "";

                string disabledMessage = Properties.Resources.Anime_DisabledImage;

                if (img.GetType() == typeof(PosterContainer))
                {
                    PosterContainer poster = (PosterContainer)img;

                    if (isDefault && poster.IsImageDisabled)
                    {
                        MessageBox.Show(disabledMessage, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    switch (poster.ImageType)
                    {
                        case ImageEntityType.TvDB_Cover:
                            TvDB_ImagePosterVM tvPoster = poster.PosterObject as TvDB_ImagePosterVM;
                            res = JMMServerVM.Instance.clientBinaryHTTP.SetDefaultImage(isDefault, ser.AniDB_ID,
                                tvPoster.TvDB_ImagePosterID, (int)ImageEntityType.TvDB_Cover, (int)ImageSizeType.Poster);
                            tvPoster.IsImageDefault = isDefault;
                            tvPoster.IsImageNotDefault = !isDefault;
                            break;

                        case ImageEntityType.Trakt_Poster:
                            Trakt_ImagePosterVM traktPoster = poster.PosterObject as Trakt_ImagePosterVM;
                            res = JMMServerVM.Instance.clientBinaryHTTP.SetDefaultImage(isDefault, ser.AniDB_ID,
                                traktPoster.Trakt_ImagePosterID, (int)ImageEntityType.Trakt_Poster, (int)ImageSizeType.Poster);
                            traktPoster.IsImageDefault = isDefault;
                            traktPoster.IsImageNotDefault = !isDefault;
                            break;

                        case ImageEntityType.AniDB_Cover:
                            AniDB_AnimeVM anime = poster.PosterObject as AniDB_AnimeVM;
                            res = JMMServerVM.Instance.clientBinaryHTTP.SetDefaultImage(isDefault, ser.AniDB_ID,
                                anime.AnimeID, (int)ImageEntityType.AniDB_Cover, (int)ImageSizeType.Poster);
                            anime.IsImageDefault = isDefault;
                            anime.IsImageNotDefault = !isDefault;
                            break;

                        case ImageEntityType.MovieDB_Poster:
                            MovieDB_PosterVM moviePoster = poster.PosterObject as MovieDB_PosterVM;
                            res = JMMServerVM.Instance.clientBinaryHTTP.SetDefaultImage(isDefault, ser.AniDB_ID,
                                moviePoster.MovieDB_PosterID, (int)ImageEntityType.MovieDB_Poster, (int)ImageSizeType.Poster);
                            moviePoster.IsImageDefault = isDefault;
                            moviePoster.IsImageNotDefault = !isDefault;
                            break;
                    }
                    poster.IsImageDefault = isDefault;
                    poster.IsImageNotDefault = !isDefault;
                }

                if (img.GetType() == typeof(FanartContainer))
                {
                    FanartContainer fanart = (FanartContainer)img;

                    if (isDefault && fanart.IsImageDisabled)
                    {
                        MessageBox.Show(disabledMessage, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    switch (fanart.ImageType)
                    {
                        case ImageEntityType.TvDB_FanArt:
                            TvDB_ImageFanartVM tvFanart = fanart.FanartObject as TvDB_ImageFanartVM;
                            res = JMMServerVM.Instance.clientBinaryHTTP.SetDefaultImage(isDefault, ser.AniDB_ID,
                                tvFanart.TvDB_ImageFanartID, (int)ImageEntityType.TvDB_FanArt, (int)ImageSizeType.Fanart);
                            tvFanart.IsImageDefault = isDefault;
                            tvFanart.IsImageNotDefault = !isDefault;
                            break;

                        case ImageEntityType.Trakt_Fanart:
                            Trakt_ImageFanartVM traktFanart = fanart.FanartObject as Trakt_ImageFanartVM;
                            res = JMMServerVM.Instance.clientBinaryHTTP.SetDefaultImage(isDefault, ser.AniDB_ID,
                                traktFanart.Trakt_ImageFanartID, (int)ImageEntityType.Trakt_Fanart, (int)ImageSizeType.Fanart);
                            traktFanart.IsImageDefault = isDefault;
                            traktFanart.IsImageNotDefault = !isDefault;
                            break;

                        case ImageEntityType.MovieDB_FanArt:
                            MovieDB_FanartVM movieFanart = fanart.FanartObject as MovieDB_FanartVM;
                            res = JMMServerVM.Instance.clientBinaryHTTP.SetDefaultImage(isDefault, ser.AniDB_ID,
                                movieFanart.MovieDB_FanartID, (int)ImageEntityType.MovieDB_FanArt, (int)ImageSizeType.Fanart);
                            movieFanart.IsImageDefault = isDefault;
                            movieFanart.IsImageNotDefault = !isDefault;
                            break;
                    }
                    fanart.IsImageDefault = isDefault;
                    fanart.IsImageNotDefault = !isDefault;
                }

                if (img.GetType() == typeof(TvDB_ImageWideBannerVM))
                {
                    TvDB_ImageWideBannerVM banner = (TvDB_ImageWideBannerVM)img;

                    if (isDefault && banner.IsImageDisabled)
                    {
                        MessageBox.Show(disabledMessage, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }


                    res = JMMServerVM.Instance.clientBinaryHTTP.SetDefaultImage(isDefault, ser.AniDB_ID,
                                banner.TvDB_ImageWideBannerID, (int)ImageEntityType.TvDB_Banner, (int)ImageSizeType.WideBanner);
                    banner.IsImageDefault = isDefault;
                    banner.IsImageNotDefault = !isDefault;
                }

                if (res.Length > 0)
                {
                    MessageBox.Show(res, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (refreshData)
                    {
                        MainListHelperVM.Instance.UpdateHeirarchy(ser);
                        MainListHelperVM.Instance.UpdateAnime(ser.AniDB_ID);
                        RefreshImagesData();
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

        void btnUpdateAniDBInfo_Click(object sender, RoutedEventArgs e)
        {
            AnimeSeriesVM ser = this.DataContext as AnimeSeriesVM;
            if (ser == null) return;

            try
            {
                this.Cursor = Cursors.Wait;
                JMMServerVM.Instance.clientBinaryHTTP.UpdateAnimeData(ser.AniDB_ID);

                // refresh the data
                MainListHelperVM.Instance.UpdateHeirarchy(ser);
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

        void tabContainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                TabControl tab = e.Source as TabControl;
                if (tab.SelectedIndex == 1) // episodes
                {
                    this.Cursor = Cursors.Wait;
                    epListMain.PopulateToolbars();
                    this.Cursor = Cursors.Arrow;
                }
                else if (tab.SelectedIndex == 2) // images
                {
                    RefreshImagesData();
                }
                else if (tab.SelectedIndex == 3) // related and similar
                {
                    this.Cursor = Cursors.Wait;
                    ucSimilarAnime.RefreshData();
                    ucRelatedAnime.RefreshData();
                    this.Cursor = Cursors.Arrow;
                }
                else if (tab.SelectedIndex == 4) // trakt shouts
                {
                    this.Cursor = Cursors.Wait;
                    ucTraktShouts.RefreshComments();
                    this.Cursor = Cursors.Arrow;
                }
                else if (tab.SelectedIndex == 5) // files
                {
                    this.Cursor = Cursors.Wait;
                    ShowFileSummary();
                    this.Cursor = Cursors.Arrow;
                }

            }
        }

        private void RefreshImagesData()
        {
            this.Cursor = Cursors.Wait;

            try
            {
                AniDB_AnimeCrossRefsVM AniDB_AnimeCrossRefs = null;
                SeriesTvDBWideBanners = null;
                AllPosters = null;
                AllFanarts = null;

                AnimeSeriesVM ser = this.DataContext as AnimeSeriesVM;
                if (ser == null) return;

                JMMServerBinary.Contract_AniDB_AnimeCrossRefs xrefDetails = JMMServerVM.Instance.clientBinaryHTTP.GetCrossRefDetails(ser.AniDB_ID);
                if (xrefDetails == null) return;

                AniDB_AnimeCrossRefs = new AniDB_AnimeCrossRefsVM();
                AniDB_AnimeCrossRefs.Populate(xrefDetails);

                SeriesTvDBWideBanners = AniDB_AnimeCrossRefs.TvDBImageWideBanners;

                AllPosters = AniDB_AnimeCrossRefs.AllPosters;
                AllFanarts = AniDB_AnimeCrossRefs.AllFanarts;
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

        void btnTvDBLinks_Click(object sender, RoutedEventArgs e)
        {
            UserSettingsVM.Instance.SeriesTvDBLinksExpanded = !UserSettingsVM.Instance.SeriesTvDBLinksExpanded;

            ShowTvDBLinks();


        }

        void btnAnimeTitles_Click(object sender, RoutedEventArgs e)
        {
            UserSettingsVM.Instance.TitlesExpanded = !UserSettingsVM.Instance.TitlesExpanded;
        }

        void btnAnimeTags_Click(object sender, RoutedEventArgs e)
        {
            UserSettingsVM.Instance.TagsExpanded = !UserSettingsVM.Instance.TagsExpanded;
        }

        void btnCustomTags_Click(object sender, RoutedEventArgs e)
        {
            UserSettingsVM.Instance.CustomTagsExpanded = !UserSettingsVM.Instance.CustomTagsExpanded;
        }

        void btnManageTags_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window wdw = Window.GetWindow(this);

                ManageCustomTags frm = new ManageCustomTags();
                frm.Owner = wdw;
                bool? result = frm.ShowDialog();
                if (result.Value)
                {
                    // refresh
                    //RefreshRecords();
                }

            }
            catch { }
        }

        void btnAddCustomTag_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cboCustomTag.SelectedItem == null) return;

                AnimeSeriesVM ser = this.DataContext as AnimeSeriesVM;
                if (ser == null) return;

                CustomTagVM tag = cboCustomTag.SelectedItem as CustomTagVM;

                // check if we already have this tag
                foreach (CustomTagVM ctag in ser.AniDB_Anime.Detail.CustomTags)
                {
                    if (ctag.CustomTagID == tag.CustomTagID)
                        return;
                }

                JMMServerBinary.Contract_CrossRef_CustomTag xref = new JMMServerBinary.Contract_CrossRef_CustomTag();
                xref.CrossRefID = ser.AniDB_ID;
                xref.CrossRefType = (int)CustomTagCrossRefType.Anime;
                xref.CustomTagID = tag.CustomTagID;

                JMMServerBinary.Contract_CrossRef_CustomTag_SaveResponse resp = JMMServerVM.Instance.clientBinaryHTTP.SaveCustomTagCrossRef(xref);
                if (!string.IsNullOrEmpty(resp.ErrorMessage))
                {
                    Utils.ShowErrorMessage(resp.ErrorMessage);
                    return;
                }
                else
                {
                    ser.AniDB_Anime.Detail.CustomTags.Add(tag);
                    ser.AniDB_Anime.Detail.ViewCustomTags.Refresh();
                }

            }
            catch { }
        }

        private void CommandBinding_DeleteCustomTag(object sender, ExecutedRoutedEventArgs e)
        {
            object obj = e.Parameter;
            if (obj == null) return;

            try
            {
                AnimeSeriesVM ser = this.DataContext as AnimeSeriesVM;
                if (ser == null) return;

                this.Cursor = Cursors.Wait;
                string res = "";


                // NOTE if we are disabling an image we should also make sure it is not the default
                CustomTagVM tag = null;
                if (obj.GetType() == typeof(CustomTagVM))
                {
                    tag = (CustomTagVM)obj;
                    res = JMMServerVM.Instance.clientBinaryHTTP.DeleteCustomTagCrossRef(
                        tag.CustomTagID, (int)CustomTagCrossRefType.Anime, ser.AniDB_ID);

                }

                if (res.Length > 0)
                {
                    MessageBox.Show(res, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    CustomTagVM ctagToRemove = null;
                    foreach (CustomTagVM ctag in ser.AniDB_Anime.Detail.CustomTags)
                    {
                        if (ctag.CustomTagID == tag.CustomTagID)
                        {
                            ctagToRemove = ctag;
                            break;
                        }

                    }

                    if (ctagToRemove != null)
                    {
                        ser.AniDB_Anime.Detail.CustomTags.Remove(ctagToRemove);
                        ser.AniDB_Anime.Detail.ViewCustomTags.Refresh();
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

        /*void btnPlayNextEpisode_Click(object sender, RoutedEventArgs e)
		{
			UserSettingsVM.Instance.SeriesNextEpisodeExpanded = !UserSettingsVM.Instance.SeriesNextEpisodeExpanded;

			ShowNextEpisode();
		}*/

        private void CommandBinding_RefreshSeries(object sender, ExecutedRoutedEventArgs e)
        {
            AnimeSeriesVM ser = this.DataContext as AnimeSeriesVM;
            if (ser == null) return;

            ser.RefreshBase();

            LoadSeries();
        }

        private void LoadSeries()
        {
            this.Cursor = Cursors.Wait;

            AnimeSeriesVM ser = this.DataContext as AnimeSeriesVM;
            if (ser == null) return;

            FullDescription = false;
            TruncatedDescription = true;

            ShowFileSummary();
            ShowTvDBLinks();
            ShowNextEpisode();



            ser.PopulateIsFave();

            epListMain.DataContext = ser;
            ucSimilarAnime.DataContext = ser;
            ucRelatedAnime.DataContext = ser;
            ucTraktShouts.DataContext = ser;

            if (tabContainer.SelectedIndex == 1) // episodes
            {
                epListMain.PopulateToolbars();
            }
            else if (tabContainer.SelectedIndex == 2) // images
            {
                RefreshImagesData();
            }
            else if (tabContainer.SelectedIndex == 3) // related and similar
            {
                this.Cursor = Cursors.Wait;
                ucSimilarAnime.RefreshData();
                ucRelatedAnime.RefreshData();
                this.Cursor = Cursors.Arrow;
            }
            else if (tabContainer.SelectedIndex == 4) // trakt shouts
            {
                this.Cursor = Cursors.Wait;
                ucTraktShouts.RefreshComments();
                this.Cursor = Cursors.Arrow;
            }
            else if (tabContainer.SelectedIndex == 5) // files
            {
                this.Cursor = Cursors.Wait;
                ShowFileSummary();
                this.Cursor = Cursors.Arrow;
            }

            cboVoteType.Items.Clear();
            cboVoteType.Items.Add(Properties.Resources.VoteTypeAnimeTemporary);
            if (ser.AniDB_Anime.FinishedAiring)
                cboVoteType.Items.Add(Properties.Resources.VoteTypeAnimePermanent);

            if (ser.AniDB_Anime.FinishedAiring && ser.AllFilesWatched)
                cboVoteType.SelectedIndex = 1;
            else
                cboVoteType.SelectedIndex = 0;


            EvaluateEditing();

            this.Cursor = Cursors.Arrow;
        }

        void AnimeSeries_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            LoadSeries();
        }


        private void ShowFileSummary()
        {
            if (tabContainer.SelectedIndex == 5)
            {
                AnimeSeriesVM ser = this.DataContext as AnimeSeriesVM;
                if (ser == null) return;
                ucFileSummary.DataContext = ser.AniDB_Anime;
                ucFolderSummary.DataContext = ser.AniDB_Anime;
            }
        }

        private void ShowTvDBLinks()
        {
            if (UserSettingsVM.Instance.SeriesTvDBLinksExpanded)
            {
                AnimeSeriesVM ser = this.DataContext as AnimeSeriesVM;
                if (ser == null) return;
                ucTvDBLinks.DataContext = ser.AniDB_Anime;
            }
        }

        private void ShowNextEpisode()
        {
            AnimeSeriesVM ser = this.DataContext as AnimeSeriesVM;
            if (ser == null) return;

            JMMServerBinary.Contract_AnimeEpisode ep = JMMServerVM.Instance.clientBinaryHTTP.GetNextUnwatchedEpisode(ser.AnimeSeriesID.Value,
                JMMServerVM.Instance.CurrentUser.JMMUserID.Value);
            if (ep != null)
            {
                AnimeEpisodeVM aniep = new AnimeEpisodeVM(ep);
                aniep.SetTvDBInfo();
                ucNextEpisode.DataContext = aniep;
            }
            else
            {
                ucNextEpisode.EpisodeExists = false;
                ucNextEpisode.EpisodeMissing = true;
                ucNextEpisode.DataContext = null;
            }
        }

        void btnAnimeGroupShow_Click(object sender, RoutedEventArgs e)
        {
            UserSettingsVM.Instance.SeriesGroupExpanded = !UserSettingsVM.Instance.SeriesGroupExpanded;
        }



        void AnimeSeries_Loaded(object sender, RoutedEventArgs e)
        {
            if (cboCustomTag.Items != null && cboCustomTag.Items.Count > 0)
                cboCustomTag.SelectedIndex = 0;
        }

        void cRating_OnRatingValueChangedEvent(RatingValueEventArgs ev)
        {
            AnimeSeriesVM ser = this.DataContext as AnimeSeriesVM;
            if (ser == null) return;

            try
            {
                decimal rating = (decimal)ev.RatingValue;

                int voteType = 1;
                if (cboVoteType.SelectedItem.ToString() == Properties.Resources.VoteTypeAnimeTemporary) voteType = 2;
                if (cboVoteType.SelectedItem.ToString() == Properties.Resources.VoteTypeAnimePermanent) voteType = 1;

                JMMServerVM.Instance.VoteAnime(ser.AniDB_ID, rating, voteType);

                // refresh the data
                //ser.RefreshBase();
                //ser.AniDB_Anime.Detail.RefreshBase();
                MainListHelperVM.Instance.UpdateHeirarchy(ser);
            }
            catch (Exception ex)
            {
                Utils.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// This event bubbles up from PlayEpisodeControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBinding_ToggleWatchedStatus(object sender, ExecutedRoutedEventArgs e)
        {
            object obj = e.Parameter;
            if (obj == null) return;

            this.Cursor = Cursors.Wait;

            try
            {
                Window parentWindow = Window.GetWindow(this);
                AnimeSeriesVM serTemp = null;
                bool newStatus = false;

                if (obj.GetType() == typeof(VideoDetailedVM))
                {
                    VideoDetailedVM vid = obj as VideoDetailedVM;
                    newStatus = !vid.Watched;
                    JMMServerVM.Instance.clientBinaryHTTP.ToggleWatchedStatusOnVideo(vid.VideoLocalID, newStatus, JMMServerVM.Instance.CurrentUser.JMMUserID.Value);

                    MainListHelperVM.Instance.UpdateHeirarchy(vid);

                    serTemp = MainListHelperVM.Instance.GetSeriesForVideo(vid.VideoLocalID);
                }

                if (obj.GetType() == typeof(AnimeEpisodeVM))
                {
                    AnimeEpisodeVM ep = obj as AnimeEpisodeVM;
                    newStatus = !ep.Watched;
                    JMMServerBinary.Contract_ToggleWatchedStatusOnEpisode_Response response = JMMServerVM.Instance.clientBinaryHTTP.ToggleWatchedStatusOnEpisode(ep.AnimeEpisodeID,
                        newStatus, JMMServerVM.Instance.CurrentUser.JMMUserID.Value);
                    if (!string.IsNullOrEmpty(response.ErrorMessage))
                    {
                        MessageBox.Show(response.ErrorMessage, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    MainListHelperVM.Instance.UpdateHeirarchy(response.AnimeEpisode);

                    serTemp = MainListHelperVM.Instance.GetSeriesForEpisode(ep);
                }

                ShowNextEpisode();

                if (newStatus == true && serTemp != null)
                {
                    Utils.PromptToRateSeries(serTemp, parentWindow);
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

    public class PlaylistMenuCommand
    {
        public PlaylistItemType AddType { get; set; }
        public int PlaylistID { get; set; }

        public PlaylistMenuCommand()
        {
        }

        public PlaylistMenuCommand(PlaylistItemType addType, int playlistID)
        {
            AddType = addType;
            PlaylistID = playlistID;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", AddType, PlaylistID);
        }
    }

    public enum PlaylistItemType
    {
        Series = 1,
        AllEpisodes = 2,
        UnwatchedEpisodes = 3,
        SingleEpisode = 4
    }


}
