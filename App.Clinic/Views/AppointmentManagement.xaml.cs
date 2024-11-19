using Library.Clinic.Models;
using Library.Clinic.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using App.Clinic.ViewModels;

namespace App.Clinic.Views;

public partial class AppointmentManagement : ContentPage, INotifyPropertyChanged
{
	public AppointmentManagement()
	{
		InitializeComponent();
        BindingContext = new AppointmentManagementViewModel();
	}
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void DeleteClicked(object sender, EventArgs e)
    {

    }
    private void AddAppointment(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//AppointmentDetails");
    }
    private void EditAppointment(object sender, EventArgs e)
    {
        var selectedAppointmentId = (BindingContext as AppointmentManagementViewModel)?
            .SelectedAppointment?.Model?.Id ?? 0;
    }
    private void AppointmentManagement_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as AppointmentManagementViewModel)?.Refresh();

    }
    private void ViewAppointments(object sender, EventArgs e)
    {

    }

    
    private void AddClicked(object sender, EventArgs e)
    {

        Shell.Current.GoToAsync("//AppointmentDetails");
    }

    private void RefreshClicked(object sender, EventArgs e)
    {
        (BindingContext as AppointmentManagementViewModel)?.Refresh();
    }
}