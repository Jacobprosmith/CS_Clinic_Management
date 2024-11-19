using Library.Clinic.Models;
using Library.Clinic.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using App.Clinic.ViewModels;

namespace App.Clinic.Views;

public partial class PhysicianManagement : ContentPage, INotifyPropertyChanged
{

    public PhysicianManagement()
    {
        InitializeComponent();
        BindingContext = new PhysicianManagementViewModel();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as PhysicianManagementViewModel)?.Delete();
    }
    private void ViewPhysicians(object sender, EventArgs e)
    {

    }
    private void AddPhysician(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//PhysicianDetails?physicianId=0");
    }
    private void EditPhysician(object sender, EventArgs e)
    {
        var selectedPhysicianId = (BindingContext as PhysicianManagementViewModel)?
            .SelectedPhysician?.PhysicianId ?? 0;
        Shell.Current.GoToAsync($"//PhysicianDetails?physicianId={selectedPhysicianId}");
    }
    private void PhysicianManagement_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as PhysicianManagementViewModel)?.Refresh();
    }
}