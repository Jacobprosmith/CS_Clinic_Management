using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Services
{
    public class PatientServerProxy
    {
        private static object _lock = new object();
        public static PatientServerProxy Current
        {
            get
            {
                lock(_lock)
                { 
                    if (instance == null)
                    {
                        instance = new PatientServerProxy();
                    }
                }
                return instance;
            }
        }

        private static PatientServerProxy? instance;
        private PatientServerProxy()
        {
            instance = null;
            Patients = new List<Patient>
            {
                new Patient{Id = 1, Name = "John Doe"}
                ,new Patient{Id = 2, Name = "Jane Doe" }
            }; 
        }
       
        public int LastKey
        {
            get
            {
                if (Patients.Any())
                {
                    return Patients.Select(x => x.Id).Max();
                }
                return 0;
            }
        }
        private List<Patient> patients;
        public List<Patient> Patients
        {
            get
            {
                return patients;
            }
            private set
            {
                //if (patients != null)
                //{
                patients = value;
                //}

            }
        }

        public void AddorUpdatePatient(Patient patient)
        {
            bool isAdd = false;
            if (patient.Id <= 0)
            {
                patient.Id = LastKey + 1;
                isAdd = true;
            }
            if (isAdd)
            {
                Patients.Add(patient);
            }
        }
        public void DeletePatient(int id) {
            var patientToRemove = Patients.FirstOrDefault(p => p.Id== id);
            if (patientToRemove != null)
            {
                Patients.Remove(patientToRemove);
            }
        }
       
    }
}