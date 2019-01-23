
#Platforms.Bluetooth Document

.NET Core 2.1 Library for serving common bluetooth functionality to all platforms.

##Usage Guidelines

How to use this program:

###Requirements

**PostSharp**
>This library is decorated with attributes which are post-processed by PostSharp.
>This will take care of the threading model, logging, security, validation, and more.
>Global aspects are in `.\GlobalAspects.cs`

Please install the PostSharp extension into Visual Studio or include the assemblies at *version 6.0.33* or greater.

###Entry Point

The entry point is currently exposed at the runtime. At the time of writing this is `BluetoothRuntime.Entry();`

The entry point will freeze the platform and cleanly launch it inside several layers of exception catchment.
When in doubt, refer to the logs and enable any debugging flags in the config.

###Native Extensions

All native implementations are kept in the .\Native\ folder.
Each imlementation is stored within a folder with the name of its `Environment.OSVersion.Platform` value.
For Windows this is `Win32NT`.
If you had a new platform called "AndroidOS" then you could add a class "NativeBluetooth.cs" to `.\Native\AndroidOS\`.
So long as that class implements `INativeBluetooth` then it will be injected as soon as the platform name is recognized.
If you wish to change the name of `NativeBluetooth.cs` then you must refer to `BluetoothHost.cs`.
