using System;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JamCaturOffline.Models;

namespace JamCaturOffline.ViewModels
{
    [QueryProperty(nameof(Durasi),nameof(Durasi))]
	public partial class JamVM : ObservableObject
    {
        #region constructor
        #endregion


        #region properties
        [ObservableProperty]
        DurasiModel _durasi;
        partial void OnDurasiChanged(DurasiModel value)
        {
            RemainingWhite = TimeSpan.FromMinutes(value.Menit);
            RemainingBlack = TimeSpan.FromMinutes(value.Menit);

            BlackIncrement = TimeSpan.FromSeconds(value.Increment);
            WhiteIncrement = TimeSpan.FromSeconds(value.Increment);
        }

        [ObservableProperty]
        int _moveCount;

        [ObservableProperty]
        TimeSpan _whiteIncrement;

        [ObservableProperty]
        TimeSpan _blackIncrement;

        [ObservableProperty]
        TimeSpan _remainingWhite;

        [ObservableProperty]
        TimeSpan _remainingBlack;

        // pause,stop,continues button

        [ObservableProperty]
        bool _pause;

        [ObservableProperty]
        bool _stop;

        [ObservableProperty]
        bool _whitePause;

        [ObservableProperty]
        bool _blackPause;

        [ObservableProperty]
        bool _btnStartVisible = true;

        // [ObservableProperty]
        // bool _pauseVisible;

        [ObservableProperty]
        bool _btnContinuesVisibled = false;

        [ObservableProperty]
        bool _btnStartEnabled = true;

        [ObservableProperty]
        bool _btnPauseEnabled = false;

        [ObservableProperty]
        bool _btnStopEnabled = false;

        [ObservableProperty]
        bool _resetBtnEnabled = true;


        //button jam

        [ObservableProperty]
        bool _btnWhiteEnabled;

        [ObservableProperty]
        bool _btnBlackEnabled;


        #endregion


        #region command
        [RelayCommand]
        void Start()
        {
            BtnStartEnabled = false;
            BtnStartVisible = false;
            BtnPauseEnabled = true;
            BtnStopEnabled = true;

            BlackPause = true;
            BtnWhiteEnabled = true;
            new Thread(async () =>
            {
                await WhiteTimeProgress();
            }).Start();

            new Thread(async () =>
            {
                await BlackTimeProgress();
            }).Start();
        }

        [RelayCommand]
        void PauseProgress()
        {
            Pause = true;
            BtnStartVisible = false;
            BtnPauseEnabled = false;
            BtnStopEnabled = true;
            BtnContinuesVisibled = true;


            BtnBlackEnabled = false;
            BtnWhiteEnabled = false;
            ResetBtnEnabled = true;
        }

        [RelayCommand]
        void Continues()
        {
            Pause = false;

            BtnContinuesVisibled = false;
            BtnPauseEnabled = true;
            BtnStopEnabled = true;



            // aktifkan ulang tombol berdasarkan siapa yang jalan
            if (WhitePause)
            {
                BtnBlackEnabled = true;
                BtnWhiteEnabled = false;
            }
            else
            {
                BtnBlackEnabled = false;
                BtnWhiteEnabled = true;
            }

            ResetBtnEnabled = false;
        }

        [RelayCommand]
        async void StopProgress()
        {
            Pause = true;
            var confirmasi = await App.Current.MainPage.DisplayAlert("Stop Timer?", "Are you want to stop timer?", "OK", "Cancel");
            if (confirmasi)
            {
                BtnPauseEnabled = false;
                BtnStartEnabled = false;
                BtnContinuesVisibled = false;
                ResetBtnEnabled = true;
                Pause = true;
                Stop = true;

            }
            else
            {
                Pause = false;
            }

        }


        [RelayCommand]
        async void Reset()
        {
            try
            {
                Pause = true;

                var confirmasi = await App.Current.MainPage.DisplayAlert("Stop Timer?", "Are you want to reset timer?", "OK", "Cancel");

                if (confirmasi)
                {
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    Pause = false;
                }

            }
            catch (Exception ex)
            {
                await Toast.Make(ex.Message).Show();
            }
        }

        [RelayCommand]
        void TimeWhiteClicked()
        {

            WhitePause = true;
            BlackPause = false;
            BtnWhiteEnabled = false;
            BtnBlackEnabled = true;
            RemainingWhite += WhiteIncrement;

            MoveCount += 1;
        }

        [RelayCommand]
        void TimeBlackClicked()
        {
            WhitePause = false;
            BlackPause = true;
            BtnWhiteEnabled = true;
            BtnBlackEnabled = false;
            RemainingBlack += BlackIncrement;
        }


        #endregion

        #region customcommand

        async Task WhiteTimeProgress()
        {
            while (RemainingWhite > TimeSpan.Zero || Stop)
            {
                while (WhitePause)
                {
                    await Task.Delay(1000);
                }

                while (Pause)
                {
                    await Task.Delay(1000);
                }


                RemainingWhite -= TimeSpan.FromSeconds(1);
                await Task.Delay(1000);
                StopTime();

            }
        }

        async Task BlackTimeProgress()
        {
            while (RemainingBlack > TimeSpan.Zero || Stop)
            {
                while (BlackPause)
                {
                    await Task.Delay(1000);
                }

                while (Pause)
                {
                    await Task.Delay(1000);
                }

                RemainingBlack -= TimeSpan.FromSeconds(1);
                await Task.Delay(1000);
                StopTime();


            }
        }

        void StopTime()
        {
            if (RemainingWhite <= TimeSpan.Zero || RemainingBlack <= TimeSpan.Zero)
            {
                ResetBtnEnabled = true;
                BtnBlackEnabled = false;
                BtnWhiteEnabled = false;
                //PauseVisible = false;
                BtnPauseEnabled = false;
                BtnStopEnabled = false;
                BtnStartEnabled = true;
            }
        }
        #endregion
    }
}

