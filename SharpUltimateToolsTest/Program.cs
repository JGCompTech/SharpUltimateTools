using JGCompTech.CSharp;
using JGCompTech.CSharp.Tools.HWInfo;
using JGCompTech.CSharp.Tools.OSInfo;
using System;

namespace SharpUltimateToolsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //double num = 6215891419.136;
            //double num2 = 12800;
            //Console.WriteLine(num.ConvertBytes());
            //Console.WriteLine(num.BytesToKB());
            Console.WriteLine(Name.String);
            Console.WriteLine(Name.StringExpanded);
            Console.WriteLine(Name.StringExpanded2);
            Console.WriteLine(UserInfo.RegisteredOrganization);
            Console.WriteLine(UserInfo.RegisteredOwner);
            Console.WriteLine(BIOS.Vendor);
            Console.ReadLine();
        }
    }
}
