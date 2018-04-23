# DXRichEdit for Silverlight: How to load DXRichEdit assemblies on demand


<p>This example illustrates the technique for loading DXRichEdit assemblies on demand. The will significantly improve initial loading time of your application. See the <a href="http://msdn.microsoft.com/en-us/library/cc903931(v=VS.95).aspx"><u>How to: Load Assemblies On Demand</u></a> MSDN article to learn more about this technique.</p><p>Note also that all DXRichEdit assemblies are deployed in the ClientBin directory as a single zipped archive (along with the main *.xap archive). The approach described in <a href="http://blogs.msdn.com/b/blemmon/archive/2009/11/25/reading-zip-files-from-silverlight.aspx"><u>Reading zip files from Silverlight</u></a> blog post allows you to unzip this archive in the most flexible manner.</p>

<br/>


