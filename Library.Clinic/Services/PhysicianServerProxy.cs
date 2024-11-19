using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Clinic.Models;


namespace Library.Clinic.Services
{
    public class PhysicianServerProxy
    {

        private static object _lock = new object();
        public static PhysicianServerProxy Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new PhysicianServerProxy();
                    }
                }
                return instance;
            }

        }
        private static PhysicianServerProxy? instance;
        private PhysicianServerProxy()
        {
            instance = null;
            Physicians = new List<Physician>
            {
                new Physician { PhysicianId = 1, Name = "Sean Doe" },
                new Physician { PhysicianId = 2, Name = "S"
                }
            };
        }

        public int LastKey
        {
            get
            {
                if (Physicians.Any())
                {
                    return Physicians.Select(x => x.PhysicianId).Max();
                }
                return 0;
            }
        }
        public List<Physician> physicians;
        public List<Physician> Physicians
        {
            get
            {
                return physicians;
            }
            private set
            {
                physicians = value;
            }
        }


        public void AddorUpdatePhysician(Physician physician)
        {
            bool isAdd = false;
            if (physician.PhysicianId <= 0)
            {
                physician.PhysicianId = LastKey + 1;
                isAdd = true;
            }
            if (isAdd)
            {
                Physicians.Add(physician);
            }
        }
        public void DeletePhysician(int id)
        {
            var physicianToRemove = Physicians.FirstOrDefault(p => p.PhysicianId == id);
            if (physicianToRemove != null)
            {
                Physicians.Remove(physicianToRemove);
            }
        }
    }
}