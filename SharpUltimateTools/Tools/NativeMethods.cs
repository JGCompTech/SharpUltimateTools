using System;
using System.Runtime.InteropServices;

[assembly: CLSCompliant(true)]
namespace JGCompTech.CSharp.Tools
{
    static class NativeMethods
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct OSVERSIONINFOEX
        {
            public int dwOSVersionInfoSize;
            public int dwMajorVersion;
            public int dwMinorVersion;
            public int dwBuildNumber;
            public int dwPlatFormId;

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
          [In] int dwOSMajorVersion,
          [In] int dwOSMinorVersion,
          [In] int dwSpMajorVersion,
          [In] int dwSpMinorVersion,
          [Out] out int pdwReturnedProductType);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean GetSystemMetrics([In] int nIndex);

        [DllImport("kernel32.dll")]
        internal static extern ulong VerSetConditionMask([In] ulong dwlConditionMask, [In] uint dwTypeBitMask, [In] byte dwConditionMask);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean VerifyVersionInfo(ref OSVERSIONINFOEX osVersionInfo, [In] uint dwTypeMask, [In] ulong dwlConditionMask);

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
                dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            }
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern Boolean GetPhysicallyInstalledSystemMemory(out long MemoryInKilobytes);

        public static int getProductInfo(int Major, int Minor)
        {
            var strProductType = new int();
            GetProductInfo(Major, Minor, 0, 0, out strProductType);
            return strProductType;
        }
    }
}
