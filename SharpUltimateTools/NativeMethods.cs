using System;
using System.Runtime.InteropServices;

[assembly: CLSCompliant(true)]
namespace Microsoft.CSharp.Tools
{
    //Win32 API definitions

    static class NativeMethods
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct OSVERSIONINFOEX
        {
            public Int32 dwOSVersionInfoSize;
            public Int32 dwMajorVersion;
            public Int32 dwMinorVersion;
            public Int32 dwBuildNumber;
            public Int32 dwPlatFormId;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public String szCSDVersion;

            public short wServicePackMajor;
            public short wServicePackMinor;
            public short wSuiteMask;
            public byte wProductType;
            public byte wReserved;
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean GetVersionEx(ref OSVERSIONINFOEX osVersionInfo);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean GetProductInfo(
          [In] Int32 dwOSMajorVersion,
          [In] Int32 dwOSMinorVersion,
          [In] Int32 dwSpMajorVersion,
          [In] Int32 dwSpMinorVersion,
          [Out] out Int32 pdwReturnedProductType);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean GetSystemMetrics([In] Int32 nIndex);

        [DllImport("kernel32.dll")]
        internal static extern UInt64 VerSetConditionMask([In] ulong dwlConditionMask, [In] uint dwTypeBitMask, [In] byte dwConditionMask);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean VerifyVersionInfo(ref OSVERSIONINFOEX osVersionInfo, [In] uint dwTypeMask, [In] UInt64 dwlConditionMask);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.SysUInt)]
        internal static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        [return: MarshalAs(UnmanagedType.SysUInt)]
        internal static extern IntPtr GetModuleHandle(string moduleName);

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true, ThrowOnUnmappableChar = true)]
        [return: MarshalAs(UnmanagedType.SysUInt)]
        internal static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean IsWow64Process(IntPtr hProcess, [MarshalAs(UnmanagedType.Bool)] out Boolean wow64Process);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal class MEMORYSTATUSEX
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;

            public MEMORYSTATUSEX()
            {
                dwLength = (uint)Marshal.SizeOf(typeof(NativeMethods.MEMORYSTATUSEX));
            }
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern Boolean GetPhysicallyInstalledSystemMemory(out long MemoryInKilobytes);
    }
}
