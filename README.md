SharpCrush
==============

A .NET Wrapper for the media crush API. https://mediacru.sh/docs/API

# Contributing

...   



# Example Usage

#### Retrieving file info
MediaCrush API Implementation: https://github.com/MediaCrush/MediaCrush/blob/master/docs/api.md#apihash
```csharp
  var fileInfo = SharpCrush.GetFileInfo("tVWMM_ziA3nm");
```

#### Retrieving info about multiple files
MediaCrush API Implementation: https://github.com/MediaCrush/MediaCrush/blob/master/docs/api.md#apiinfolisthash
```csharp
  var fileInfos = SharpCrush.GetFileInfo("tVWMM_ziA3nm", "CPvuR5lRhmS0", "tVWMM_ziA3nm", "CPvuR5lRhmS0", ... );
```

or

```csharp
  var fileInfos = SharpCrush.GetFileInfo(new string[] {"tVWMM_ziA3nm", "CPvuR5lRhmS0", "tVWMM_ziA3nm", ...} );
```

#### Checking for file existance
MediaCrush API Implementation: https://github.com/MediaCrush/MediaCrush/blob/master/docs/api.md#apihash
```csharp

  var fileExists = SharpCrush.GetFileExists("tVWMM_ziA3nm");
  
  if(fileExists) 
  {
    // ...
  }
  
```

#### Deleting files
MediaCrush API Implementation: https://github.com/MediaCrush/MediaCrush/blob/master/docs/api.md#apihash
```csharp

  var deleteFileResult = SharpCrush.DeleteFile("tVWMM_ziA3nm");
  
  if(deleteFileResult == DeleteFileResult.Successful)
  {
    // ...
  }
  
```

#### Retrieving file upload status
MediaCrush API Implementation: https://github.com/MediaCrush/MediaCrush/blob/master/docs/api.md#apihash
```csharp

  var fileStatusResult = SharpCrush.GetFileStatus("tVWMM_ziA3nm");
  
  if(fileStatusResult == GetFileStatusResult.Successful)
  {
    // ...
  }
  
```

#### Uploading files
MediaCrush API Implementation: https://github.com/MediaCrush/MediaCrush/blob/master/docs/api.md#apihash
```csharp

  var fileUploadResult = SharpCrush.UploadFile(fileToUploadGoesHere);
  
  if(fileStatusResult == FileUploadResult.Successful)
  {
    // ...
  }
  
```

#### Uploading URLs
MediaCrush API Implementation: https://github.com/MediaCrush/MediaCrush/blob/master/docs/api.md#apihash
```csharp

  var fileStatusResult = SharpCrush.UploadUrl("http://example.com/reallyslowimages/img.gif");
  
  if(fileStatusResult == UrlUploadResult.Successful)
  {
    // ...
  }
  
```


