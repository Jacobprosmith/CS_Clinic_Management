namespace App.Clinic.Views;

using global::App.Clinic.ViewModels;
using Library.Clinic.Models;
using Library.Clinic.Services;
using System.ComponentModel;

//[QueryProperty(nameof())]

public partial class AppointmentView : ContentPage
{
    public AppointmentView()
    {
        InitializeComponent();
        // Binds to Appointmentviewmodel.cs here 
        BindingContext = new AppointmentViewModel();
    }
    public int AppId { get; set; }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Appointments");
    }
    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as AppointmentViewModel)?.AddOrUpdate();
        Shell.Current.GoToAsync("//Appointments");
    }
    private void TimePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        (BindingContext as AppointmentViewModel)?.RefreshTime();
    }

    private void AppointmentView_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new AppointmentViewModel();

        /*
        if (Patid > 0)
        {
            var model = PatientServerProxy
                .Current.Patients.FirstOrDefault
                (p => p.Id == PatId);
            if (model != null)
            {
                BindingContext = new PatientViewModel(model);
            }
            else
            {
                BindingContext = new PatientViewModel();
            }
        }
        else
        {
            BindingContext = new PatientViewModel();
        }
        */
    }

}