using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Library.Clinic.Models;
using Library.Clinic.Services;

namespace App.Clinic.ViewModels
{
    public class AppointmentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        //Create an object Model that is a rep of Appointment.cs(the real class)
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Appointment? Model { get; set; }
        public string PatientName
        {
            get
            {
                if (Model != null && Model.PatientId > 0)
                {
                    if (Model.Patient == null)
                    {
                        Model.Patient = PatientServerProxy.Current.Patients.FirstOrDefault(p => p.Id == Model.PatientId);
                    }
                }
                return Model?.Patient?.Name ?? string.Empty;
            }
        }

        public Patient? SelectedPatient {  get
            {
                return Model?.Patient;
            }
            set
            {
                var selectedPatient = value;
                if (Model != null)
                {
                    Model.Patient = selectedPatient;
                    Model.PatientId = selectedPatient?.Id ?? 0;
                }
            }
        }
        public decimal BasePrice
        {
            get => Model?.BasePrice ?? 0.0m;
            set
            {
                if (Model != null && Model.BasePrice != value)
                {
                    Model.BasePrice = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(FinalPrice)); // Update FinalPrice when BasePrice changes
                }
            }
        }

        public ObservableCollection<Treatment> Treatments
        {
            get => new ObservableCollection<Treatment>(TreatmentServerProxy.Current.Treatments);
        }

        private Treatment? selectedTreatment;
        public Treatment? SelectedTreatment
        {
            get => Model?.Treatment;
            set
            {
                if (Model != null && Model.Treatment != value)
                {
                    selectedTreatment = value;
                    Model.Treatment = value;
                    Model.BasePrice = selectedTreatment?.Price ?? 0.0m;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(FinalPrice)); // Recalculate FinalPrice
                    NotifyPropertyChanged(nameof(TreatmentName)); // Notify TreatmentName change
                }
            }
        }
        public string TreatmentName
        {
            get
            {
                if (Model?.Treatment != null)
                {
                    return Model.Treatment.Name;
                }
                return string.Empty;
            }
        }

        public decimal FinalPrice => Model?.FinalPrice ?? 0.0m;

        public ObservableCollection<Patient> Patients
        {
            get
            {
                return new ObservableCollection<Patient>(PatientServerProxy.Current.Patients);
            }
        }

        public AppointmentViewModel()
        {
            Model = new Appointment();
        }
        public AppointmentViewModel(Appointment a)
        {
            Model = a;
        }

        public DateTime DateTime { get; set; }

        public TimeSpan TimeSpan { get
            {
                return new TimeSpan(DateTime.Hour, DateTime.Minute, DateTime.Second);
            }
            set
            {
                DateTime = DateTime.Date;
                DateTime = DateTime.Add(value);
            }
        }
        public void RefreshTime()
        {
            if (Model != null)
            {
                if (Model.StartTime != null)
                {
                    Model.StartTime = StartDate;
                    Model.StartTime = Model.StartTime.Value.AddHours(StartTime.Hours);
                }
                if (Model.EndTime != null)
                {
                    Model.EndTime = EndDate;
                    Model.EndTime = Model.EndTime.Value.AddHours(EndTime.Hours);
                }
            }
        }


        public DateTime StartDate
        {
            get => Model?.StartTime?.Date ?? DateTime.Today;
            set
            {
                if (Model != null)
                {
                    Model.StartTime = value.Date.Add(StartTime); // Combine Date and Time
                    Model.EndTime = Model.StartTime?.AddHours(2); // Set EndTime 2 hours later
                    NotifyPropertyChanged(); // Notify StartDate change
                    NotifyPropertyChanged(nameof(EndDate)); // Notify EndDate change
                    NotifyPropertyChanged(nameof(EndTime)); // Notify EndTime change
                }
            }
        }

        private TimeSpan startTime;
        public TimeSpan StartTime
        {
            get => Model?.StartTime?.TimeOfDay ?? TimeSpan.Zero; // Read from Model
            set
            {
                if (Model != null && Model.StartTime?.TimeOfDay != value)
                {
                    Model.StartTime = StartDate.Date.Add(value); // Update Model
                    NotifyPropertyChanged(nameof(StartTime));    // Notify change
                    NotifyPropertyChanged(nameof(EndTime));      // Ensure EndTime updates
                }
            }
        }

        public DateTime EndDate
        {
            get => Model?.EndTime?.Date ?? StartDate.AddHours(2);
            set
            {
                if (Model != null)
                {
                    Model.EndTime = value.Date.Add(EndTime); // Combine Date and Time
                    NotifyPropertyChanged(); // Notify EndDate change
                }
            }
        }

        public TimeSpan EndTime
        {
            get => Model?.EndTime?.TimeOfDay ?? StartTime.Add(new TimeSpan(2, 0, 0));
            set
            {
                if (Model != null)
                {
                    Model.EndTime = StartDate.Date.Add(value);
                    NotifyPropertyChanged(); // Notify EndTime change
                }
            }
        }


        public void AddOrUpdate()
        {
            if (Model != null)
            {
                AppointmentServerProxy.Current.AddOrUpdate(Model);
            }

        }

    }
}