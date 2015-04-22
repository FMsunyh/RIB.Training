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
    public class UnitTest
    {
        [TestMethod]
        public void TestGetFirst()
        {
            int[] data = new int[2];
            data = DatabaseOperate.GetFirst();
            Assert.AreEqual(1, data[0]);    //ID
            Assert.AreEqual(0, data[1]);    //SORTING
        }

        [TestMethod]
        public void TestGetLast()
        {
            int[] data = new int[2];
            data = DatabaseOperate.GetLast();
            Assert.AreEqual(12, data[0]);       //ID
            Assert.AreEqual(12, data[1]);        //SORTING
        }

        [TestMethod]
        public void TestGetById()
        {
            int id = 2;
            ArrayList data = new ArrayList();
            data = DatabaseOperate.GetById(id);
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
        public void TestSave()
        {
            int[] id = new int[]{1,3,5,6};
            int version = 0;
            ArrayList data = new ArrayList();
            for (int i = 0; i < id.Length; i++)
            {
                data = DatabaseOperate.GetById(id[i]);
                version = (int)data[6] + 1;
                bool flag = DatabaseOperate.GetSave(id[i], (int)data[6] + 1, DateTime.Now);
                if (flag == true)
                {
                    data = DatabaseOperate.GetById(id[i]);
                    Assert.AreEqual(version, (int)data[6]);
                }
            }
        }

        [TestMethod]
        public void TestCreate()
        {
            int id = 16;
            bool flag = DatabaseOperate.GetCreate(id, 16, "to doing 16", 1, DateTime.Now, DateTime.Now,0);
            if (flag)
            {
                ArrayList data = new ArrayList();
                data = DatabaseOperate.GetById(id);
                Assert.AreEqual(data[0], id);
            }
        }

        [TestMethod]
        public void TestDelete()
        {
            int id = 16;
            bool flag = DatabaseOperate.GetDelete(id);
            Assert.IsTrue(flag);

            //if (flag == true)
            //{
            //    //ArrayList data = new ArrayList();
            //    //data = DatabaseOperate.GetById(id);
            //    //Assert.AreEqual(data.Count, 0);
            //}
        }

        [TestMethod]
        public void TestSearch()
        {
            string description = "to do";
            DataSet dataSet = DatabaseOperate.GetSearch(description);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                bool flag = dataSet.Tables[0].Rows[i][2].ToString().Contains(description);
                Assert.IsTrue(flag);
            }
        }
    }
}
