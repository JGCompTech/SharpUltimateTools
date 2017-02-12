using System;

namespace JGCompTech.CSharp.Tools.OSInfo.Enums
{
    [Flags]
    internal enum OtherConsts
    {
        //Type bitmask ( http://msdn.microsoft.com/en-gb/library/ms725494(vs.85).aspx )
        //VERMinorVersion = 1,
        //VERMajorVersion = 2,
        //VERBuildNumber = 4,
        //VERPlatformID = 8,
        //VERServicePackMinor = 16,
        //VERServicePackMajor = 32,
        //VERSuiteName = 64,
        //VERProductType = 128,

        //Condition bitmask ( http://msdn.microsoft.com/en-gb/library/ms725494(vs.85).aspx )
        //VEREqual = 1,
        //VERGreater = 2,
        //VERGreaterEqual = 3,
        //VERLess = 4,
        //VERLessEqual = 5,
        //VERAnd = 6, // only For wSuiteMask
        //VEROr = 7, // only For wSuiteMask

        //sysMetrics ( http://msdn.microsoft.com/en-us/library/ms724385(VS.85).aspx )
        SMTabletPC = 86,
        SMMediaCenter = 87,
        //SMStarter = 88,
        SMServerR2 = 89
    }
}
