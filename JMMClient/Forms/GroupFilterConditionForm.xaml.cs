﻿using JMMClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace JMMClient.Forms
{
    /// <summary>
    /// Interaction logic for GroupFilterConditionForm.xaml
    /// </summary>
    public partial class GroupFilterConditionForm : Window
    {
        public GroupFilterVM groupFilter = null;
        public GroupFilterConditionVM groupFilterCondition = null;
        public ICollectionView ViewGroups { get; set; }
        public ObservableCollection<AnimeGroupVM> AllGroups { get; set; }

        public ICollectionView ViewTagNames { get; set; }
        public ObservableCollection<string> AllTagNames { get; set; }

        public ICollectionView ViewCustomTagNames { get; set; }
        public ObservableCollection<string> AllCustomTagNames { get; set; }

        public ObservableCollection<string> AllVideoQuality { get; set; }
        public ObservableCollection<string> AllAnimeTypes { get; set; }
        public ObservableCollection<string> AllAudioLanguages { get; set; }
        public ObservableCollection<string> AllSubtitleLanguages { get; set; }

        public static readonly DependencyProperty IsParameterDateProperty = DependencyProperty.Register("IsParameterDate",
            typeof(bool), typeof(GroupFilterConditionForm), new UIPropertyMetadata(false, null));

        public bool IsParameterDate
        {
            get { return (bool)GetValue(IsParameterDateProperty); }
            set { SetValue(IsParameterDateProperty, value); }
        }

        public static readonly DependencyProperty IsParameterAnimeGroupProperty = DependencyProperty.Register("IsParameterAnimeGroup",
            typeof(bool), typeof(GroupFilterConditionForm), new UIPropertyMetadata(false, null));

        public bool IsParameterAnimeGroup
        {
            get { return (bool)GetValue(IsParameterAnimeGroupProperty); }
            set { SetValue(IsParameterAnimeGroupProperty, value); }
        }

        public static readonly DependencyProperty IsParameterAnimeTypeProperty = DependencyProperty.Register("IsParameterAnimeType",
            typeof(bool), typeof(GroupFilterConditionForm), new UIPropertyMetadata(false, null));

        public bool IsParameterAnimeType
        {
            get { return (bool)GetValue(IsParameterAnimeTypeProperty); }
            set { SetValue(IsParameterAnimeTypeProperty, value); }
        }

        public static readonly DependencyProperty IsParameterTextProperty = DependencyProperty.Register("IsParameterText",
            typeof(bool), typeof(GroupFilterConditionForm), new UIPropertyMetadata(false, null));

        public bool IsParameterText
        {
            get { return (bool)GetValue(IsParameterTextProperty); }
            set { SetValue(IsParameterTextProperty, value); }
        }

        public static readonly DependencyProperty IsParameterInNotInProperty = DependencyProperty.Register("IsParameterInNotIn",
            typeof(bool), typeof(GroupFilterConditionForm), new UIPropertyMetadata(false, null));

        public bool IsParameterInNotIn
        {
            get { return (bool)GetValue(IsParameterInNotInProperty); }
            set { SetValue(IsParameterInNotInProperty, value); }
        }

        public static readonly DependencyProperty IsParameterTagProperty = DependencyProperty.Register("IsParameterTag",
            typeof(bool), typeof(GroupFilterConditionForm), new UIPropertyMetadata(false, null));

        public bool IsParameterTag
        {
            get { return (bool)GetValue(IsParameterTagProperty); }
            set { SetValue(IsParameterTagProperty, value); }
        }

        public static readonly DependencyProperty IsParameterCustomTagProperty = DependencyProperty.Register("IsParameterCustomTag",
            typeof(bool), typeof(GroupFilterConditionForm), new UIPropertyMetadata(false, null));

        public bool IsParameterCustomTag
        {
            get { return (bool)GetValue(IsParameterCustomTagProperty); }
            set { SetValue(IsParameterCustomTagProperty, value); }
        }

        public static readonly DependencyProperty IsParameterVideoQualityProperty = DependencyProperty.Register("IsParameterVideoQuality",
            typeof(bool), typeof(GroupFilterConditionForm), new UIPropertyMetadata(false, null));

        public bool IsParameterVideoQuality
        {
            get { return (bool)GetValue(IsParameterVideoQualityProperty); }
            set { SetValue(IsParameterVideoQualityProperty, value); }
        }

        public static readonly DependencyProperty IsParameterAudioLanguageProperty = DependencyProperty.Register("IsParameterAudioLanguage",
            typeof(bool), typeof(GroupFilterConditionForm), new UIPropertyMetadata(false, null));

        public bool IsParameterAudioLanguage
        {
            get { return (bool)GetValue(IsParameterAudioLanguageProperty); }
            set { SetValue(IsParameterAudioLanguageProperty, value); }
        }

        public static readonly DependencyProperty IsParameterSubtitleLanguageProperty = DependencyProperty.Register("IsParameterSubtitleLanguage",
            typeof(bool), typeof(GroupFilterConditionForm), new UIPropertyMetadata(false, null));

        public bool IsParameterSubtitleLanguage
        {
            get { return (bool)GetValue(IsParameterSubtitleLanguageProperty); }
            set { SetValue(IsParameterSubtitleLanguageProperty, value); }
        }


        public static readonly DependencyProperty IsParameterRatingProperty = DependencyProperty.Register("IsParameterRating",
            typeof(bool), typeof(GroupFilterConditionForm), new UIPropertyMetadata(false, null));

        public bool IsParameterRating
        {
            get { return (bool)GetValue(IsParameterRatingProperty); }
            set { SetValue(IsParameterRatingProperty, value); }
        }

        public static readonly DependencyProperty IsParameterIntegerProperty = DependencyProperty.Register("IsParameterInteger",
            typeof(bool), typeof(GroupFilterConditionForm), new UIPropertyMetadata(false, null));

        public bool IsParameterInteger
        {
            get { return (bool)GetValue(IsParameterIntegerProperty); }
            set { SetValue(IsParameterIntegerProperty, value); }
        }

        public static readonly DependencyProperty IsParameterLastXDaysProperty = DependencyProperty.Register("IsParameterLastXDays",
            typeof(bool), typeof(GroupFilterConditionForm), new UIPropertyMetadata(false, null));

        public bool IsParameterLastXDays
        {
            get { return (bool)GetValue(IsParameterLastXDaysProperty); }
            set { SetValue(IsParameterLastXDaysProperty, value); }
        }

        public GroupFilterConditionForm()
        {
            InitializeComponent();

            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(AppSettings.Culture);

            txtGroupSearch.TextChanged += new TextChangedEventHandler(txtGroupSearch_TextChanged);
            btnClearGroupSearch.Click += new RoutedEventHandler(btnClearGroupSearch_Click);

            txtTagSearch.TextChanged += new TextChangedEventHandler(txtTagSearch_TextChanged);
            btnClearTagSearch.Click += new RoutedEventHandler(btnClearTagSearch_Click);

            btnCancel.Click += new RoutedEventHandler(btnCancel_Click);
            btnConfirm.Click += new RoutedEventHandler(btnConfirm_Click);

            lbTags.MouseDoubleClick += new MouseButtonEventHandler(lbTags_MouseDoubleClick);
            lbVideoQuality.MouseDoubleClick += new MouseButtonEventHandler(lbVideoQuality_MouseDoubleClick);
            lbAnimeTypes.MouseDoubleClick += new MouseButtonEventHandler(lbAnimeTypes_MouseDoubleClick);
            lbAudioLanguages.MouseDoubleClick += new MouseButtonEventHandler(lbAudioLanguages_MouseDoubleClick);
            lbSubtitleLanguages.MouseDoubleClick += new MouseButtonEventHandler(lbSubtitleLanguages_MouseDoubleClick);

            btnClearCustomTagSearch.Click += btnClearCustomTagSearch_Click;
            txtCustomTagSearch.TextChanged += txtCustomTagSearch_TextChanged;
            lbCustomTags.MouseDoubleClick += lbCustomTags_MouseDoubleClick;
        }







        void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            // get the details from the form
            groupFilterCondition.ConditionType = (int)GroupFilterHelper.GetEnumForText_ConditionType(cboConditionType.SelectedItem.ToString());
            groupFilterCondition.ConditionOperator = (int)GroupFilterHelper.GetEnumForText_Operator(cboConditionOperator.SelectedItem.ToString());

            NumberStyles style = NumberStyles.Number;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");


            // get the parameter details
            if (IsParameterDate)
            {
                if (dpDate.SelectedDate == null)
                {
                    MessageBox.Show(Properties.Resources.MSG_ERR_SelectDate, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    dpDate.Focus();
                    return;
                }
                else
                {
                    groupFilterCondition.ConditionParameter = GroupFilterHelper.GetDateAsString(dpDate.SelectedDate.Value);
                }

            }

            if (IsParameterRating)
            {
                if (txtParameter.Text.Trim().Length == 0)
                {
                    MessageBox.Show(Properties.Resources.MSG_ERR_EnterValue, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    txtParameter.Focus();
                    return;
                }
                else
                {
                    decimal dRating = -1;
                    decimal.TryParse(txtParameter.Text, style, culture, out dRating);
                    if (dRating <= 0 || dRating > 10)
                    {
                        MessageBox.Show(Properties.Resources.MSG_ERR_RatingValue, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                        txtParameter.Focus();
                        return;
                    }

                    groupFilterCondition.ConditionParameter = txtParameter.Text.Trim();
                }

            }

            if (IsParameterInteger)
            {
                if (txtParameter.Text.Trim().Length == 0)
                {
                    MessageBox.Show(Properties.Resources.MSG_ERR_EnterValue, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    txtParameter.Focus();
                    return;
                }
                else
                {
                    int parmInt = -1;
                    if (!int.TryParse(txtParameter.Text, out parmInt))
                    {
                        MessageBox.Show(Properties.Resources.GroupFilter_IntegerOnly, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                        txtParameter.Focus();
                        return;
                    }

                    groupFilterCondition.ConditionParameter = parmInt.ToString();
                }

            }

            if (IsParameterLastXDays)
            {
                if (txtParameter.Text.Trim().Length == 0)
                {
                    MessageBox.Show(Properties.Resources.MSG_ERR_EnterValue, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    txtParameter.Focus();
                    return;
                }
                else
                {
                    int days = -1;
                    int.TryParse(txtParameter.Text, out days);
                    if (days < 1 || days > int.MaxValue)
                    {
                        MessageBox.Show(Properties.Resources.MSG_ERR_DaysValue, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                        txtParameter.Focus();
                        return;
                    }

                    groupFilterCondition.ConditionParameter = txtParameter.Text.Trim();
                }

            }


            if (IsParameterTag)
            {
                if (txtSelectedTags.Text.Trim().Length == 0)
                {
                    MessageBox.Show(Properties.Resources.MSG_ERR_EnterValue, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    txtParameter.Focus();
                    return;
                }
                else
                {
                    // validate
                    string[] cats = txtSelectedTags.Text.Trim().Split(',');
                    groupFilterCondition.ConditionParameter = "";
                    foreach (string cat in cats)
                    {
                        if (cat.Trim().Length == 0) continue;
                        if (cat.Trim() == ", ") continue;

                        if (groupFilterCondition.ConditionParameter.Length > 0) groupFilterCondition.ConditionParameter += ", ";
                        groupFilterCondition.ConditionParameter += cat;
                    }

                }

            }

            if (IsParameterCustomTag)
            {
                if (txtSelectedCustomTags.Text.Trim().Length == 0)
                {
                    MessageBox.Show(Properties.Resources.MSG_ERR_EnterValue, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    txtSelectedCustomTags.Focus();
                    return;
                }
                else
                {
                    // validate
                    string[] tags = txtSelectedCustomTags.Text.Trim().Split(',');
                    groupFilterCondition.ConditionParameter = "";
                    foreach (string tag in tags)
                    {
                        if (tag.Trim().Length == 0) continue;
                        if (tag.Trim() == ", ") continue;

                        if (groupFilterCondition.ConditionParameter.Length > 0) groupFilterCondition.ConditionParameter += ", ";
                        groupFilterCondition.ConditionParameter += tag;
                    }

                }

            }

            if (IsParameterVideoQuality)
            {

                if (txtSelectedVideoQuality.Text.Trim().Length == 0)
                {
                    MessageBox.Show(Properties.Resources.MSG_ERR_EnterValue, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    txtParameter.Focus();
                    return;
                }
                else
                {
                    // validate
                    string[] vidQuals = txtSelectedVideoQuality.Text.Trim().Split(',');
                    groupFilterCondition.ConditionParameter = "";
                    foreach (string vidq in vidQuals)
                    {
                        if (vidq.Trim().Length == 0) continue;
                        if (vidq.Trim() == ", ") continue;

                        if (groupFilterCondition.ConditionParameter.Length > 0) groupFilterCondition.ConditionParameter += ", ";
                        groupFilterCondition.ConditionParameter += vidq;
                    }

                }

            }

            if (IsParameterAudioLanguage)
            {

                if (txtSelectedAudioLanguages.Text.Trim().Length == 0)
                {
                    MessageBox.Show(Properties.Resources.MSG_ERR_EnterValue, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    txtParameter.Focus();
                    return;
                }
                else
                {
                    // validate
                    string[] languages = txtSelectedAudioLanguages.Text.Trim().Split(',');
                    groupFilterCondition.ConditionParameter = "";
                    foreach (string lanName in languages)
                    {
                        if (lanName.Trim().Length == 0) continue;
                        if (lanName.Trim() == ", ") continue;

                        if (groupFilterCondition.ConditionParameter.Length > 0) groupFilterCondition.ConditionParameter += ", ";
                        groupFilterCondition.ConditionParameter += lanName;
                    }

                }

            }

            if (IsParameterSubtitleLanguage)
            {

                if (txtSelectedSubtitleLanguages.Text.Trim().Length == 0)
                {
                    MessageBox.Show(Properties.Resources.MSG_ERR_EnterValue, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    txtParameter.Focus();
                    return;
                }
                else
                {
                    // validate
                    string[] languages = txtSelectedSubtitleLanguages.Text.Trim().Split(',');
                    groupFilterCondition.ConditionParameter = "";
                    foreach (string lanName in languages)
                    {
                        if (lanName.Trim().Length == 0) continue;
                        if (lanName.Trim() == ", ") continue;

                        if (groupFilterCondition.ConditionParameter.Length > 0) groupFilterCondition.ConditionParameter += ", ";
                        groupFilterCondition.ConditionParameter += lanName;
                    }

                }

            }

            if (IsParameterAnimeType)
            {


                if (txtSelectedAnimeTypes.Text.Trim().Length == 0)
                {
                    MessageBox.Show(Properties.Resources.MSG_ERR_EnterValue, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    txtParameter.Focus();
                    return;
                }
                else
                {
                    // validate
                    string[] aTypes = txtSelectedAnimeTypes.Text.Trim().Split(',');
                    groupFilterCondition.ConditionParameter = "";
                    foreach (string aType in aTypes)
                    {
                        if (aType.Trim().Length == 0) continue;
                        if (aType.Trim() == ", ") continue;

                        if (groupFilterCondition.ConditionParameter.Length > 0) groupFilterCondition.ConditionParameter += ", ";
                        groupFilterCondition.ConditionParameter += aType;
                    }

                }

            }

            if (IsParameterAnimeGroup)
            {
                if (lbGroups.SelectedItem == null)
                {
                    MessageBox.Show(Properties.Resources.MSG_ERR_GroupSelectionRequired, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    lbGroups.Focus();
                    return;
                }
                else
                {
                    AnimeGroupVM grp = lbGroups.SelectedItem as AnimeGroupVM;
                    groupFilterCondition.ConditionParameter = grp.AnimeGroupID.Value.ToString();
                }
            }

            if (IsParameterText)
            {
                if (txtParameter.Text.Trim().Length == 0)
                {
                    MessageBox.Show(Properties.Resources.MSG_ERR_EnterValue, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    txtParameter.Focus();
                    return;
                }
                else
                {
                    groupFilterCondition.ConditionParameter = txtParameter.Text.Trim();
                }

            }

            this.DialogResult = true;
            this.Close();
        }

        void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void EvaluateConditionsAndOperators()
        {
            if (cboConditionType.SelectedItem == null || cboConditionOperator.SelectedItem == null) return;

            GroupFilterConditionType conditionType = GroupFilterHelper.GetEnumForText_ConditionType(cboConditionType.SelectedItem.ToString());
            GroupFilterOperator opType = GroupFilterHelper.GetEnumForText_Operator(cboConditionOperator.SelectedItem.ToString());

            IsParameterDate = false;
            IsParameterAnimeGroup = false;
            IsParameterAnimeType = false;
            IsParameterText = false;
            IsParameterInNotIn = false;
            IsParameterTag = false;
            IsParameterCustomTag = false;
            IsParameterRating = false;
            IsParameterLastXDays = false;
            IsParameterVideoQuality = false;
            IsParameterAudioLanguage = false;
            IsParameterSubtitleLanguage = false;
            IsParameterInteger = false;

			switch (conditionType)
			{
				case GroupFilterConditionType.AirDate:
				case GroupFilterConditionType.SeriesCreatedDate:
				case GroupFilterConditionType.EpisodeAddedDate:
				case GroupFilterConditionType.EpisodeWatchedDate:
                case GroupFilterConditionType.LatestEpisodeAirDate:
					if (opType == GroupFilterOperator.LastXDays)
					{
						IsParameterLastXDays = true;
						IsParameterText = true;
					}
					else
						IsParameterDate = true; 
					break;

                case GroupFilterConditionType.AnimeGroup:
                    IsParameterAnimeGroup = true;
                    break;

                case GroupFilterConditionType.AnimeType:
                    IsParameterAnimeType = true; break;

                case GroupFilterConditionType.ReleaseGroup:
                case GroupFilterConditionType.Studio:
                    IsParameterInNotIn = true; break;

                case GroupFilterConditionType.Tag:
                    IsParameterInNotIn = true;
                    IsParameterTag = true;
                    break;

                case GroupFilterConditionType.CustomTags:
                    IsParameterInNotIn = true;
                    IsParameterCustomTag = true;
                    break;

                case GroupFilterConditionType.AudioLanguage:
                    IsParameterInNotIn = true;
                    IsParameterAudioLanguage = true;
                    break;

                case GroupFilterConditionType.SubtitleLanguage:
                    IsParameterInNotIn = true;
                    IsParameterSubtitleLanguage = true;
                    break;

                case GroupFilterConditionType.Year:
                    IsParameterInNotIn = true;
                    break;

                case GroupFilterConditionType.VideoQuality:
					IsParameterInNotIn = true;
					IsParameterVideoQuality = true;
					break;

                case GroupFilterConditionType.AniDBRating:
                case GroupFilterConditionType.UserRating:
                    IsParameterText = true;
                    IsParameterRating = true;
                    break;

                case GroupFilterConditionType.EpisodeCount:
                    IsParameterText = true;
                    IsParameterInteger = true;
                    break;

            }
        }

        void cboConditionOperator_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EvaluateConditionsAndOperators();
        }

        void cboConditionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GroupFilterConditionType conditionType = GroupFilterHelper.GetEnumForText_ConditionType(cboConditionType.SelectedItem.ToString());

            cboConditionOperator.Items.Clear();
            foreach (string op in GroupFilterHelper.GetAllowedOperators(conditionType))
                cboConditionOperator.Items.Add(op);

            cboConditionOperator.SelectedIndex = 0;

            EvaluateConditionsAndOperators();
        }

        private void PopulateTags()
        {
            AllTagNames = new ObservableCollection<string>();

            ViewTagNames = CollectionViewSource.GetDefaultView(AllTagNames);
            List<string> tagsRaw = JMMServerVM.Instance.clientBinaryHTTP.GetAllTagNames();

            foreach (string tag in tagsRaw)
                AllTagNames.Add(tag);

            ViewTagNames.Filter = TagFilter;
        }

        private void PopulateCustomTags()
        {
            AllCustomTagNames = new ObservableCollection<string>();

            ViewCustomTagNames = CollectionViewSource.GetDefaultView(AllCustomTagNames);

            List<SortPropOrFieldAndDirection> sortCriteria = new List<SortPropOrFieldAndDirection>();
            sortCriteria.Add(new SortPropOrFieldAndDirection("TagName", false, SortType.eString));
            List<CustomTagVM> tags = Sorting.MultiSort<CustomTagVM>(JMMServerVM.Instance.AllCustomTags.ToList(), sortCriteria);

            foreach (CustomTagVM tag in tags)
                AllCustomTagNames.Add(tag.TagName);


            ViewCustomTagNames.Filter = CustomTagFilter;
            ViewCustomTagNames.Refresh();
        }

        private void PopulateVideoQuality()
        {
            AllVideoQuality = new ObservableCollection<string>();

            List<string> vidsRaw = JMMServerVM.Instance.clientBinaryHTTP.GetAllUniqueVideoQuality();
            vidsRaw.Sort();

            foreach (string vidq in vidsRaw)
                AllVideoQuality.Add(vidq);
        }

        private void PopulateLanguages()
        {
            AllAudioLanguages = new ObservableCollection<string>();

            List<string> audioRaw = JMMServerVM.Instance.clientBinaryHTTP.GetAllUniqueAudioLanguages();
            audioRaw.Sort();

            foreach (string aud in audioRaw)
                AllAudioLanguages.Add(aud);

            AllSubtitleLanguages = new ObservableCollection<string>();

            List<string> subRaw = JMMServerVM.Instance.clientBinaryHTTP.GetAllUniqueSubtitleLanguages();
            subRaw.Sort();

            foreach (string sub in subRaw)
                AllSubtitleLanguages.Add(sub);
        }

        private void PopulateAnimeTypes()
        {
            AllAnimeTypes = new ObservableCollection<string>();
            AllAnimeTypes.Add(JMMClient.Properties.Resources.AnimeType_Movie);
            AllAnimeTypes.Add(JMMClient.Properties.Resources.AnimeType_Other);
            AllAnimeTypes.Add(JMMClient.Properties.Resources.AnimeType_OVA);
            AllAnimeTypes.Add(JMMClient.Properties.Resources.AnimeType_TVSeries);
            AllAnimeTypes.Add(JMMClient.Properties.Resources.AnimeType_TVSpecial);
            AllAnimeTypes.Add(JMMClient.Properties.Resources.AnimeType_Web);
        }

        private void PopulateAnimeGroups()
        {
            AllGroups = new ObservableCollection<AnimeGroupVM>();

            ViewGroups = CollectionViewSource.GetDefaultView(AllGroups);
            ViewGroups.SortDescriptions.Add(new SortDescription("SortName", ListSortDirection.Ascending));

            List<JMMServerBinary.Contract_AnimeGroup> grpsRaw = JMMServerVM.Instance.clientBinaryHTTP.GetAllGroups(JMMServerVM.Instance.CurrentUser.JMMUserID.Value);

            foreach (JMMServerBinary.Contract_AnimeGroup grp in grpsRaw)
            {
                AnimeGroupVM grpNew = new AnimeGroupVM(grp);
                AllGroups.Add(grpNew);
            }

            ViewGroups.Filter = GroupSearchFilter;
        }

        public void Init(GroupFilterVM gf, GroupFilterConditionVM gfc)
        {
            groupFilter = gf;
            groupFilterCondition = gfc;


            try
            {
                cboConditionType.Items.Clear();
                foreach (string cond in GroupFilterHelper.GetAllConditionTypes())
                    cboConditionType.Items.Add(cond);

                PopulateAnimeGroups();
                PopulateTags();
                PopulateCustomTags();
                PopulateVideoQuality();
                PopulateAnimeTypes();
                PopulateLanguages();

                // find the right condition
                int idx = 0;
                for (int i = 0; i < cboConditionType.Items.Count; i++)
                {
                    GroupFilterConditionType conditionTypeTemp = GroupFilterHelper.GetEnumForText_ConditionType(cboConditionType.Items[i].ToString());
                    if (conditionTypeTemp == gfc.ConditionTypeEnum)
                    {
                        idx = i;
                        break;
                    }
                }
                cboConditionType.SelectedIndex = idx;
                GroupFilterConditionType conditionType = GroupFilterHelper.GetEnumForText_ConditionType(cboConditionType.SelectedItem.ToString());

                cboConditionOperator.Items.Clear();
                foreach (string op in GroupFilterHelper.GetAllowedOperators(conditionType))
                    cboConditionOperator.Items.Add(op);

                cboConditionType.SelectionChanged += new SelectionChangedEventHandler(cboConditionType_SelectionChanged);
                cboConditionOperator.SelectionChanged += new SelectionChangedEventHandler(cboConditionOperator_SelectionChanged);

                // find the right operator
                idx = 0;
                for (int i = 0; i < cboConditionOperator.Items.Count; i++)
                {
                    GroupFilterOperator opTypeTemp = GroupFilterHelper.GetEnumForText_Operator(cboConditionOperator.Items[i].ToString());
                    if (opTypeTemp == gfc.ConditionOperatorEnum)
                    {
                        idx = i;
                        break;
                    }
                }
                cboConditionOperator.SelectedIndex = idx;
                GroupFilterOperator opType = GroupFilterHelper.GetEnumForText_Operator(cboConditionOperator.Items[idx].ToString());

				// display the selected filter value
				switch (conditionType)
				{
					case GroupFilterConditionType.AirDate:
					case GroupFilterConditionType.SeriesCreatedDate:
					case GroupFilterConditionType.EpisodeAddedDate:
					case GroupFilterConditionType.EpisodeWatchedDate:
                    case GroupFilterConditionType.LatestEpisodeAirDate:

                        if (opType == GroupFilterOperator.LastXDays)
                            txtParameter.Text = gfc.ConditionParameter;
                        else
                        {
                            DateTime airDate = GroupFilterHelper.GetDateFromString(gfc.ConditionParameter);
                            dpDate.SelectedDate = airDate;
                        }
                        break;

                    case GroupFilterConditionType.AnimeGroup:

                        // don't display anything
                        break;

                    case GroupFilterConditionType.AnimeType:
                    case GroupFilterConditionType.Tag:
                    case GroupFilterConditionType.CustomTags:
					case GroupFilterConditionType.ReleaseGroup:
					case GroupFilterConditionType.Studio:
					case GroupFilterConditionType.VideoQuality:
					case GroupFilterConditionType.AniDBRating:
					case GroupFilterConditionType.UserRating:
					case GroupFilterConditionType.AudioLanguage:
					case GroupFilterConditionType.SubtitleLanguage:
                    case GroupFilterConditionType.Year:
						txtParameter.Text = gfc.ConditionParameter;
						break;
				}

            }
            catch (Exception ex)
            {
                Utils.ShowErrorMessage(ex);
            }

        }

        private bool GroupSearchFilter(object obj)
        {
            AnimeGroupVM grpvm = obj as AnimeGroupVM;
            if (grpvm == null) return true;

            return GroupSearchFilterHelper.EvaluateGroupTextSearch(grpvm, txtGroupSearch.Text.Replace("'", "`"));
        }

        private bool TagFilter(object obj)
        {
            string tagName = obj as string;
            if (tagName == null) return true;

            int index = tagName.IndexOf(txtTagSearch.Text.Trim(), 0, StringComparison.InvariantCultureIgnoreCase);
            if (index > -1) return true;
            return false;
        }

        private bool CustomTagFilter(object obj)
        {
            string tagName = obj as string;
            if (tagName == null) return true;


            int index = tagName.IndexOf(txtCustomTagSearch.Text.Trim(), 0, StringComparison.InvariantCultureIgnoreCase);
            if (index > -1) return true;
            return false;
        }

        void txtGroupSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewGroups.Refresh();
        }

        void btnClearGroupSearch_Click(object sender, RoutedEventArgs e)
        {
            txtGroupSearch.Text = "";
        }

        void btnClearTagSearch_Click(object sender, RoutedEventArgs e)
        {
            txtTagSearch.Text = "";
        }

        void txtTagSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewTagNames.Refresh();
        }

        void btnClearCustomTagSearch_Click(object sender, RoutedEventArgs e)
        {
            txtCustomTagSearch.Text = "";
        }

        void txtCustomTagSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewCustomTagNames.Refresh();
        }

        void lbSubtitleLanguages_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            object obj = lbSubtitleLanguages.SelectedItem;
            if (obj == null) return;

            string lanName = obj.ToString();
            string currentList = txtSelectedSubtitleLanguages.Text.Trim();

            // add to the selected list
            int index = currentList.IndexOf(lanName, 0, StringComparison.InvariantCultureIgnoreCase);
            if (index > -1) return;

            if (currentList.Length > 0) currentList += ", ";
            currentList += lanName;

            txtSelectedSubtitleLanguages.Text = currentList;
        }

        void lbAudioLanguages_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            object obj = lbAudioLanguages.SelectedItem;
            if (obj == null) return;

            string lanName = obj.ToString();
            string currentList = txtSelectedAudioLanguages.Text.Trim();

            // add to the selected list
            int index = currentList.IndexOf(lanName, 0, StringComparison.InvariantCultureIgnoreCase);
            if (index > -1) return;

            if (currentList.Length > 0) currentList += ", ";
            currentList += lanName;

            txtSelectedAudioLanguages.Text = currentList;
        }

        void lbVideoQuality_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            object obj = lbVideoQuality.SelectedItem;
            if (obj == null) return;

            string vidQual = obj.ToString();
            string currentList = txtSelectedVideoQuality.Text.Trim();

            // add to the selected list
            int index = currentList.IndexOf(vidQual, 0, StringComparison.InvariantCultureIgnoreCase);
            if (index > -1) return;

            if (currentList.Length > 0) currentList += ", ";
            currentList += vidQual;

            txtSelectedVideoQuality.Text = currentList;
        }

        void lbAnimeTypes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            object obj = lbAnimeTypes.SelectedItem;
            if (obj == null) return;

            string aType = obj.ToString();
            string currentList = txtSelectedAnimeTypes.Text.Trim();

            // add to the selected list
            int index = currentList.IndexOf(aType, 0, StringComparison.InvariantCultureIgnoreCase);
            if (index > -1) return;

            if (currentList.Length > 0) currentList += ", ";
            currentList += aType;

            txtSelectedAnimeTypes.Text = currentList;
        }

        void lbTags_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            object obj = lbTags.SelectedItem;
            if (obj == null) return;

            string catName = obj.ToString();
            string currentList = txtSelectedTags.Text.Trim();

            // add to the selected list
            int index = currentList.IndexOf(catName, 0, StringComparison.InvariantCultureIgnoreCase);
            if (index > -1) return;

            if (currentList.Length > 0) currentList += ", ";
            currentList += catName;

            txtSelectedTags.Text = currentList;
        }

        void lbCustomTags_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            object obj = lbCustomTags.SelectedItem;
            if (obj == null) return;


            string tagName = obj.ToString();
            string currentList = txtSelectedCustomTags.Text.Trim();

            // add to the selected list
            int index = currentList.IndexOf(tagName, 0, StringComparison.InvariantCultureIgnoreCase);
            if (index > -1) return;

            if (currentList.Length > 0) currentList += ", ";
            currentList += tagName;

            txtSelectedCustomTags.Text = currentList;
        }
    }
}
