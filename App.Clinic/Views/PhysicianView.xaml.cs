namespace App.Clinic.Views;
using Library.Clinic.Models;
using Library.Clinic.Services;
using System.ComponentModel;

[QueryProperty(nameof(PhysId), "physicianId")]

public partial class PhysicianView : ContentPage
{
    public PhysicianView()
    {
        InitializeComponent();
    }
    public int PhysId { get; set; }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Physicians");
    }
    private void AddClicked(object sender, EventArgs e)
    {
        var physicianToAdd = BindingContext as Physician;
        if (physicianToAdd != null)
        {
            PhysicianServerProxy.Current.AddorUpdatePhysician(physicianToAdd);
        }
        Shell.Current.GoToAsync("//Physicians");
    }

    private void PhysicianView_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        //BindingContext = new Physician();
        if (PhysId > 0)
        {
            BindingContext = PhysicianServerProxy.Current.Physicians.FirstOrDefault(p => p.PhysicianId == PhysId); // My PhysicianId from physician class adn then PhysId from up above
        }
        else
        {
            BindingContext = new Physician();
        }
    }

}