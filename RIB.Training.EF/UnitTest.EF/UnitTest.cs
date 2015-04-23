using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RIB.Training.EF;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        
        [TestMethod]
        public void TestGetFirst()
        {
            BAS_TODOITEM item = null;

            item = EF.GetFirst();
            if (item != null)
            {
                Assert.AreEqual(1, item.ID);            //ID
                Assert.AreEqual(0, item.SORTING);       //SORTING
            }
        }

        [TestMethod]
        public void TestGetLast()
        {
            BAS_TODOITEM item = null;

            item = EF.GetLast();
            if (item != null)
            {
                Assert.AreEqual(19, item.ID);            //ID
                Assert.AreEqual(19, item.SORTING);       //SORTING
            }
        }

        [TestMethod]
        public void TestGetById()
        {
            int id = 2;
            BAS_TODOITEM item = null;

            item = EF.GetById(id);
            if (item != null)
            {
                Assert.AreEqual(id, item.ID);   //ID
            }
        }

        [TestMethod]
        public void TestGetAll()
        {
            int count = 10;
            List<BAS_TODOITEM> itemList = null;

            itemList = EF.GetAll();
            if (itemList.Count > 0)
            {
                Assert.AreEqual(count, itemList.Count);
            }
        }

        [TestMethod]
        public void TestGetDefault()
        {
            BAS_TODOITEM item = null;

            item = EF.GetDefault();
            if (item != null)
            {
                Assert.IsTrue(item.ISDEFAULT);
            }
        }

        [TestMethod]
        public void TestSaveUpdate()
        {
            bool flag = false;
            BAS_TODOITEM[] item = { new BAS_TODOITEM(), new BAS_TODOITEM() };
            item[0].ID = 3;
            item[0].SORTING = 3;
            item[0].DESCRIPTION = "to do 3";
            item[0].ISDEFAULT = false;
            item[0].CREATEDDATE = DateTime.Now;
            item[0].UPDATEDDATE = DateTime.Now;
            item[0].VERSION = 0;

            item[1].ID = 4;
            item[1].SORTING = 4;
            item[1].DESCRIPTION = "to do 4";
            item[1].ISDEFAULT = false;
            item[1].CREATEDDATE = DateTime.Now;
            item[1].UPDATEDDATE = DateTime.Now;
            item[1].VERSION = 0;

            flag = EF.SaveUpdate(item);
            Assert.IsTrue(flag);
        }

        [TestMethod]
        public void TestCreate()
        {
            bool flag = false;
            BAS_TODOITEM[] item = { new BAS_TODOITEM(), new BAS_TODOITEM() };

            item[0].ID = 5;
            item[0].SORTING = 5;
            item[0].DESCRIPTION = "to do 5";
            item[0].ISDEFAULT = false;
            item[0].CREATEDDATE = DateTime.Now;
            item[0].UPDATEDDATE = DateTime.Now;
            item[0].VERSION = 0;

            item[1].ID = 6;
            item[1].SORTING = 6;
            item[1].DESCRIPTION = "to do 6";
            item[1].ISDEFAULT = false;
            item[1].CREATEDDATE = DateTime.Now;
            item[1].UPDATEDDATE = DateTime.Now;
            item[1].VERSION = 0;

            flag = EF.Create(item);
            Assert.IsTrue(flag);
        }

        [TestMethod]
        public void TestDelete()
        {
            bool flag = false;
            int[] itemId = new int[] { 3, 4 };

            flag = EF.Delete(itemId);
            Assert.IsTrue(flag);

            BAS_TODOITEM[] item = { new BAS_TODOITEM(), new BAS_TODOITEM() };
            item[0].ID = 5;
            item[1].ID = 6;

            flag = EF.Delete(item);
            Assert.IsTrue(flag);
        }

        [TestMethod]
        public void TestSearch()
        {
            string description = "to do";
            List<BAS_TODOITEM> itemList = null;
            itemList = EF.Search(description);
            foreach (var item in itemList)
            {
                bool flag = item.DESCRIPTION.Contains(description);
                Assert.IsTrue(flag);
            }
        }
    }
}
