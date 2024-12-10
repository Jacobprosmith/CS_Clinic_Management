namespace App.Clinic.Views;

using global::App.Clinic.ViewModels;
using Library.Clinic.Models;
using Library.Clinic.Services;
using System.ComponentModel;

[QueryProperty(nameof(TreatId), "treatmentId")]

public partial class TreatmentView : ContentPage
{
    public TreatmentView()
    {
        InitializeComponent();
    }
    public int TreatId { get; set; }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Treatments");
    }
    private void AddClicked(object sender, EventArgs e)
    {
        (BindingContext as TreatmentViewModel)?.ExecuteAdd();
    }

    private void TreatmentView_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        //BindingContext = new Treatment();
        if (TreatId > 0)
        {
            var model = TreatmentServerProxy
                .Current.Treatments.FirstOrDefault
                (p => p.Id == TreatId);
            if (model != null)
            {
                BindingContext = new TreatmentViewModel(model);
            }
            else
            {
                BindingContext = new TreatmentViewModel();
            }
        }
        else
        {
            BindingContext = new TreatmentViewModel();
        }
    }

}