#region Header
// Buttplug.Client.Platforms.Bluetooth/InvokeKernel32.cs - Created on 2019-01-21 at 10:48 PM by Sshado.
// This file is part of Buttplug.io which is BSD licensed.
#endregion

using System ;
using System.Runtime.InteropServices ;

using Microsoft.Win32.SafeHandles ;

namespace Buttplug.Client.Platforms.Bluetooth.Native.Win32NT.Legacy
{
    internal class InvokeKernel32
    {
        /// <summary>
        ///     https://msdn.microsoft.com/121cd5b2-e6fd-4eb4-99b4-b652d27b53e8 - Naming Files, Paths, and Namespaces.
        /// </summary>
        internal const short MAX_PATH = 260 ;

        /// <summary>
        ///     Signed integers greater than 0x80000000 are too large so use unchecked to cast. (Programming .NET Compact Framework section 3.3.3)
        ///     Runtime will complain about arithmetic overflow if checked.
        /// </summary>
        internal const int GENERIC_READ = unchecked( ( int ) 0x80_00_00_00 ) ;

        internal const int GENERIC_WRITE = 0x40_00_00_00 ;

        internal const int OPEN_EXISTING = 3 ;

        /// <summary>
        ///     The file or device is being opened or created for asynchronous I/O.
        ///     When subsequent I/O operations are completed on this handle, the event specified in the OVERLAPPED structure will be set to the signaled state.
        /// </summary>
        internal const int FILE_FLAG_OVERLAPPED = 0x40_00_00_00 ;

        /// <summary>
        ///     
        /// </summary>
        /// <param name="hFile">A handle to the communications device. The <see cref="CreateFile"/> function returns this handle.</param>
        /// <param name="lpErrors"></param>
        /// <param name="lpStat"></param>
        /// <returns></returns>
        [ DllImport ( "kernel32.dll", CharSet = CharSet.Auto, SetLastError = true ) ]
        internal static extern bool ClearCommError ( SafeFileHandle hFile, ref int lpErrors, ref COMSTAT lpStat ) ;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lpFileName">The name of the file or device to be created or opened. Buffer Overflow: Limit to <see cref="MAX_PATH"/> or 260 length.</param>
        /// <param name="dwDesiredAccess">The requested access to the file or device, which can be summarized as read, write, both or neither zero.</param>
        /// <param name="dwShareMode">The requested sharing mode of the file or device, which can be read, write, both, delete, all of these, or none (refer to the following table).</param>
        /// <param name="securityAttributes">A pointer to a <see cref="SECURITY_ATTRIBUTES"/> structure. If this is null the handle cannot be inherited by children.</param>
        /// <param name="dwCreationDisposition">Specifies a <see cref="SECURITY_DESCRIPTOR"/> for a file or device.</param>
        /// <param name="dwFlagsAndAtributes">An action to take on a file or device which possibly exists. Typically <see cref="OPEN_EXISTING"/> for devices.</param>
        /// <param name="hTemplateFile">The file or device attributes. <see cref="FILE_FLAG_OVERLAPPED"/> for async/IO and <see cref="FILE_ATTRIBUTE_NORMAL"/> for files.</param>
        /// <returns></returns>
        /// <remarks>
        ///     You may use either forward slashes (/) or backslashes () in <see cref="lpFileName"/> name.
        ///     Defining an MS-DOS device name: https://docs.microsoft.com/en-us/windows/desktop/FileIO/defining-an-ms-dos-device-name
        ///     To create a file stream, specify the name of the file, a colon, and then the name of the stream.
        ///     dwDesiredAccess can be <see cref="GENERIC_READ"/> or <see cref="GENERIC_WRITE"/>
        ///     if dwDesiredAccess is zero then you can query metadata without accessing the file or device even if access is denied.
        ///     if dwShareMode is zero and CreateFile succeeds then the device/file will be locked until the handle is closed.
        /// </remarks>
        [ DllImport ( "kernel32.dll", CharSet = CharSet.Auto, SetLastError = true ) ]
        internal static extern SafeFileHandle CreateFile ( string lpFileName,
                                                           int    dwDesiredAccess,
                                                           int    dwShareMode,
                                                           IntPtr securityAttributes,
                                                           int    dwCreationDisposition,
                                                           int    dwFlagsAndAtributes,
                                                           IntPtr hTemplateFile ) ;
    }
}
