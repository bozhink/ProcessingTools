'http://stackoverflow.com/questions/58510/using-net-how-can-you-find-the-mime-type-of-a-file-based-on-the-file-signature
using System.Runtime.InteropServices;

Public Shared Function GetFromFileName(ByVal fileName As String) As String
        Return GetFromExtension(Path.GetExtension(fileName).Remove(0, 1))
    End Function

    Public Shared Function GetFromExtension(ByVal extension As String) As String
        If extension.StartsWith("."c) Then
            extension = extension.Remove(0, 1)
        End If

        If MIMETypesDictionary.ContainsKey(extension) Then
            Return MIMETypesDictionary(extension)
        End If

        Return "unknown/unknown"
    End Function

    Private Shared ReadOnly MIMETypesDictionary As New Dictionary(Of String, String)() From { _
         {"ai", "application/postscript"}, _
         {"aif", "audio/x-aiff"}, _
         {"aifc", "audio/x-aiff"}, _
         {"aiff", "audio/x-aiff"}, _
         {"asc", "text/plain"}, _
         {"atom", "application/atom+xml"}, _
         {"au", "audio/basic"}, _
         {"avi", "video/x-msvideo"}, _
         {"bcpio", "application/x-bcpio"}, _
         {"bin", "application/octet-stream"}, _
         {"bmp", "image/bmp"}, _
         {"cdf", "application/x-netcdf"}, _
         {"cgm", "image/cgm"}, _
         {"class", "application/octet-stream"}, _
         {"cpio", "application/x-cpio"}, _
         {"cpt", "application/mac-compactpro"}, _
         {"csh", "application/x-csh"}, _
         {"css", "text/css"}, _
         {"dcr", "application/x-director"}, _
         {"dif", "video/x-dv"}, _
         {"dir", "application/x-director"}, _
         {"djv", "image/vnd.djvu"}, _
         {"djvu", "image/vnd.djvu"}, _
         {"dll", "application/octet-stream"}, _
         {"dmg", "application/octet-stream"}, _
         {"dms", "application/octet-stream"}, _
         {"doc", "application/msword"}, _
         {"dtd", "application/xml-dtd"}, _
         {"dv", "video/x-dv"}, _
         {"dvi", "application/x-dvi"}, _
         {"dxr", "application/x-director"}, _
         {"eps", "application/postscript"}, _
         {"etx", "text/x-setext"}, _
         {"exe", "application/octet-stream"}, _
         {"ez", "application/andrew-inset"}, _
         {"gif", "image/gif"}, _
         {"gram", "application/srgs"}, _
         {"grxml", "application/srgs+xml"}, _
         {"gtar", "application/x-gtar"}, _
         {"hdf", "application/x-hdf"}, _
         {"hqx", "application/mac-binhex40"}, _
         {"htm", "text/html"}, _
         {"html", "text/html"}, _
         {"ice", "x-conference/x-cooltalk"}, _
         {"ico", "image/x-icon"}, _
         {"ics", "text/calendar"}, _
         {"ief", "image/ief"}, _
         {"ifb", "text/calendar"}, _
         {"iges", "model/iges"}, _
         {"igs", "model/iges"}, _
         {"jnlp", "application/x-java-jnlp-file"}, _
         {"jp2", "image/jp2"}, _
         {"jpe", "image/jpeg"}, _
         {"jpeg", "image/jpeg"}, _
         {"jpg", "image/jpeg"}, _
         {"js", "application/x-javascript"}, _
         {"kar", "audio/midi"}, _
         {"latex", "application/x-latex"}, _
         {"lha", "application/octet-stream"}, _
         {"lzh", "application/octet-stream"}, _
         {"m3u", "audio/x-mpegurl"}, _
         {"m4a", "audio/mp4a-latm"}, _
         {"m4b", "audio/mp4a-latm"}, _
         {"m4p", "audio/mp4a-latm"}, _
         {"m4u", "video/vnd.mpegurl"}, _
         {"m4v", "video/x-m4v"}, _
         {"mac", "image/x-macpaint"}, _
         {"man", "application/x-troff-man"}, _
         {"mathml", "application/mathml+xml"}, _
         {"me", "application/x-troff-me"}, _
         {"mesh", "model/mesh"}, _
         {"mid", "audio/midi"}, _
         {"midi", "audio/midi"}, _
         {"mif", "application/vnd.mif"}, _
         {"mov", "video/quicktime"}, _
         {"movie", "video/x-sgi-movie"}, _
         {"mp2", "audio/mpeg"}, _
         {"mp3", "audio/mpeg"}, _
         {"mp4", "video/mp4"}, _
         {"mpe", "video/mpeg"}, _
         {"mpeg", "video/mpeg"}, _
         {"mpg", "video/mpeg"}, _
         {"mpga", "audio/mpeg"}, _
         {"ms", "application/x-troff-ms"}, _
         {"msh", "model/mesh"}, _
         {"mxu", "video/vnd.mpegurl"}, _
         {"nc", "application/x-netcdf"}, _
         {"oda", "application/oda"}, _
         {"ogg", "application/ogg"}, _
         {"pbm", "image/x-portable-bitmap"}, _
         {"pct", "image/pict"}, _
         {"pdb", "chemical/x-pdb"}, _
         {"pdf", "application/pdf"}, _
         {"pgm", "image/x-portable-graymap"}, _
         {"pgn", "application/x-chess-pgn"}, _
         {"pic", "image/pict"}, _
         {"pict", "image/pict"}, _
         {"png", "image/png"}, _
         {"pnm", "image/x-portable-anymap"}, _
         {"pnt", "image/x-macpaint"}, _
         {"pntg", "image/x-macpaint"}, _
         {"ppm", "image/x-portable-pixmap"}, _
         {"ppt", "application/vnd.ms-powerpoint"}, _
         {"ps", "application/postscript"}, _
         {"qt", "video/quicktime"}, _
         {"qti", "image/x-quicktime"}, _
         {"qtif", "image/x-quicktime"}, _
         {"ra", "audio/x-pn-realaudio"}, _
         {"ram", "audio/x-pn-realaudio"}, _
         {"ras", "image/x-cmu-raster"}, _
         {"rdf", "application/rdf+xml"}, _
         {"rgb", "image/x-rgb"}, _
         {"rm", "application/vnd.rn-realmedia"}, _
         {"roff", "application/x-troff"}, _
         {"rtf", "text/rtf"}, _
         {"rtx", "text/richtext"}, _
         {"sgm", "text/sgml"}, _
         {"sgml", "text/sgml"}, _
         {"sh", "application/x-sh"}, _
         {"shar", "application/x-shar"}, _
         {"silo", "model/mesh"}, _
         {"sit", "application/x-stuffit"}, _
         {"skd", "application/x-koan"}, _
         {"skm", "application/x-koan"}, _
         {"skp", "application/x-koan"}, _
         {"skt", "application/x-koan"}, _
         {"smi", "application/smil"}, _
         {"smil", "application/smil"}, _
         {"snd", "audio/basic"}, _
         {"so", "application/octet-stream"}, _
         {"spl", "application/x-futuresplash"}, _
         {"src", "application/x-wais-source"}, _
         {"sv4cpio", "application/x-sv4cpio"}, _
         {"sv4crc", "application/x-sv4crc"}, _
         {"svg", "image/svg+xml"}, _
         {"swf", "application/x-shockwave-flash"}, _
         {"t", "application/x-troff"}, _
         {"tar", "application/x-tar"}, _
         {"tcl", "application/x-tcl"}, _
         {"tex", "application/x-tex"}, _
         {"texi", "application/x-texinfo"}, _
         {"texinfo", "application/x-texinfo"}, _
         {"tif", "image/tiff"}, _
         {"tiff", "image/tiff"}, _
         {"tr", "application/x-troff"}, _
         {"tsv", "text/tab-separated-values"}, _
         {"txt", "text/plain"}, _
         {"ustar", "application/x-ustar"}, _
         {"vcd", "application/x-cdlink"}, _
         {"vrml", "model/vrml"}, _
         {"vxml", "application/voicexml+xml"}, _
         {"wav", "audio/x-wav"}, _
         {"wbmp", "image/vnd.wap.wbmp"}, _
         {"wbmxl", "application/vnd.wap.wbxml"}, _
         {"wml", "text/vnd.wap.wml"}, _
         {"wmlc", "application/vnd.wap.wmlc"}, _
         {"wmls", "text/vnd.wap.wmlscript"}, _
         {"wmlsc", "application/vnd.wap.wmlscriptc"}, _
         {"wrl", "model/vrml"}, _
         {"xbm", "image/x-xbitmap"}, _
         {"xht", "application/xhtml+xml"}, _
         {"xhtml", "application/xhtml+xml"}, _
         {"xls", "application/vnd.ms-excel"}, _
         {"xml", "application/xml"}, _
         {"xpm", "image/x-xpixmap"}, _
         {"xsl", "application/xml"}, _
         {"xslt", "application/xslt+xml"}, _
         {"xul", "application/vnd.mozilla.xul+xml"}, _
         {"xwd", "image/x-xwindowdump"}, _
         {"xyz", "chemical/x-xyz"}, _
         {"zip", "application/zip"} _
        }




