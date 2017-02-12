using System;

namespace JGCompTech.CSharp.Tools.OSInfo.Enums
{
    /// <summary>
    /// General constants
    /// </summary>
    /// <summary>
    /// A list of Product Editions according to ( http://msdn.microsoft.com/en-us/library/ms724358(VS.85).aspx )
    /// </summary>
    [Flags]
    public enum ProductEdition
    {
        /// <summary>
        /// Business
        /// </summary>
        Business = 6,
        /// <summary>
        /// BusinessN
        /// </summary>
        BusinessN = 16,
        /// <summary>
        /// ClusterServer
        /// </summary>
        ClusterServer = 18,
        /// <summary>
        /// DatacenterServer
        /// </summary>
        DatacenterServer = 8,
        /// <summary>
        /// DatacenterServerCore
        /// </summary>
        DatacenterServerCore = 12,
        /// <summary>
        /// DatacenterServerCoreV
        /// </summary>
        DatacenterServerCoreV = 39,
        /// <summary>
        /// DatacenterServerV
        /// </summary>
        DatacenterServerV = 37,

        //DeveloperPreview = 74,
        /// <summary>
        /// Enterprise
        /// </summary>
        Enterprise = 4,

        /// <summary>
        /// EnterpriseE
        /// </summary>
        EnterpriseE = 70,
        /// <summary>
        /// EnterpriseN
        /// </summary>
        EnterpriseN = 27,
        /// <summary>
        /// EnterpriseServer
        /// </summary>
        EnterpriseServer = 10,
        /// <summary>
        /// EnterpriseServerCore
        /// </summary>
        EnterpriseServerCore = 14,
        /// <summary>
        /// EnterpriseServerCoreV
        /// </summary>
        EnterpriseServerCoreV = 41,
        /// <summary>
        /// EnterpriseServerIA64
        /// </summary>
        EnterpriseServerIA64 = 15,
        /// <summary>
        /// EnterpriseServerV
        /// </summary>
        EnterpriseServerV = 38,
        /// <summary>
        /// HomeBasic
        /// </summary>
        HomeBasic = 2,
        /// <summary>
        /// HomeBasicE
        /// </summary>
        HomeBasicE = 67,
        /// <summary>
        /// HomeBasicN
        /// </summary>
        HomeBasicN = 5,
        /// <summary>
        /// HomePremium
        /// </summary>
        HomePremium = 3,
        /// <summary>
        /// HomePremiumE
        /// </summary>
        HomePremiumE = 68,
        /// <summary>
        /// HomePremiumN
        /// </summary>
        HomePremiumN = 26,

        //HomePremiumServer = 34,
        //HyperV = 42,
        /// <summary>
        /// MediumBusinessServerManagement
        /// </summary>
        MediumBusinessServerManagement = 30,

        /// <summary>
        /// MediumBusinessServerMessaging
        /// </summary>
        MediumBusinessServerMessaging = 32,
        /// <summary>
        /// MediumBusinessServerSecurity
        /// </summary>
        MediumBusinessServerSecurity = 31,
        /// <summary>
        /// Professional
        /// </summary>
        Professional = 48,
        /// <summary>
        /// ProfessionalE
        /// </summary>
        ProfessionalE = 69,
        /// <summary>
        /// ProfessionalN
        /// </summary>
        ProfessionalN = 49,

        //SBSolutionServer = 50,
        /// <summary>
        /// ServerForSmallBusiness
        /// </summary>
        ServerForSmallBusiness = 24,

        /// <summary>
        /// ServerForSmallBusinessV
        /// </summary>
        ServerForSmallBusinessV = 35,

        //ServerFoundation = 33,
        /// <summary>
        /// SmallBusinessServer
        /// </summary>
        SmallBusinessServer = 9,

        //SmallBusinessServerPremium = 25,
        //SolutionEmbeddedServer = 56,
        /// <summary>
        /// StandardServer
        /// </summary>
        StandardServer = 7,

        /// <summary>
        /// StandardServerCore
        /// </summary>
        StandardServerCore = 13,
        /// <summary>
        /// StandardServerCoreV
        /// </summary>
        StandardServerCoreV = 40,
        /// <summary>
        /// StandardServerV
        /// </summary>
        StandardServerV = 36,
        /// <summary>
        /// Starter
        /// </summary>
        Starter = 11,
        /// <summary>
        /// StarterE
        /// </summary>
        StarterE = 66,
        /// <summary>
        /// StarterN
        /// </summary>
        StarterN = 47,
        /// <summary>
        /// StorageEnterpriseServer
        /// </summary>
        StorageEnterpriseServer = 23,
        /// <summary>
        /// StorageExpressServer
        /// </summary>
        StorageExpressServer = 20,
        /// <summary>
        /// StorageStandardServer
        /// </summary>
        StorageStandardServer = 21,
        /// <summary>
        /// StorageWorkgroupServer
        /// </summary>
        StorageWorkgroupServer = 22,
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Ultimate
        /// </summary>
        Ultimate = 1,
        /// <summary>
        /// UltimateE
        /// </summary>
        UltimateE = 71,
        /// <summary>
        /// UltimateN
        /// </summary>
        UltimateN = 28,
        /// <summary>
        /// WebServer
        /// </summary>
        WebServer = 17,
        /// <summary>
        /// WebServerCore
        /// </summary>
        WebServerCore = 29
    }
}
