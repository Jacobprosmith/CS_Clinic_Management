using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Models
{
    public class Physician
    {
        public override string ToString()
        {
            return Display;
        }
        public string Display
        {
            get
            {
                return $"[{PhysicianId}] {Name}"; 
            }
        }
        public string? name;

        public string Name
        {
            get
            {
                return name ?? string.Empty;
            }
            set
            {
                name = value;
            }
        }
        public string Specializations { get; set; }
        public string LicenseNumber { get; set; }
        public int PhysicianId { get; set; }
        public DateTime GraduationDate { get; set; }
        public Physician() { 
            Name = string.Empty;
            Specializations = string.Empty;
            LicenseNumber = string.Empty;
            GraduationDate = DateTime.MinValue;
        }

    }
}
