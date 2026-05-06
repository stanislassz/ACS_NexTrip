using System.Collections.ObjectModel;
using ACS_NexTrip.Models;
using ACS_NexTrip.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ACS_NexTrip.ViewModel
{
    public partial class LieuViewModel : ObservableObject
    {
        // 1. Le lien vers ton service de base de données
        private readonly ConnexionBD _db;

        // 2. La collection qui contient tes lieux (utilisée par la CollectionView)
        // Le Toolkit génère la propriété publique "Lieux"
        [ObservableProperty]
        private ObservableCollection<Lieu> _lieux = new();

        public LieuViewModel(ConnexionBD db)
        {
            _db = db;
        }

        // --- NAVIGATION ---

        [RelayCommand]
        private async Task GoToAddLieu()
        {
            // Navigue vers ta page d'ajout
            await Shell.Current.GoToAsync("AddLieuPage");
        }

        // --- ACTIONS ---

        // Cette méthode est appelée par l'événement OnAppearing de ta page XAML
        [RelayCommand]
        private async Task Appearing()
        {
            await GetLieux();
        }

        [RelayCommand]
        private async Task GetLieux()
        {
            // Appel de ta méthode GetLieuxAsync() que tu m'as montrée
            var data = await _db.GetLieuxAsync();

            // On vide la liste actuelle pour ne pas avoir de doublons
            Lieux.Clear();

            // On remplit avec les données fraîches de la BDD
            foreach (var l in data)
            {
                Lieux.Add(l);
            }
        }

        [RelayCommand]
        private async Task DeleteLieu(Lieu lieu)
        {
            if (lieu == null) return;

            // Demande de confirmation (Optionnel mais conseillé)
            bool answer = await App.Current.MainPage.DisplayAlert("Suppression",
                $"Voulez-vous vraiment supprimer {lieu.LIE_LIBELLE} ?", "Oui", "Non");

            if (!answer) return;

            // Appel au service pour supprimer en base (ps_DeleteLieu)
            bool success = await _db.DeleteLieuAsync(lieu.LIE_ID);

            if (success)
            {
                // Si SQL a réussi, on l'enlève de la liste visuelle
                Lieux.Remove(lieu);
            }
        }
    }
}