using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Alerts;
using JamCaturOffline.Models;
using Plugin.MauiMTAdmob;

namespace JamCaturOffline.ViewModels
{
    public partial class HomePageVM : ObservableObject
    {


        #region properties
        [ObservableProperty]
        int _minutes = 10;
        partial void OnMinutesChanged(int value)
        {
            if (Minutes > 60)
            {
                Minutes = 60;
            }

            if (Minutes < 1)
            {
                Minutes = 1;
            }
        }



        //[NotifyPropertyChangedFor(nameof(GetSuggestions))]
        [ObservableProperty]
        int _seconds;
        partial void OnSecondsChanged(int value)
        {
            if (Seconds > 30)
            {
                Seconds = 30;
            }

            if (Seconds < 1)
            {
                Seconds = 0;
            }
        }

        #endregion

        #region command


        [RelayCommand]
        async void Start()
        {
            try
            {
                var durasi = new DurasiModel
                {
                    Menit = Minutes,
                    Increment = Seconds
                };
                await Shell.Current.GoToAsync($"{nameof(Views.Jam)}",new Dictionary<string, object>
                {
                    ["Durasi"] = durasi
                });


            }
            catch (Exception ex)
            {
                await Toast.Make(ex.Message).Show();
            }
        }


        #endregion
    }
}

