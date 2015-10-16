﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Prism.Windows.Mvvm;

using Vulpecula.Universal.Models;
using Vulpecula.Universal.ViewModels.Contents;

namespace Vulpecula.Universal.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public AccountManager AccountManager { get; set; }

        public MainPageViewModel()
        {
            this.Timelines = new ObservableCollection<TimelineViewModel>();
            this.Text = "Hello MVVM on UWP!";
            this.IsHamburgerChecked = false;
            this.AccountManager = new AccountManager();
        }

        private async Task Initialize()
        {
            await this.AccountManager.InitializeAccounts();
        }

        private async Task Authorization()
        {
            await this.AccountManager.AuthorizationAccount();
        }

        #region Properties

        public ObservableCollection<TimelineViewModel> Timelines { get; }

        #region Text

        private string _text;

        public string Text
        {
            get { return this._text; }
            set { this.SetProperty(ref this._text, value); }
        }

        #endregion

        #region IsHamburgerChecked

        private bool _isHamburgerChecked;

        public bool IsHamburgerChecked
        {
            get { return this._isHamburgerChecked; }
            set { this.SetProperty(ref this._isHamburgerChecked, value); }
        }

        #endregion

        #endregion

        #region Events

        // TODO: Remove Code-behind
        public async void OnLoaded(object sender, RoutedEventArgs e) => await this.Initialize();

        public void OnChecked(object sender, RoutedEventArgs e) => this.IsHamburgerChecked = true;

        public void OnUnchecked(object sender, RoutedEventArgs e) => this.IsHamburgerChecked = false;

        public void PaneClosing(object sender, SplitViewPaneClosingEventArgs e) => this.IsHamburgerChecked = false;

        public async void OnTapped(object sender, RoutedEventArgs e) => await this.Authorization();

        public void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
            => ((ListBox) sender).SelectedIndex = -1;

        #endregion
    }
}