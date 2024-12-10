using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Library.Clinic.Models
{
    public class Appointment
    {
        //public Appointment() { }
        public Appointment()
        {
            Patient = new Patient();
            BasePrice = 0.0m;
        }
        public int Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
        public decimal BasePrice { get; set; }
        public Treatment? Treatment { get; set; }
        public decimal FinalPrice
        {
            get
            {
                if (Patient == null || string.IsNullOrEmpty(Patient.InsuranceType))
                {
                    return BasePrice; // No patient or insurance, full price
                }

                // Get the discount from the InsurancePlan class
                decimal discountPercentage = InsurancePlan.GetDiscount(Patient.InsuranceType);

                // Calculate and return the final price
                return BasePrice * (1 - discountPercentage);
            }
        }
    }
}