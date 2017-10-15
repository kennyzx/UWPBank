﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPBank
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MediaPage : Page
    {
        MediaCapture mediaCapture;
        public MediaPage()
        {
            this.InitializeComponent();
        }

        private async System.Threading.Tasks.Task ReInitCamera()
        {
            mediaCapture = new MediaCapture();
            await mediaCapture.InitializeAsync();
            previewElement.Source = mediaCapture;
            await mediaCapture.StartPreviewAsync();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ReInitCamera();
            Window.Current.Activated += Current_Activated;
        }

        protected override async void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            await mediaCapture.StopPreviewAsync();
            mediaCapture = null;
            Window.Current.Activated -= Current_Activated;
        }

        private async void Current_Activated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            await ReInitCamera();
        }
    }
}
