namespace App.Clinic.Views;

using global::App.Clinic.ViewModels;
using Library.Clinic.Models;
using Library.Clinic.Services;
using System.ComponentModel;

[QueryProperty(nameof(PatId), "patientId")]

public partial class PatientView : ContentPage
{
	public PatientView()
	{
		InitializeComponent();
	}
	public int PatId { get; set; }
	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//Patients");
	}
	private void AddClicked(object sender, EventArgs e)
	{
		(BindingContext as PatientViewModel)?.ExecuteAdd();
	}

    private void PatientView_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        //BindingContext = new Patient();
		if (PatId > 0)
		{
            var model = PatientServerProxy
				.Current.Patients.FirstOrDefault
				(p => p.Id == PatId);
			if (model != null)
			{
				BindingContext = new PatientViewModel(model);
			} else
			{
				BindingContext = new PatientViewModel();
			}
        } else
		{
			BindingContext = new PatientViewModel();
		}
    }

}