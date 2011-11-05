﻿/* 
    Copyright (c) 2011 Microsoft Corporation.  All rights reserved.
    Use of this sample source code is subject to the terms of the Microsoft license 
    agreement under which you licensed this sample source code and is provided AS-IS.
    If you did not accept the terms of the license agreement, you are not authorized 
    to use this sample source code.  For the terms of the license, please see the 
    license agreement between you and Microsoft.
  
    To see all Code Samples for Windows Phone, visit http://go.microsoft.com/fwlink/?LinkID=219604 
  
*/

#region imports

using System;
using System.Device.Location;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework.Media;


#endregion

namespace CSE520S.Rover {
    public partial class MainPage : PhoneApplicationPage {
        private string accuracyText = "";
        private GeoPosition<GeoCoordinate> currentCoordinate;
        private PhotoCamera camera;
        private readonly MediaLibrary library = new MediaLibrary();
        private bool cameraInitialized;

        /// <summary>
        /// This sample receives data from the Location Service and displays the geographic coordinates of the device.
        /// </summary>
        private GeoCoordinateWatcher watcher;

        #region Initialization

        /// <summary>
        /// Constructor for the PhoneApplicationPage object
        /// </summary>
        public MainPage() {
            InitializeComponent();
        }

        #endregion

        #region User Interface

        /// <summary>
        /// Click event handler for the low accuracy button
        /// </summary>
        /// <param name="sender">The control that raised the event</param>
        /// <param name="e">An EventArgs object containing event data.</param>
        private void LowButtonClick(object sender, EventArgs e) {
            // Start data acquisition from the Location Service, low accuracy
            accuracyText = "default accuracy";
            StartLocationService();
            StartCameraService();
        }

        /// <summary>
        /// Click event handler for the high accuracy button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HighButtonClick(object sender, EventArgs e) {
            // Start data acquisition from the Location Service, high accuracy
            accuracyText = "high accuracy";
            StartLocationService();
            StartCameraService();
        }

        private void StopButtonClick(object sender, EventArgs e) {
            if (watcher != null) {
                watcher.Stop();
            }
            StatusTextBlock.Text = "location service is off";
            LatitudeTextBlock.Text = " ";
            LongitudeTextBlock.Text = " ";
        }

        #endregion

        #region Application logic

        /// <summary>
        /// Helper method to start up the location data acquisition
        /// </summary>
        private void StartLocationService() {
            watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High) {MovementThreshold = 5};

            // Add event handlers for StatusChanged and PositionChanged events
            watcher.StatusChanged += watcher_StatusChanged;
            watcher.PositionChanged += watcher_PositionChanged;

