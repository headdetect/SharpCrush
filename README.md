SharpCrush
==============

A .NET 4 & 4.5 (seperate projects) wrapper for the media crush API: https://mediacru.sh/docs/API  
MediaCrush source repo is found at https://github.com/MediaCrush/MediaCrush


# Example Usage


### Retrieving file info
MediaCrush API Implementation: https://github.com/MediaCrush/MediaCrush/blob/master/docs/api.md#apihash
```csharp
  var fileInfo = SharpCrush.GetFileInfo("tVWMM_ziA3nm");
  
  // The compression of the file //
  float compression = fileInfo.Compression;
  
  // All converted files associated with MediaCrush //
  SharpCrushFile[] files  = fileInfo.Files;
  
  // The original image uploaded //
  string OG = fileInfo.Original;
  
  // The image file type of the original file uploaded //
  string OGFileType = fileInfo.OriginalFileType;
  
  
  // If there is an error, you can retrieve it by implementing the following: //
  int statusCode = fileInfo.StatusCode;
  // it will return the http status code. 404, 409, 500, etc //
  
  
```

### Retrieving info about multiple files
MediaCrush API Implementation: https://github.com/MediaCrush/MediaCrush/blob/master/docs/api.md#apiinfolisthash
```csharp
  var fileInfos = SharpCrush.GetFileInfo("tVWMM_ziA3nm", "CPvuR5lRhmS0", "tVWMM_ziA3nm", "CPvuR5lRhmS0", ... );
  
  // just a dictionary of media files with the hash as the key //
  foreach(var file in fileInfos) 
  {
    string hash = file.Key;
    SharpCrushMediaFile mediaFile = file.Value;
  }
```

or

```csharp

  // Very useless, but available. //
  var fileInfos = SharpCrush.GetFileInfo(new string[] {"tVWMM_ziA3nm", "CPvuR5lRhmS0", "tVWMM_ziA3nm", ...} );
```

### Checking for file existance
MediaCrush API Implementation: https://github.com/MediaCrush/MediaCrush/blob/master/docs/api.md#apihashexists
```csharp

  var fileExists = SharpCrush.GetFileExists("tVWMM_ziA3nm");
  
  if(fileExists) 
  {
    // ...
  }
  
```

### Deleting files
MediaCrush API Implementation: https://github.com/MediaCrush/MediaCrush/blob/master/docs/api.md#apihashdelete
```csharp

  var deleteFileResult = SharpCrush.DeleteFile("tVWMM_ziA3nm");
  
  if(deleteFileResult == DeleteFileResult.Successful)
  {
    // ...
  }
  
```

### Retrieving file upload status
MediaCrush API Implementation: https://github.com/MediaCrush/MediaCrush/blob/master/docs/api.md#apihashstatus
```csharp

  var fileStatusResult = SharpCrush.GetFileStatus("tVWMM_ziA3nm");
  
  if(fileStatusResult == GetFileStatusResult.Successful)
  {
    // ...
  }
  
```

### Uploading files
MediaCrush API Implementation: https://github.com/MediaCrush/MediaCrush/blob/master/docs/api.md#apiuploadfile
```csharp

  var fileUploadResult = SharpCrush.UploadFile("insert_path_to_file_here.png");
  
  string fileHash = fileUploadResult.FileHash;
  
  if(fileStatusResult.Result == FileUploadResults.Successful)
  {
    // ...
  }
  
```
_Note_: Try not to confuse `FileUploadResult` with `FileUploadResults` (Notice the 's' at the end).   
**FileUploadResult** is the wrapper class for the API. Hashes, SharpCrushMediaFiles, etc...  
**FileUploadResults** are the result codes that are returned. 404 (NotFound), 200(Successful), 409(AlreadyUploaded), etc...


### Uploading URLs
MediaCrush API Implementation: https://github.com/MediaCrush/MediaCrush/blob/master/docs/api.md#apiuploadurl
```csharp

  var urlUploadResult = SharpCrush.UploadUrl("http://example.com/reallyslowimagesandorcrappyvideofiles/img.gif");
  
  string fileHash = urlUploadResult.FileHash;
  
  if(urlUploadResult.Result == UrlUploadResults.Successful)
  {
    // ...
  }
  
```

_Note_: Try not to confuse `UrlUploadResult` with `UrlUploadResults` (Notice the 's' at the end).   
**UrlUploadResult** is the wrapper class for the API. Hashes, SharpCrushMediaFiles, etc...  
**UrlUploadResults** are the result codes that are returned. 404 (NotFound), 200(Successful), 409(AlreadyUploaded), etc...


_NOTE: These will probably change as I find myself hating my code more and more_

# Contributing

See [Contributing.md](contributing.md)

# Licensing

See [LICENSE](LICENSE)
