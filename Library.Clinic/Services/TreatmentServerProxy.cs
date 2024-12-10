using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Clinic.Models;



namespace Library.Clinic.Services
{
    public class TreatmentServerProxy
    {
        private static object _lock = new object();
        public static TreatmentServerProxy Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new TreatmentServerProxy();
                    }
                }
                return instance;
            }

        }
        private static TreatmentServerProxy? instance;
        private TreatmentServerProxy()
        {
            instance = null;
            Treatments = new List<Treatment>
            {
                new Treatment { Id = 1, Name = "Physical Body check", Price = 100 },
                new Treatment { Id = 2, Name = "MRI", Price = 1000 
                }
            };
        }

        public int LastKey
        {
            get
            {
                if (Treatments.Any())
                {
                    return Treatments.Select(x => x.Id).Max();
                }
                return 0;
            }
        }
        public List<Treatment> treatments;
        public List<Treatment> Treatments
        {
            get
            {
                return treatments;
            }
            private set
            {
                treatments = value;
            }
        }


        public void AddorUpdateTreatment(Treatment treatment)
        {
            bool isAdd = false;
            if (treatment.Id <= 0)
            {
                treatment.Id = LastKey + 1;
                isAdd = true;
            }
            if (isAdd)
            {
                Treatments.Add(treatment);
            }
        }
        public void DeleteTreatment(int id)
        {
            var treatmentToRemove = Treatments.FirstOrDefault(p => p.Id == id);
            if (treatmentToRemove != null)
            {
                Treatments.Remove(treatmentToRemove);
            }
        }
    }

    }