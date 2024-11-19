using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Clinic.Models;

namespace App.Clinic.ViewModels
{
    public class PhysicianViewModel
    {
        private Physician? model { get; set; }

        public int PhysicianId
        {
            get
            {
                if (model == null)
                {
                    return -1;
                }

                return model.PhysicianId;
            }
            set
            {
                if (model != null && model.PhysicianId != value)
                {
                    model.PhysicianId = value;
                }
                model.PhysicianId = value;
            }
        }
        public string Name
        {
            get => model?.Name ?? string.Empty;


            set
            {
                if (model != null)
                {
                    model.Name = value;
                }

            }
        }
        public PhysicianViewModel()
        {
            model = new Physician();
        }
        public PhysicianViewModel(Physician? _model)
        {
            model = _model;
        }
    }
}
