using Library.Clinic.Models;
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
    public class PhysicianManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public PhysicianViewModel? SelectedPhysician { get; set; }
        public ObservableCollection<PhysicianViewModel> Physicians
        //public List<Physician> Physicians
        {
            get
            {
                return new ObservableCollection<PhysicianViewModel>(
                    PhysicianServerProxy.Current.Physicians
                    .Where(p => p != null)
                    .Select(p => new PhysicianViewModel(p)));


            }
        }
        public void Delete()
        {
            if (SelectedPhysician == null)
            {
                return;
            }
            PhysicianServerProxy.Current.DeletePhysician(SelectedPhysician.PhysicianId);
            Refresh();

        }
        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Physicians));
        }
    }
}
