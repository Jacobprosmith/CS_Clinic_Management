﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Library.Clinic.Models;
using Library.Clinic.Services;

namespace App.Clinic.ViewModels
{
    public class PatientViewModel
    {
        public Patient? Model { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }

        public int PatientId
        {
            get
            {
                if (Model == null)
                {
                    return -1;
                }

                return Model.Id;
            }
            set
            {
                if (Model != null && Model.Id != value)
                {
                    Model.Id = value;
                }
                Model.Id = value;
            }
        }
        public string Name
        {
            get => Model?.Name ?? string.Empty;


            set
            {
                if (Model != null)
                {
                    Model.Name = value;
                }

            }
        }
        public void SetupCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as PatientViewModel));
        }
        private void DoDelete()
        {
            if (PatientId > 0)
            {
                PatientServerProxy.Current.DeletePatient(PatientId);
            }
        }

        private void DoEdit(PatientViewModel? pvm)
        {
            if (pvm == null)
            {
                return;
            }
            var selectedPatientId = pvm?.PatientId ?? 0;
            Shell.Current.GoToAsync($"//PatientDetails?patientId={selectedPatientId}");
        }
        public PatientViewModel() 
        {
            Model = new Patient();
            SetupCommands();
        }
        public PatientViewModel(Patient? _model)
        {
            Model = _model;
            SetupCommands();
        }

        public void ExecuteAdd()
        {
            if (Model != null)
            {
                PatientServerProxy.Current.AddorUpdatePatient(Model);
            }
            Shell.Current.GoToAsync("//Patients");
        }
    }
}
