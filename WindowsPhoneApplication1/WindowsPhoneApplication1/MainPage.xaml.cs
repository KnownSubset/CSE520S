/* 
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
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework.Media;
using RestSharp;
using RestSharp.Deserializers;
using WindowsPhoneApplication1;

#endregion

namespace CSE520S.Rover {
    public partial class MainPage : PhoneApplicationPage {
        private readonly MediaLibrary library = new MediaLibrary();
        private string accuracyText = "";
        private PhotoCamera camera;
        private bool cameraInUse;
        private bool cameraInitialized;
        private GeoPosition<GeoCoordinate> currentCoordinate;
        private string weather = "";

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
            PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
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
            /*if (watcher != null) {
                watcher.Stop();
            }
            StatusTextBlock.Text = "location service is off";
            LatitudeTextBlock.Text = " ";
            LongitudeTextBlock.Text = " ";*/
            var random = new Random();
            double latitude = random.NextDouble()*90;
            double longitude = random.NextDouble()*-180;
            var geoCoordinate = new GeoCoordinate(latitude, longitude);
            var geoPosition = new GeoPosition<GeoCoordinate>(new DateTimeOffset(),geoCoordinate );
            var geoPositionChangedEventArgs = new GeoPositionChangedEventArgs<GeoCoordinate>(geoPosition);
            MyPositionChanged(geoPositionChangedEventArgs);
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
            camera.CaptureImageAvailable += cam_CaptureImageCompleted;

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
                lock (this) {
                    if (!cameraInUse) {
                        cameraInUse = true;
                        try {
                            camera.Focus();
                            DebugTextBlock.Text = "focus";
                        }
                        catch (Exception exception) {
                            DebugTextBlock.Text = exception.Message;
                        }
                    }
                }
            }
        }


        // Update the UI if initialization succeeds.
        private void cam_Initialized(object sender, CameraOperationCompletedEventArgs e) {
            if (e.Succeeded) {
                cameraInitialized = true;
            }
        }


        // Informs when thumbnail picture has been taken, saves to isolated storage
        // User will select this image in the pictures application to bring up the full-resolution picture. 
        public void cam_CaptureThumbnailAvailable(object sender, ContentReadyEventArgs e) {
            var latitude = currentCoordinate.Location.Latitude.ToString("0.00000");
            var longitude = currentCoordinate.Location.Longitude.ToString("0.00000");
            string fileName = latitude + longitude + "_th.jpg";

            try {
                // Save thumbnail as JPEG to isolated storage.
                var bytes = new List<byte>();
                using (IsolatedStorageFile isStore = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream targetStream = isStore.OpenFile(fileName, FileMode.Create,FileAccess.Write)) {
                        // Initialize the buffer for 4KB disk pages.
                        var readBuffer = new byte[4096];
                        int bytesRead;

                        // Copy the thumbnail to isolated storage. 
                        while ((bytesRead = e.ImageStream.Read(readBuffer, 0, readBuffer.Length)) > 0) {
                            targetStream.Write(readBuffer, 0, bytesRead);
                            bytes.AddRange(readBuffer);
                        }
                        bytes.RemoveRange((int)targetStream.Length, (int)(bytes.Count - targetStream.Length));
                    }
                    UploadImage(latitude, longitude, fileName, bytes.ToArray());
                }
            }catch (Exception exception){
                DebugTextBlock.Text = exception.Message;
            }finally {
                // Close image stream
                e.ImageStream.Close();
            }
        }

        private void UploadImage(string latitude, string longitude, string fileName, byte[] readBuffer) {
            GatherWeatherData(latitude, longitude);
            var resourceXml = "";
            while (resourceXml == string.Empty || resourceXml != weather) {
                resourceXml = weather;
            }

            XDocument xDocument = XDocument.Parse(resourceXml);
            IEnumerable<XElement> currentConditions = xDocument.Descendants("current_condition");
            var tempature = (from currentCondtion in currentConditions select currentCondtion.Element("temp_F").Value).Single();
            var condition = (from currentCondtion in currentConditions select currentCondtion.Element("weatherDesc").Value).Single();
            var pressure = (from currentCondtion in currentConditions select currentCondtion.Element("pressure").Value).Single();
            var humidity = (from currentCondtion in currentConditions select currentCondtion.Element("humidity").Value).Single();
            var restClient = new RestClient {BaseUrl = "http://ec2-107-20-224-204.compute-1.amazonaws.com/node"};
            var restRequest =new RestRequest(Method.POST)
                                .AddFile("file", readBuffer, fileName)
                                .AddParameter("light", 1.0)
                                .AddParameter("tempatue", tempature)
                                .AddParameter("condition", condition)
                                .AddParameter("humidity", humidity)
                                .AddParameter("pressure", pressure)
                                .AddParameter("lat", latitude)
                                .AddParameter("lon", longitude);
            var callback = new Action<RestResponse>(delegate { });
            restClient.ExecuteAsync(restRequest, callback);
            Deployment.Current.Dispatcher.BeginInvoke(() => {
                                                          lock (this) {
                                                              DebugTextBlock.Text = "posted";
                                                          }
                                                      });
        }

        private string GetValueOfElement(string resourceXml, string element){
            string startElement = "<"+element+">";
            string endElement = "</"+element+">";
            int start = resourceXml.IndexOf(startElement) + startElement.Count();
            int end = resourceXml.IndexOf(endElement);
            return resourceXml.Substring(start, end - start);
        }

        private void GatherWeatherData(string latitude, string longitude) {
            var restClient = new RestClient { BaseUrl = "http://free.worldweatheronline.com/feed/weather.ashx" };
            latitude = latitude.Replace(".", "");
            longitude = longitude.Replace(".", "");
            //var weatherParameter = string.Format("{0},{1}", latitude, longitude);
            var weatherParameter = "45.50000,-73.58300";
            var restRequest = new RestRequest(Method.GET)
                .AddParameter("key", "bb17f4b95c050309113011")
                .AddParameter("q", weatherParameter);
            
            Action<RestResponse> callback = response => {
                                                weather = response.Content;
                                            }; 
            restClient.ExecuteAsync(restRequest, callback);
        }


        // Informs when full resolution picture has been taken, saves to local media library and isolated storage.
        private void cam_CaptureImageAvailable(object sender, ContentReadyEventArgs e) {
            var latitude = currentCoordinate.Location.Latitude.ToString("0.00000");
            var longitude = currentCoordinate.Location.Longitude.ToString("0.00000");
            string fileName = latitude + longitude + ".jpg";

            try {
                // Save picture to the library camera roll.
                library.SavePictureToCameraRoll(fileName, e.ImageStream);

                // Set the position of the stream back to start
                e.ImageStream.Seek(0, SeekOrigin.Begin);

                // Save picture as JPEG to isolated storage.
                var bytes = new List<byte>();
                using (IsolatedStorageFile isStore = IsolatedStorageFile.GetUserStoreForApplication()) {
                    using (IsolatedStorageFileStream targetStream = isStore.OpenFile(fileName, FileMode.Create, FileAccess.Write)) {
                        // Initialize the buffer for 4KB disk pages.
                            var readBuffer = new byte[4096];
                            int bytesRead;

                            // Copy the image to isolated storage. 
                            while ((bytesRead = e.ImageStream.Read(readBuffer, 0, readBuffer.Length)) > 0) {
                                targetStream.Write(readBuffer, 0, bytesRead);
                                bytes.AddRange(readBuffer);
                            }
                            bytes.RemoveRange((int)targetStream.Length, (int) (bytes.Count - targetStream.Length));
                        }
                }
            }catch (Exception exception){
                DebugTextBlock.Text = exception.Message;
            }finally {
                // Close image stream
                e.ImageStream.Close();
            }
        }

        // Informs when full resolution picture has been taken, saves to local media library and isolated storage.
        private void cam_CaptureImageCompleted(object sender, ContentReadyEventArgs e) {
            Deployment.Current.Dispatcher.BeginInvoke(() => {
                                                          lock (this) {
                                                              DebugTextBlock.Text = "complete";
                                                              cameraInUse = false;
                                                          }
                                                      });
        }

        private void cam_AutoFocusCompleted(object sender, CameraOperationCompletedEventArgs e) {
            Deployment.Current.Dispatcher.BeginInvoke(() => {
                                                          lock (this) {
                                                              try {
                                                                  camera.CaptureImage();
                                                                  DebugTextBlock.Text = "capture";
                                                              }
                                                              catch (Exception exception) {
                                                                  DebugTextBlock.Text = exception.Message;
                                                              }
                                                          }
                                                      });
        }

        #endregion

    }
}