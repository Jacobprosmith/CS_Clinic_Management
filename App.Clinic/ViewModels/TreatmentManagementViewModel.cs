﻿using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace App.Clinic.ViewModels
{
    public class TreatmentManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public TreatmentViewModel? SelectedTreatment { get; set; }
        public string? Query { get; set; }
        public ObservableCollection<TreatmentViewModel> Treatments
        //public List<Treatment> Treatments
        {
            get
            {
                var retval = new ObservableCollection<TreatmentViewModel>(
                    TreatmentServerProxy.Current.Treatments.Take(100)
                    .Where(p => p != null)
                    .Where(p => p.Name.ToUpper().Contains(Query?.ToUpper() ?? string.Empty))
                    .Select(p => new TreatmentViewModel(p)));
                return retval;
            }
        }
        public void Delete()
        {
            if (SelectedTreatment == null)
            {
                return;
            }
            TreatmentServerProxy.Current.DeleteTreatment(SelectedTreatment.TreatmentId);
            Refresh();

        }
        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Treatments));
        }
    }
}