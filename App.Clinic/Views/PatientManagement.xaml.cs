using Library.Clinic.Models;
using Library.Clinic.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using App.Clinic.ViewModels;

namespace App.Clinic.Views;

public partial class PatientManagement : ContentPage, INotifyPropertyChanged
{
    
	public PatientManagement()
	{
		InitializeComponent();
        BindingContext = new PatientManagementViewModel();
	}

    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as PatientManagementViewModel)?.Delete();
    }
    private void ViewPatients(object sender, EventArgs e)
    {

    }
    private void AddPatient(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//PatientDetails?patientId=0");
    }
    private void EditPatient(object sender, EventArgs e)
    {
        var selectedPatientId = (BindingContext as PatientManagementViewModel)?
            .SelectedPatient?.PatientId ?? 0;
        Shell.Current.GoToAsync($"//PatientDetails?patientId={selectedPatientId}");
    }
    private void PatientManagement_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as PatientManagementViewModel)?.Refresh();
    }

    private void RefreshClicked(object sender, EventArgs e)
    {
        (BindingContext as PatientManagementViewModel)?.Refresh();
    }

}