using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ACS_NexTrip.Models;
using ACS_NexTrip.Services;

namespace ACS_NexTrip.ViewModel
{
    [QueryProperty(nameof(LieuAModifier), "LieuEchange")] // On reçoit l'objet
    public partial class EditLieuViewModel : ObservableObject
    {
        private readonly ConnexionBD _db;
        public EditLieuViewModel(ConnexionBD db) => _db = db;

        [ObservableProperty]
        private Lieu _lieuAModifier;

        [ObservableProperty]
        private string _libelle;

        // Quand l'objet arrive, on remplit le champ texte
        partial void OnLieuAModifierChanged(Lieu value)
        {
            if (value != null) Libelle = value.LIE_LIBELLE;
        }

        [RelayCommand]  
        private async Task Update()
        {
            if (string.IsNullOrWhiteSpace(Libelle)) return;

            LieuAModifier.LIE_LIBELLE = Libelle; // On met à jour le nom

            if (await _db.UpdateLieuAsync(LieuAModifier)) // Appel à ta méthode UPDATE
            {
                await Shell.Current.GoToAsync("..");
            }
        }

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
