Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.IO
Imports System.Text

Namespace RichEditLoadOnDemandSL
	Public Class ZipUtil
		''' <summary>
		''' Reads the file names from the header of the zip file
		''' </summary>
		''' <param name="zipStream">The stream to the zip file</param>
		''' <returns>An array of file names stored within the zip file. These file names may also include relative paths.</returns>
		Public Shared Function GetZipContents(ByVal zipStream As System.IO.Stream) As String()
			Dim names As New List(Of String)()
			Dim reader As New BinaryReader(zipStream)

			Do While reader.ReadUInt32() = &H04034b50
				' Skip the portions of the header we don't care about
				reader.BaseStream.Seek(14, SeekOrigin.Current)
				Dim compressedSize As UInteger = reader.ReadUInt32()
				Dim uncompressedSize As UInteger = reader.ReadUInt32()
				Dim nameLength As Integer = reader.ReadUInt16()
				Dim extraLength As Integer = reader.ReadUInt16()
				Dim nameBytes() As Byte = reader.ReadBytes(nameLength)
				names.Add(Encoding.UTF8.GetString(nameBytes, 0, nameLength))
				reader.BaseStream.Seek(extraLength + compressedSize, SeekOrigin.Current)
			Loop

			' Move the stream back to the begining
			zipStream.Seek(0, SeekOrigin.Begin)
			Return names.ToArray()
		End Function
	End Class
End Namespace