Imports System.Runtime.InteropServices

<DllImport("urlmon.dll", CharSet:=CharSet.Auto)> _
Private Shared Function FindMimeFromData(pBC As System.UInt32, <MarshalAs(UnmanagedType.LPStr)> pwzUrl As System.String, <MarshalAs(UnmanagedType.LPArray)> pBuffer As Byte(), cbSize As System.UInt32, <MarshalAs(UnmanagedType.LPStr)> pwzMimeProposed As System.String, dwMimeFlags As System.UInt32, _
ByRef ppwzMimeOut As System.UInt32, dwReserverd As System.UInt32) As System.UInt32
End Function
Private Function GetMimeType(ByVal f As FileInfo) As String
    'See http://stackoverflow.com/questions/58510/using-net-how-can-you-find-the-mime-type-of-a-file-based-on-the-file-signature
    Dim returnValue As String = ""
    Dim fileStream As FileStream = Nothing
    Dim fileStreamLength As Long = 0
    Dim fileStreamIsLessThanBByteSize As Boolean = False

    Const byteSize As Integer = 255
    Const bbyteSize As Integer = byteSize + 1

    Const ambiguousMimeType As String = "application/octet-stream"
    Const unknownMimeType As String = "unknown/unknown"

    Dim buffer As Byte() = New Byte(byteSize) {}
    Dim fnGetMimeTypeValue As New Func(Of Byte(), Integer, String)(
        Function(_buffer As Byte(), _bbyteSize As Integer) As String
            Dim _returnValue As String = ""
            Dim mimeType As UInt32 = 0
            FindMimeFromData(0, Nothing, _buffer, _bbyteSize, Nothing, 0, mimeType, 0)
            Dim mimeTypePtr As IntPtr = New IntPtr(mimeType)
            _returnValue = Marshal.PtrToStringUni(mimeTypePtr)
            Marshal.FreeCoTaskMem(mimeTypePtr)
            Return _returnValue
        End Function)

    If (f.Exists()) Then
        Try
            fileStream = New FileStream(f.FullName(), FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            fileStreamLength = fileStream.Length()

            If (fileStreamLength >= bbyteSize) Then
                fileStream.Read(buffer, 0, bbyteSize)
            Else
                fileStreamIsLessThanBByteSize = True
                fileStream.Read(buffer, 0, CInt(fileStreamLength))
            End If

            returnValue = fnGetMimeTypeValue(buffer, bbyteSize)

            If (returnValue.Equals(ambiguousMimeType, StringComparison.OrdinalIgnoreCase) AndAlso fileStreamIsLessThanBByteSize AndAlso fileStreamLength > 0) Then
                'Duplicate the stream content until the stream length is >= bbyteSize to get a more deterministic mime type analysis.
                Dim currentBuffer As Byte() = buffer.Take(fileStreamLength).ToArray()
                Dim repeatCount As Integer = Math.Floor((bbyteSize / fileStreamLength) + 1)
                Dim bBufferList As List(Of Byte) = New List(Of Byte)
                While (repeatCount > 0)
                    bBufferList.AddRange(currentBuffer)
                    repeatCount -= 1
                End While
                Dim bbuffer As Byte() = bBufferList.Take(bbyteSize).ToArray()
                returnValue = fnGetMimeTypeValue(bbuffer, bbyteSize)
            End If
        Catch ex As Exception
            returnValue = unknownMimeType
        Finally
            If (fileStream IsNot Nothing) Then fileStream.Close()
        End Try
    End If
    Return returnValue
End Function