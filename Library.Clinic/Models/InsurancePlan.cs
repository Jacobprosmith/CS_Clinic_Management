using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Models
{
    public static class InsurancePlan
    {
        public const string Amazing = "Amazing";
        public const string Good = "Good";
        public const string Bad = "Bad";

        // Provide a list of all insurance options
        public static readonly List<string> Values = new List<string> { Amazing, Good, Bad };
        public static decimal GetDiscount(string plan)
        {
            return plan switch
            {
                Amazing => 0.50m, // 50% discount
                Good => 0.25m,    // 25% discount
                Bad => 0.10m,     // 10% discount
                _ => 0.0m         // Default to no discount
            };
        }
    }
}