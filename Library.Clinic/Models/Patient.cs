﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Models
{
    public class Patient
    {
        public override string ToString()
        {
            return Display;
        }

        public string Display
        {
            get
            {
                return $"[{Id}] {Name} - {InsuranceType}";
            }
        }
        private string? name;
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
        public int Id { get; set; } 
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public DateTime Birthday { get; set; }
        public string Description { get; set; }
        public string Diagnoses { get; set; }
        public string InsuranceType { get; set; }

        public Patient()
        {
            Name = string.Empty;
            Address = string.Empty;
            Gender = string.Empty;
            Race = string.Empty;
            Birthday = DateTime.MinValue;
            Description = string.Empty;
            Diagnoses = string.Empty;
            InsuranceType = InsurancePlan.Bad;
        }
    }
}
