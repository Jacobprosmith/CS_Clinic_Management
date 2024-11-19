using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Clinic.Models;



namespace Library.Clinic.Services
{
    public class AppointmentServerProxy
    {
        private static object _lock = new object();
        private int lastKey
        {
            get
            {
                if (Appointments.Any())
                {
                    return Appointments.Select(x => x.Id).Max();
                }
                return 0;
            }
        }
        public List<Appointment> Appointments { get; private set; }
        private static AppointmentServerProxy _instance;

        public static AppointmentServerProxy Current
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new AppointmentServerProxy();
                    }
                    return _instance;

                }
                
            }
        }
        private AppointmentServerProxy() {
            Appointments = new List<Appointment>
            {
                new Appointment {Id= 1, StartTime = new DateTime(2024, 10,9)
                ,EndTime = new DateTime(2024, 10,9), PatientId = 1  }
            };
        }
        public void AddOrUpdate(Appointment a)
        {
            var isAdd = false;
            if (a.Id <= 0)
            {
                a.Id = lastKey + 1;
                isAdd = true;
            }
            if (isAdd) {
                Appointments.Add(a);
            }
        }
    }
}
