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
                Assert.AreEqual(13, item.ID);            //ID
                Assert.AreEqual(12, item.SORTING);       //SORTING
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
            int count = 11;
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
            BAS_TODOITEM item = new BAS_TODOITEM();
            item.ID = 12;
            item.SORTING = 12;
            item.DESCRIPTION = "to do 12";
            item.ISDEFAULT = false;
            item.CREATEDDATE = DateTime.Now;
            item.UPDATEDDATE = DateTime.Now;
            item.VERSION = 0;

            flag = EF.SaveUpdate(item);
            Assert.IsTrue(flag);
        }

        [TestMethod]
        public void TestCreate()
        {
            bool flag = false;
            BAS_TODOITEM item = new BAS_TODOITEM();
            item.ID = 15;
            item.SORTING = 15;
            item.DESCRIPTION = "to do 15";
            item.ISDEFAULT = false;
            item.CREATEDDATE = DateTime.Now;
            item.UPDATEDDATE = DateTime.Now;
            item.VERSION = 0;

            flag = EF.Create(item);
            Assert.IsTrue(flag);
        }

        [TestMethod]
        public void TestDelete()
        {
            bool flag = false;
            int itemId = 15;

            flag = EF.Delete(itemId);
            Assert.IsTrue(flag);

            //int[] itemsId =new int[]{1,15};

            //flag = EF.Delete(itemsId);
            //Assert.IsTrue(flag);
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
