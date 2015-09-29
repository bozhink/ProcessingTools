'http://stackoverflow.com/questions/58510/using-net-how-can-you-find-the-mime-type-of-a-file-based-on-the-file-signature
'using System.Runtime.InteropServices;
Imports System.Runtime.InteropServices

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

Private Shared ReadOnly MIMETypesDictionary As New Dictionary(Of String, String)() From {
     {"ai", "application/postscript"},
     {"aif", "audio/x-aiff"},
     {"aifc", "audio/x-aiff"},
     {"aiff", "audio/x-aiff"},
     {"asc", "text/plain"},
     {"atom", "application/atom+xml"},
     {"au", "audio/basic"},
     {"avi", "video/x-msvideo"},
     {"bcpio", "application/x-bcpio"},
     {"bin", "application/octet-stream"},
     {"bmp", "image/bmp"},
     {"cdf", "application/x-netcdf"},
     {"cgm", "image/cgm"},
     {"class", "application/octet-stream"},
     {"cpio", "application/x-cpio"},
     {"cpt", "application/mac-compactpro"},
     {"csh", "application/x-csh"},
     {"css", "text/css"},
     {"dcr", "application/x-director"},
     {"dif", "video/x-dv"},
     {"dir", "application/x-director"},
     {"djv", "image/vnd.djvu"},
     {"djvu", "image/vnd.djvu"},
     {"dll", "application/octet-stream"},
     {"dmg", "application/octet-stream"},
     {"dms", "application/octet-stream"},
     {"doc", "application/msword"},
     {"dtd", "application/xml-dtd"},
     {"dv", "video/x-dv"},
     {"dvi", "application/x-dvi"},
     {"dxr", "application/x-director"},
     {"eps", "application/postscript"},
     {"etx", "text/x-setext"},
     {"exe", "application/octet-stream"},
     {"ez", "application/andrew-inset"},
     {"gif", "image/gif"},
     {"gram", "application/srgs"},
     {"grxml", "application/srgs+xml"},
     {"gtar", "application/x-gtar"},
     {"hdf", "application/x-hdf"},
     {"hqx", "application/mac-binhex40"},
     {"htm", "text/html"},
     {"html", "text/html"},
     {"ice", "x-conference/x-cooltalk"},
     {"ico", "image/x-icon"},
     {"ics", "text/calendar"},
     {"ief", "image/ief"},
     {"ifb", "text/calendar"},
     {"iges", "model/iges"},
     {"igs", "model/iges"},
     {"jnlp", "application/x-java-jnlp-file"},
     {"jp2", "image/jp2"},
     {"jpe", "image/jpeg"},
     {"jpeg", "image/jpeg"},
     {"jpg", "image/jpeg"},
     {"js", "application/x-javascript"},
     {"kar", "audio/midi"},
     {"latex", "application/x-latex"},
     {"lha", "application/octet-stream"},
     {"lzh", "application/octet-stream"},
     {"m3u", "audio/x-mpegurl"},
     {"m4a", "audio/mp4a-latm"},
     {"m4b", "audio/mp4a-latm"},
     {"m4p", "audio/mp4a-latm"},
     {"m4u", "video/vnd.mpegurl"},
     {"m4v", "video/x-m4v"},
     {"mac", "image/x-macpaint"},
     {"man", "application/x-troff-man"},
     {"mathml", "application/mathml+xml"},
     {"me", "application/x-troff-me"},
     {"mesh", "model/mesh"},
     {"mid", "audio/midi"},
     {"midi", "audio/midi"},
     {"mif", "application/vnd.mif"},
     {"mov", "video/quicktime"},
     {"movie", "video/x-sgi-movie"},
     {"mp2", "audio/mpeg"},
     {"mp3", "audio/mpeg"},
     {"mp4", "video/mp4"},
     {"mpe", "video/mpeg"},
     {"mpeg", "video/mpeg"},
     {"mpg", "video/mpeg"},
     {"mpga", "audio/mpeg"},
     {"ms", "application/x-troff-ms"},
     {"msh", "model/mesh"},
     {"mxu", "video/vnd.mpegurl"},
     {"nc", "application/x-netcdf"},
     {"oda", "application/oda"},
     {"ogg", "application/ogg"},
     {"pbm", "image/x-portable-bitmap"},
     {"pct", "image/pict"},
     {"pdb", "chemical/x-pdb"},
     {"pdf", "application/pdf"},
     {"pgm", "image/x-portable-graymap"},
     {"pgn", "application/x-chess-pgn"},
     {"pic", "image/pict"},
     {"pict", "image/pict"},
     {"png", "image/png"},
     {"pnm", "image/x-portable-anymap"},
     {"pnt", "image/x-macpaint"},
     {"pntg", "image/x-macpaint"},
     {"ppm", "image/x-portable-pixmap"},
     {"ppt", "application/vnd.ms-powerpoint"},
     {"ps", "application/postscript"},
     {"qt", "video/quicktime"},
     {"qti", "image/x-quicktime"},
     {"qtif", "image/x-quicktime"},
     {"ra", "audio/x-pn-realaudio"},
     {"ram", "audio/x-pn-realaudio"},
     {"ras", "image/x-cmu-raster"},
     {"rdf", "application/rdf+xml"},
     {"rgb", "image/x-rgb"},
     {"rm", "application/vnd.rn-realmedia"},
     {"roff", "application/x-troff"},
     {"rtf", "text/rtf"},
     {"rtx", "text/richtext"},
     {"sgm", "text/sgml"},
     {"sgml", "text/sgml"},
     {"sh", "application/x-sh"},
     {"shar", "application/x-shar"},
     {"silo", "model/mesh"},
     {"sit", "application/x-stuffit"},
     {"skd", "application/x-koan"},
     {"skm", "application/x-koan"},
     {"skp", "application/x-koan"},
     {"skt", "application/x-koan"},
     {"smi", "application/smil"},
     {"smil", "application/smil"},
     {"snd", "audio/basic"},
     {"so", "application/octet-stream"},
     {"spl", "application/x-futuresplash"},
     {"src", "application/x-wais-source"},
     {"sv4cpio", "application/x-sv4cpio"},
     {"sv4crc", "application/x-sv4crc"},
     {"svg", "image/svg+xml"},
     {"swf", "application/x-shockwave-flash"},
     {"t", "application/x-troff"},
     {"tar", "application/x-tar"},
     {"tcl", "application/x-tcl"},
     {"tex", "application/x-tex"},
     {"texi", "application/x-texinfo"},
     {"texinfo", "application/x-texinfo"},
     {"tif", "image/tiff"},
     {"tiff", "image/tiff"},
     {"tr", "application/x-troff"},
     {"tsv", "text/tab-separated-values"},
     {"txt", "text/plain"},
     {"ustar", "application/x-ustar"},
     {"vcd", "application/x-cdlink"},
     {"vrml", "model/vrml"},
     {"vxml", "application/voicexml+xml"},
     {"wav", "audio/x-wav"},
     {"wbmp", "image/vnd.wap.wbmp"},
     {"wbmxl", "application/vnd.wap.wbxml"},
     {"wml", "text/vnd.wap.wml"},
     {"wmlc", "application/vnd.wap.wmlc"},
     {"wmls", "text/vnd.wap.wmlscript"},
     {"wmlsc", "application/vnd.wap.wmlscriptc"},
     {"wrl", "model/vrml"},
     {"xbm", "image/x-xbitmap"},
     {"xht", "application/xhtml+xml"},
     {"xhtml", "application/xhtml+xml"},
     {"xls", "application/vnd.ms-excel"},
     {"xml", "application/xml"},
     {"xpm", "image/x-xpixmap"},
     {"xsl", "application/xml"},
     {"xslt", "application/xslt+xml"},
     {"xul", "application/vnd.mozilla.xul+xml"},
     {"xwd", "image/x-xwindowdump"},
     {"xyz", "chemical/x-xyz"},
     {"zip", "application/zip"}
    }






<DllImport("urlmon.dll", CharSet:=CharSet.Auto)>
Private Shared Function FindMimeFromData(pBC As System.UInt32, <MarshalAs(UnmanagedType.LPStr)> pwzUrl As System.String, <MarshalAs(UnmanagedType.LPArray)> pBuffer As Byte(), cbSize As System.UInt32, <MarshalAs(UnmanagedType.LPStr)> pwzMimeProposed As System.String, dwMimeFlags As System.UInt32,
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