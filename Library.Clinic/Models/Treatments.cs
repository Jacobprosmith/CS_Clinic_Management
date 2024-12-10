using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Models
{
    public class Treatment
    {
        public override string ToString()
        {
            return Display;
        }

        public string Display
        {
            get
            {
                return $"{Name} - ${Price:F2}";
            }
        }
        public int Id { get; set; }
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
        public decimal Price { get; set; }
        public Treatment() {
            Price = 0.0m;
            Name = string.Empty;
        }
    }
}
