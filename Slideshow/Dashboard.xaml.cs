﻿using NaturalSort.Extension;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Slideshow
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        private VisualImageHandler visualImageHandler;

        // selected image
        public Dashboard(string fullPath)
        {
            InitializeComponent();

            visualImageHandler = new VisualImageHandler(Content, fullPath);

            // add eventhandler
            visualImageHandler.Changed += new VisualEventHandler(OnChange);
            OnChange(); // Call to get initial values
        }

        private void OnChange()
        {
            if (visualImageHandler.Files.Length > 0)
            {
                Amount.Dispatcher.Invoke(() =>
                {
                    Amount.Content = (visualImageHandler.CurrentFile + 1) + " / " + visualImageHandler.Files.Length;
                    ProgressFull.Width = new GridLength(visualImageHandler.CurrentFile, GridUnitType.Star);
                    ProgressEmpty.Width = new GridLength(visualImageHandler.Files.Length - visualImageHandler.CurrentFile - 1, GridUnitType.Star);
                });
            }
        }

        private void Shortcuts(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                visualImageHandler.NextImage();
            }
            if (e.Key == Key.Left)
            {
                visualImageHandler.PrevImage();
            }
            if (e.Key == Key.Escape)
            {
                Close();
            }
            if (e.Key == Key.Enter)
            {
                new Fullscreen(visualImageHandler.Files[visualImageHandler.CurrentFile]).Show();
                Close();
            }
            if (e.Key == Key.Delete)
            {
                try
                {
                    File.Delete(visualImageHandler.Files[visualImageHandler.CurrentFile]);
                }
                catch
                {

                }
            }
        }

        private void OpenAbout(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.Owner = this;
            about.ShowDialog();
        }

        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.Owner = this;
            settings.ShowDialog();
        }

        private void StartView()
        {
            /*ImageController imgControl = ImageController.Instance;
            imgControl.FileEntries = Files;

            /*Properties.Settings.Default.Timer = Int32.Parse(Time.Text);
            Properties.Settings.Default.Randomized = (bool)Randomized.IsChecked;
            Properties.Settings.Default.Save();*//*


            Screen[] screens = Screen.AllScreens;

            screens = screens.OrderBy(c => c.WorkingArea.Left).ToArray();

            List<ImageView> imageViews = new List<ImageView>();

            foreach (Screen screen in screens)
            {
                ImageView imageView = new ImageView();
                imageViews.Add(imageView);
                imageView.Show();
                imageView.Top = screen.WorkingArea.Top;
                imageView.Left = screen.WorkingArea.Left;
                imageView.Width = screen.WorkingArea.Width;
                imageView.Height = screen.WorkingArea.Height;
                imageView.WindowState = WindowState.Maximized;
            }

            imgControl.Reset();
            imgControl.Timer = Properties.Settings.Default.Timer;

            imgControl.ImageViews = imageViews;
            imgControl.Start();

            Close();*/
        }

        // Shuffle a string array
        // TODO: change it to List<string>
        /*private string[] Shuffle(string[] files)
        {
            Random rnd = new Random();

            for (int i = 0; i < files.Length; i++)
            {
                int ticket = rnd.Next(files.Length);
                string fileHolder = files[i]; // Hold the values in [i] before removing it
                files[i] = files[ticket]; // Replace [i] with the lottery [ticket]
                files[ticket] = fileHolder; // Replace [ticket] with the fileHolder which held the [i] value
            }
            return files;
        }*/
    }
}
