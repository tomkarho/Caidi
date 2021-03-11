Caidi
======

**C**ross Pl**a**tform V**i**deo Au**d**io R**i**pper

What does it do? It extracts audio from video files.

## For developers

### Building

Go to the *Caidi.App* folder and run one of the following commands.

#### Linux

`dotnet publish -p:PublishSingleFile=true --self-contained true -r linux-x64 -o Publish/Linux`

Binaries are in Publish/Linux folder.

#### Windows

`dotnet publish -p:PublishSingleFile=true --self-contained true -r win-x64 -o Publish/Win`


Binaries are in Publish/Win folder.