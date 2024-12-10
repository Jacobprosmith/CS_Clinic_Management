using Library.Clinic.Models;
using Library.Clinic.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using App.Clinic.ViewModels;

namespace App.Clinic.Views;

public partial class TreatmentManagement : ContentPage, INotifyPropertyChanged
{

    public TreatmentManagement()
    {
        InitializeComponent();
        BindingContext = new TreatmentManagementViewModel();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as TreatmentManagementViewModel)?.Delete();
    }
    private void ViewTreatments(object sender, EventArgs e)
    {

    }
    private void AddTreatment(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//TreatmentDetails?treatmentId=0");
    }
    private void EditTreatment(object sender, EventArgs e)
    {
        var selectedTreatmentId = (BindingContext as TreatmentManagementViewModel)?
            .SelectedTreatment?.TreatmentId ?? 0;
        Shell.Current.GoToAsync($"//TreatmentDetails?treatmentId={selectedTreatmentId}");
    }
    private void TreatmentManagement_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as TreatmentManagementViewModel)?.Refresh();
    }

    private void RefreshClicked(object sender, EventArgs e)
    {
        (BindingContext as TreatmentManagementViewModel)?.Refresh();
    }

}