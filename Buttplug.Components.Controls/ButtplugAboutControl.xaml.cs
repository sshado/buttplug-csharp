﻿using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Buttplug.Core;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Buttplug.Components.Controls
{
    /// <summary>
    /// Interaction logic for ButtplugAboutControl.xaml
    /// </summary>
    public partial class ButtplugAboutControl
    {
        private string _gitHash;
        private string _buildType;
        private uint _clickCounter;
        private ButtplugConfig _config;
        private string _appName;

        public event EventHandler AboutImageClickedABunch;

        public ButtplugAboutControl()
        {
            InitializeComponent();
        }

        public void InitializeVersion()
        {
            AboutVersionNumber.Text = Assembly.GetEntryAssembly().GetName().Version.ToString();
            var longVer = System.Diagnostics.FileVersionInfo.GetVersionInfo(Application.ResourceAssembly.Location)
                .ProductVersion;
            if (longVer.Length > 0)
            {
                AboutVersionNumber.Text = longVer;
            }

            // AssemblyInformationalVersion("1.0.0.0-dev")
            var pos = longVer.IndexOf('-');
            if (pos > 0)
            {
                _buildType = longVer.Substring(pos);
            }

            AboutGitVersion.Text = string.Empty;
            var attribute = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyGitVersion), false)[0];
            if (attribute != null && ((AssemblyGitVersion)attribute).Value.Length > 0)
            {
                _gitHash = ((AssemblyGitVersion)attribute).Value;
                AboutGitVersion.Text = _gitHash;
                AboutGitVersion.MouseDown += GithubRequestNavigate;
            }
        }

        public string GetAboutVersion()
        {
            return AboutVersionNumber.Text + " " + _gitHash;
        }

        private void GithubRequestNavigate(object aObj, MouseButtonEventArgs aEvent)
        {
            System.Diagnostics.Process.Start(
                new Uri($"http://github.com/metafetish/buttplug-csharp/commit/{_gitHash}").AbsoluteUri);
        }

        private void PatreonRequestNavigate(object aObj, MouseButtonEventArgs aEvent)
        {
            System.Diagnostics.Process.Start(new Uri("http://patreon.com/qdot").AbsoluteUri);
        }

        private void Hyperlink_RequestNavigate(object aSender, System.Windows.Navigation.RequestNavigateEventArgs aEvent)
        {
            System.Diagnostics.Process.Start(aEvent.Uri.AbsoluteUri);
        }

        private void IconImage_Click(object aSender, RoutedEventArgs aEvent)
        {
            _clickCounter += 1;
            if (_clickCounter < 5)
            {
                return;
            }

            IconImage.MouseDown -= IconImage_Click;
            AboutImageClickedABunch?.Invoke(this, aEvent);
        }

        private void LicenseHyperlink_Click(object aSender, RoutedEventArgs aEvent)
        {
            new LicenseView().Show();
        }

        public void CheckUpdate([NotNull] ButtplugConfig config, [NotNull] string appName)
        {
            _config = config;
            _appName = appName;

            var check = _config.GetValue(appName + ".updateCheck");
            if (check == null)
            {
                var result = MessageBox.Show("Do you want Buttplug to automaticaly check for updates on start?", "Update Checking", MessageBoxButton.YesNo);
                check = result == MessageBoxResult.Yes ? "true" : "false";
                _config.SetValue(appName + ".updateCheck", check);
            }

            AutoUpdateCheck.IsChecked = bool.Parse(check);
            UpdateGroup.Visibility = Visibility.Visible;

            if (!bool.Parse(check))
            {
                return;
            }

            CheckForUpdates_Click(this, null);
        }

        private void AutoUpdateCheck_Checked(object sender, RoutedEventArgs e)
        {
            if (_config != null && _appName != null && _appName.Length > 0)
            {
                _config.SetValue(_appName + ".updateCheck", true.ToString());
            }
        }

        private void AutoUpdateCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_config != null && _appName != null && _appName.Length > 0)
            {
                _config.SetValue(_appName + ".updateCheck", false.ToString());
            }
        }

        private void CheckForUpdates_Click(object sender, RoutedEventArgs e)
        {
            if (_config == null || _appName == null || _appName.Length <= 0)
            {
                return;
            }

            UpdateCheckStatus.Inlines.Clear();
            UpdateCheckStatus.Text = "Checking for updates... (" + DateTime.Now.ToString() + ")";

            try
            {
                string html = string.Empty;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://updates.buttplug.io/updates.json");
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }

                var json = JObject.Parse(html);
                var latest = json[_appName]["version"]?.ToString();
                if (latest != null)
                {
                    var numericVer = AboutVersionNumber.Text.Substring(0, AboutVersionNumber.Text.IndexOf('-'));
                    var cVer = Version.Parse(numericVer);
                    var lVer = Version.Parse(latest);

                    // Reverse this sign to test
                    if (cVer.CompareTo(lVer) < 0)
                    {
                        // Update available
                        UpdateCheckStatus.Text = "Update Available! (" + DateTime.Now.ToString() + ")";
                        try
                        {
                            var location = json[_appName]["location"]?.ToString();
                            if (location != null)
                            {
                                var hyperlink = new Hyperlink(new Run(location))
                                {
                                    NavigateUri = new Uri(location),
                                };
                                hyperlink.RequestNavigate += Hyperlink_RequestNavigate;
                                UpdateCheckStatus.Text += "\n";
                                UpdateCheckStatus.Inlines.Add(hyperlink);
                            }
                        }
                        catch
                        {
                            // noop - there was an update, we just don't know where
                        }

                        MessageBox.Show("A new buttplug update is available!", "Buttplug Update");
                        try
                        {
                            ((TabControl)((TabItem)Parent).Parent).SelectedItem = Parent;
                            UpdateCheckStatus.Focus();
                        }
                        catch
                        {
                            // noop - things went bang
                        }
                    }
                    else
                    {
                        UpdateCheckStatus.Text = "No new updates! (" + DateTime.Now.ToString() + ")";
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateCheckStatus.Text = "Error encountered whilst checking for updates! (" + DateTime.Now.ToString() + ")";
                new ButtplugLogManager().GetLogger(GetType()).LogException(ex);
            }
        }
    }
}