            // Start data acquisition
            watcher.Start();
        }

        private void StartCameraService() {
            camera = new PhotoCamera(CameraType.FrontFacing);
            // Event is fired when the PhotoCamera object has been initialized.
            camera.Initialized += cam_Initialized;

            // Event is fired when the capture sequence is complete and an image is available.
            camera.CaptureImageAvailable += cam_CaptureImageAvailable;

            // Event is fired when the capture sequence is complete and a thumbnail image is available.
            camera.CaptureThumbnailAvailable += cam_CaptureThumbnailAvailable;

            // The event is fired when auto-focus is complete.
            camera.AutoFocusCompleted += cam_AutoFocusCompleted;
            cameraInitialized = false;
            viewFinderBrush.SetSource(camera);
        }

        /// <summary>
        /// Handler for the StatusChanged event. This invokes MyStatusChanged on the UI thread and
        /// passes the GeoPositionStatusChangedEventArgs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e) {
            Deployment.Current.Dispatcher.BeginInvoke(() => MyStatusChanged(e));
        }

        /// <summary>
        /// Custom method called from the StatusChanged event handler
        /// </summary>
        /// <param name="e"></param>
        private void MyStatusChanged(GeoPositionStatusChangedEventArgs e) {
            switch (e.Status) {
                case GeoPositionStatus.Disabled:
                    // The location service is disabled or unsupported.
                    // Alert the user
                    StatusTextBlock.Text = "location is unsupported on this device";
                    break;
                case GeoPositionStatus.Initializing:
                    // The location service is initializing.
                    // Disable the Start Location button
                    StatusTextBlock.Text = "initializing location service," + accuracyText;
                    break;
                case GeoPositionStatus.NoData:
                    // The location service is working, but it cannot get location data
                    // Alert the user and enable the Stop Location button
                    StatusTextBlock.Text = "data unavailable," + accuracyText;
                    break;
                case GeoPositionStatus.Ready:
                    // The location service is working and is receiving location data
                    // Show the current position and enable the Stop Location button
                    StatusTextBlock.Text = "receiving data, " + accuracyText;
                    break;
            }
        }

        /// <summary>
        /// Handler for the PositionChanged event. This invokes MyStatusChanged on the UI thread and
        /// passes the GeoPositionStatusChangedEventArgs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e) {
            Deployment.Current.Dispatcher.BeginInvoke(() => MyPositionChanged(e));
        }

        /// <summary>
        /// Custom method called from the PositionChanged event handler
        /// </summary>
        /// <param name="e"></param>
        private void MyPositionChanged(GeoPositionChangedEventArgs<GeoCoordinate> e) {
            // Update the TextBlocks to show the current location
            currentCoordinate = e.Position;
            LatitudeTextBlock.Text = e.Position.Location.Latitude.ToString("0.000000");
            LongitudeTextBlock.Text = e.Position.Location.Longitude.ToString("0.000000");
            if (cameraInitialized) {
                camera.Focus();
            }
        }


        // Update the UI if initialization succeeds.
        private void cam_Initialized(object sender, CameraOperationCompletedEventArgs e) {
            if (e.Succeeded) {
                cameraInitialized = true;
                Dispatcher.BeginInvoke(() => camera.Focus());
            }
        }


        // Informs when thumbnail picture has been taken, saves to isolated storage
        // User will select this image in the pictures application to bring up the full-resolution picture. 
        public void cam_CaptureThumbnailAvailable(object sender, ContentReadyEventArgs e) {
            string fileName = currentCoordinate.Location.Latitude.ToString("0.00000") + currentCoordinate.Location.Longitude.ToString("0.00000") + "_th.jpg";

            try {
                // Save thumbnail as JPEG to isolated storage.
                using (IsolatedStorageFile isStore = IsolatedStorageFile.GetUserStoreForApplication()) {
                    using (IsolatedStorageFileStream targetStream = isStore.OpenFile(fileName, FileMode.Create, FileAccess.Write)) {
                        // Initialize the buffer for 4KB disk pages.
                        var readBuffer = new byte[4096];
                        int bytesRead;

                        // Copy the thumbnail to isolated storage. 
                        while ((bytesRead = e.ImageStream.Read(readBuffer, 0, readBuffer.Length)) > 0) {
                            targetStream.Write(readBuffer, 0, bytesRead);
                        }
                    }
                }
            } finally {
                // Close image stream
                e.ImageStream.Close();
            }
        }


        // Informs when full resolution picture has been taken, saves to local media library and isolated storage.
        private void cam_CaptureImageAvailable(object sender, ContentReadyEventArgs e) {
            string fileName = currentCoordinate.Location.Latitude.ToString("0.00000") + currentCoordinate.Location.Longitude.ToString("0.00000") + ".jpg";

            try {
                // Save picture to the library camera roll.
                library.SavePictureToCameraRoll(fileName, e.ImageStream);

                // Set the position of the stream back to start
                e.ImageStream.Seek(0, SeekOrigin.Begin);

                // Save picture as JPEG to isolated storage.
                using (IsolatedStorageFile isStore = IsolatedStorageFile.GetUserStoreForApplication()) {
                    using (IsolatedStorageFileStream targetStream = isStore.OpenFile(fileName, FileMode.Create, FileAccess.Write)) {
                        // Initialize the buffer for 4KB disk pages.
                        var readBuffer = new byte[4096];
                        int bytesRead;

                        // Copy the image to isolated storage. 
                        while ((bytesRead = e.ImageStream.Read(readBuffer, 0, readBuffer.Length)) > 0) {
                            targetStream.Write(readBuffer, 0, bytesRead);
                        }
                    }
                }
            } finally {
                // Close image stream
                e.ImageStream.Close();
            }
        }

        private void cam_AutoFocusCompleted(object sender, CameraOperationCompletedEventArgs e) {
            Deployment.Current.Dispatcher.BeginInvoke(() => camera.CaptureImage());
        }

        #endregion
    }
}