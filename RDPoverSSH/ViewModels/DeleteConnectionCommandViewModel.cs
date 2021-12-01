﻿using System.Windows;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using ModernWpf.Controls;
using RDPoverSSH.Controls;
using RDPoverSSH.DataStore;
using RDPoverSSH.Models;
using RDPoverSSH.Properties;

namespace RDPoverSSH.ViewModels
{
    public class DeleteConnectionCommandViewModel : CommandViewModelBase
    {
        #region CommandViewModelBase

        /// <inheritdoc/>
        public override string Name => string.Empty;

        /// <inheritdoc/>
        public override string Description => Resources.DeleteConnectionCommanDescription;

        /// <inheritdoc/>
        public override ICommand Command => _command ??= new RelayCommand<object>(DeleteConnectionItem);
        private ICommand _command;

        /// <inheritdoc/>
        public override string IconGlyph => "\xE74D";

        #endregion

        #region Command

        private async void DeleteConnectionItem(object param)
        {
            if (param is ConnectionViewModel connectionViewModel)
            {
                string connectionNameFormat = string.IsNullOrWhiteSpace(connectionViewModel.Model.Name)
                    ? string.Empty
                    : $" '{connectionViewModel.Model.Name}'";

                var res = await MessageBoxHelper.Show(string.Format(Resources.ConfirmDeleteConnection, connectionNameFormat), Resources.Confirm, MessageBoxButton.YesNo);
                if (res == ContentDialogResult.Primary)
                {
                    RootModel.Instance.Connections.Remove(connectionViewModel.Model);
                    DatabaseEngine.GetCollection<ConnectionModel>().Delete(connectionViewModel.Model.ObjectId);
                }
            }
        }

        #endregion
    }
}
