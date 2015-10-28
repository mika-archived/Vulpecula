﻿using System.Collections.Generic;
using System.Linq;

using Vulpecula.Models;
using Vulpecula.Universal.Models;
using Vulpecula.Universal.Models.Services;
using Vulpecula.Universal.ViewModels.Timelines.Primitives;

// ReSharper disable PossibleMultipleEnumeration

namespace Vulpecula.Universal.ViewModels
{
    public class UserAccountViewModel : UserViewModel
    {
        private readonly CroudiaProvider _provider;

        private UserAccountViewModel(User user, CroudiaProvider provider) : base(user)
        {
            this._provider = provider;
            this.IsWhisperEnabled = false;
        }

        public static UserAccountViewModel Create(User user)
        {
            if (AccountManager.Instance.Providers.All(w => w.User.Id != user.Id))
                throw new KeyNotFoundException($"UserId:{user.Id} is not found in users that loading.");
            return new UserAccountViewModel(user, AccountManager.Instance.Providers.Single(w => w.User.Id == user.Id));
        }

        public void SendWhisper(string text)
        {
            ServiceProvider.RegisterService(new StatusService(this._provider, text));
        }

        #region IsWhisperEnabled

        private bool _isWhisperEnabled;

        public bool IsWhisperEnabled
        {
            get { return this._isWhisperEnabled; }
            set { this.SetProperty(ref this._isWhisperEnabled, value); }
        }

        #endregion
    }
}