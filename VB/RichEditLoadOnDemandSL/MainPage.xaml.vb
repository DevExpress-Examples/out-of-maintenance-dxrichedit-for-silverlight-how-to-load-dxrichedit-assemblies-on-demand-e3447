Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Net
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Resources
Imports DevExpress.Xpf.RichEdit

Namespace RichEditLoadOnDemandSL
	Partial Public Class MainPage
		Inherits UserControl
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
			' Download an "on-demand" assemblies.
			Dim webClient As New WebClient()
			AddHandler webClient.OpenReadCompleted, AddressOf webClient_OpenReadCompleted
			webClient.OpenReadAsync(New Uri("DXRichEditAssemblies.zip", UriKind.Relative))
		End Sub

		Private Sub webClient_OpenReadCompleted(ByVal sender As Object, ByVal e As OpenReadCompletedEventArgs)
			If e.Error Is Nothing Then
				LoadRichEditAssembliesFromZippedStream(e.Result)
				DisplayRichEditFromAssemblies()
			Else
				tbInfo.Text = (If(e.Error.InnerException Is Nothing, e.Error.Message, e.Error.InnerException.Message))
			End If
		End Sub

		Private Sub LoadRichEditAssembliesFromZippedStream(ByVal zipPackageStream As Stream)
			Dim zipPackageStreamResourceInfo As New StreamResourceInfo(zipPackageStream, Nothing)
			Dim assemblyPart As New AssemblyPart()
			Dim assemblies() As String = ZipUtil.GetZipContents(zipPackageStream)

			For i As Integer = 0 To assemblies.Length - 1
				assemblyPart.Load(Application.GetResourceStream(zipPackageStreamResourceInfo, New Uri(assemblies(i), UriKind.Relative)).Stream)
			Next i

			'assemblyPart.Load(Application.GetResourceStream(zipPackageStreamResourceInfo, new Uri("DevExpress.Xpf.RichEdit.v11.1.dll", UriKind.Relative)).Stream);
			'assemblyPart.Load(Application.GetResourceStream(zipPackageStreamResourceInfo, new Uri("DevExpress.Xpf.Core.v11.1.dll", UriKind.Relative)).Stream);
			'assemblyPart.Load(Application.GetResourceStream(zipPackageStreamResourceInfo, new Uri("DevExpress.RichEdit.v11.1.Core.dll", UriKind.Relative)).Stream);
			'assemblyPart.Load(Application.GetResourceStream(zipPackageStreamResourceInfo, new Uri("DevExpress.Data.v11.1.dll", UriKind.Relative)).Stream);
			'assemblyPart.Load(Application.GetResourceStream(zipPackageStreamResourceInfo, new Uri("DevExpress.Xpf.Ribbon.v11.1.dll", UriKind.Relative)).Stream);
			'assemblyPart.Load(Application.GetResourceStream(zipPackageStreamResourceInfo, new Uri("DevExpress.Printing.v11.1.Core.dll", UriKind.Relative)).Stream);
		End Sub

		Private Sub DisplayRichEditFromAssemblies()
			' Create an instance of the RichEditControl class.
			Dim richEditControl As New RichEditControl() With {.Text = "Loaded On Demand"}

			LayoutRoot.Children.Clear()
			LayoutRoot.Children.Add(richEditControl)

			' Display the new RichEditControl.
			Me.stackPanel.Children.Add(richEditControl)
		End Sub
	End Class
End Namespace
