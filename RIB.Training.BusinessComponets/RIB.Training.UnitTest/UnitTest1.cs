using System;
using System.Xml;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RIB.Training.BusinessComponets;
using System.Collections.Generic;
using System.Collections;
using System.Data;

namespace RIB.Training.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetFirst()
        {
            int[] data = new int[2];
            data = DatabaseOperate.GetFirst();
            Assert.AreEqual(1, data[0]);
            Assert.AreEqual(0, data[1]);
        }

        [TestMethod]
        public void TestGetLast()
        {
            int[] data = new int[2];
            data = DatabaseOperate.GetLast();
            Assert.AreEqual(5, data[0]);
            Assert.AreEqual(7, data[1]);
        }

        [TestMethod]
        public void TestGetById()
        {
            int id = 2;
            ArrayList data = new ArrayList();
            data = DatabaseOperate.GetById(2);
            Assert.AreEqual(data[0],id);
        }

        [TestMethod]
        public void TestGetAll()
        {
            string select = "select Count(*) from BAS_TODOITEM";
            int count1 = DatabaseOperate.GetAll("BAS_TODOITEM");
            int count2 = DatabaseOperate.GetCount(select);
            Assert.AreEqual(count2, count1);
        }

        [TestMethod]
        public void TestGetDefault()
        {
            DataSet dataSet = null;
            dataSet = DatabaseOperate.GetDefault(1);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                Assert.IsTrue((bool)dataSet.Tables[0].Rows[i][3]);
            }
        }

        [TestMethod]
        public void TestCreate()
        {
        }

        [TestMethod]
        public void TestDelete()
        {
        }

        [TestMethod]
        public void TestSearch()
        {
        }
    }
}
