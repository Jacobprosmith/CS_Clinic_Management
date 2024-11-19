using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Clinic.Models;
using Library.Clinic.Services;

namespace App.Clinic.ViewModels
{
    public class AppointmentViewModel
    {

        //Create an object Model that is a rep of Appointment.cs(the real class)
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

        public DateTime MinStartDate
        {
            get
            {
                return DateTime.Today;
            }
        }
        public DateTime MaxStartDate
        {
            get
            {
                var maxTime = StartDate;
                return maxTime.AddHours(2);
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

            get
            {
                return Model?.StartTime?.Date ?? DateTime.Today;
            }

            set
            {
                if (Model != null)
                {
                    Model.StartTime = value;
                    Model.EndTime = value;
                    RefreshTime();
                }
            }
        }
        private TimeSpan startTime;
        public TimeSpan StartTime { 
            get
            {

                return startTime;
            } 
            set {
                startTime = value;
                EndTime = startTime;
                EndTime.Add(new TimeSpan(1, 0, 0));
            }
        }
        public DateTime EndDate
        {
            get
            {
                return StartDate.AddHours(1);
            }
            set
            {
                
            }
        }
        public TimeSpan EndTime { get; set; }

        public void AddOrUpdate()
        {
            if (Model != null)
            {
                AppointmentServerProxy.Current.AddOrUpdate(Model);
            }

        }

    }
}