using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parascan.Data;
using ParascanLib.Data;

namespace ParascanLibTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string dbName = "paraschema.db";
            DAccess sda = new DAccess(dbName);
            if (!File.Exists(dbName))
            {
                sda.CreateSchema();
            }
        }
    }
}
