using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Resources;
using DevExpress.Xpf.RichEdit;

namespace RichEditLoadOnDemandSL {
    public partial class MainPage : UserControl {
        public MainPage() {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            // Download an "on-demand" assemblies.
            WebClient webClient = new WebClient(); 
            webClient.OpenReadCompleted += new OpenReadCompletedEventHandler(webClient_OpenReadCompleted);
            webClient.OpenReadAsync(new Uri("DXRichEditAssemblies.zip", UriKind.Relative));
        }

        private void webClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e) {
            if (e.Error == null) {
                LoadRichEditAssembliesFromZippedStream(e.Result);
                DisplayRichEditFromAssemblies();
            }
            else
                tbInfo.Text = (e.Error.InnerException == null ? e.Error.Message : e.Error.InnerException.Message);
        }

        private void LoadRichEditAssembliesFromZippedStream(Stream zipPackageStream) {
            StreamResourceInfo zipPackageStreamResourceInfo = new StreamResourceInfo(zipPackageStream, null);
            AssemblyPart assemblyPart = new AssemblyPart();
            string[] assemblies = ZipUtil.GetZipContents(zipPackageStream);

            for (int i = 0; i < assemblies.Length; i++) {
                assemblyPart.Load(Application.GetResourceStream(zipPackageStreamResourceInfo, new Uri(assemblies[i], UriKind.Relative)).Stream);
            }

            //assemblyPart.Load(Application.GetResourceStream(zipPackageStreamResourceInfo, new Uri("DevExpress.Xpf.RichEdit.v11.1.dll", UriKind.Relative)).Stream);
            //assemblyPart.Load(Application.GetResourceStream(zipPackageStreamResourceInfo, new Uri("DevExpress.Xpf.Core.v11.1.dll", UriKind.Relative)).Stream);
            //assemblyPart.Load(Application.GetResourceStream(zipPackageStreamResourceInfo, new Uri("DevExpress.RichEdit.v11.1.Core.dll", UriKind.Relative)).Stream);
            //assemblyPart.Load(Application.GetResourceStream(zipPackageStreamResourceInfo, new Uri("DevExpress.Data.v11.1.dll", UriKind.Relative)).Stream);
            //assemblyPart.Load(Application.GetResourceStream(zipPackageStreamResourceInfo, new Uri("DevExpress.Xpf.Ribbon.v11.1.dll", UriKind.Relative)).Stream);
            //assemblyPart.Load(Application.GetResourceStream(zipPackageStreamResourceInfo, new Uri("DevExpress.Printing.v11.1.Core.dll", UriKind.Relative)).Stream);
        }

        private void DisplayRichEditFromAssemblies() {
            // Create an instance of the RichEditControl class.
            RichEditControl richEditControl = new RichEditControl() { Text = "Loaded On Demand" };

            LayoutRoot.Children.Clear();
            LayoutRoot.Children.Add(richEditControl);

            // Display the new RichEditControl.
            this.stackPanel.Children.Add(richEditControl);
        }
    }
}
