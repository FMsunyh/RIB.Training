using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIB.Training.EF
{
    public class EF
    {
        public EF() { }

        //Returns the first element of a sequence. 
        public static BAS_TODOITEM GetFirst()
        {
            BAS_TODOITEM itemFirst = null;

            using (TrainingEntities context = new TrainingEntities())
            {
                var items = from item in context.BAS_TODOITEM orderby item.ID ascending, item.SORTING ascending select item;

                itemFirst = items.FirstOrDefault();
            }
            return itemFirst;
        }

        //Returns the last element of a sequence.
        public static BAS_TODOITEM GetLast()
        {
            BAS_TODOITEM itemLast = null;

            using (TrainingEntities context = new TrainingEntities())
            {
                var items = from item in context.BAS_TODOITEM orderby item.ID descending, item.SORTING descending select item;

                itemLast = items.FirstOrDefault();
            }

            return itemLast;
        }

        //Returns an element of a sequence by the id.
        public static BAS_TODOITEM GetById(int id)
        {
            BAS_TODOITEM itemsGetById = null;

            using (TrainingEntities context = new TrainingEntities())
            {
                var items = from item in context.BAS_TODOITEM where item.ID == id select item;
                itemsGetById = items.FirstOrDefault();
            }

            return itemsGetById;
        }


        //Returns the all elements of a sequence.
        public static List<BAS_TODOITEM> GetAll()
        {
            List<BAS_TODOITEM> itemList = new List<BAS_TODOITEM>();

            using (TrainingEntities context = new TrainingEntities())
            {
                var items = from item in context.BAS_TODOITEM orderby item.SORTING ascending select item;
                foreach (var item in items)
                {
                    itemList.Add(item as BAS_TODOITEM);
                }
            }

            return itemList;
        }

        //Returns the default elements of a sequence.
        public static BAS_TODOITEM GetDefault()
        {
            BAS_TODOITEM itemDefault = null;

            using (TrainingEntities context = new TrainingEntities())
            {
                var items = from item in context.BAS_TODOITEM where item.ISDEFAULT == true select item;

                itemDefault = items.FirstOrDefault();

                if (itemDefault == null)
                {
                    items = from item in context.BAS_TODOITEM orderby item.SORTING ascending, item.ID ascending select item;
                    itemDefault = items.FirstOrDefault();
                }
            }

            return itemDefault;
        }

        //save or update an elements of a sequence.
        public static bool SaveUpdate(params BAS_TODOITEM[] items)
        {
            BAS_TODOITEM itemUpdate = null;

            using (TrainingEntities context = new TrainingEntities())
            {
                foreach (BAS_TODOITEM item in items)
                {
                    itemUpdate = GetById(item.ID);

                    if (itemUpdate == null)
                    {
                        Create(item);
                    }
                    else
                    {
                        itemUpdate.SORTING = item.SORTING;
                        itemUpdate.DESCRIPTION = item.DESCRIPTION;
                        itemUpdate.UPDATEDDATE = item.UPDATEDDATE;
                        itemUpdate.VERSION = itemUpdate.VERSION + 1;
                    }
                }

                context.SaveChanges();
            }

            return true;
        }

        //create an elements of a sequence.
        public static bool Create(params BAS_TODOITEM[] items)
        {
            if (items == null)
            {
                return false;
            }

            using (TrainingEntities context = new TrainingEntities())
            {
                foreach (BAS_TODOITEM item in items)
                {
                    context.BAS_TODOITEM.Add(item);
                }

                context.SaveChanges();
            }

            return true;
        }

        //delete an elements of a sequence by id.
        public static bool Delete(params int[] itemIds)
        {
            //List<BAS_TODOITEM> deleteItemList = null;

            //deleteItemList = GetById(itemIds);
            //if (deleteItemList == null)
            //{
            //    return false;
            //}

            //_context.BAS_TODOITEM.Remove(deleteItemList);

            //_context.SaveChanges();

            // BAS_TODOITEM deleteItem = null;
            using (TrainingEntities context = new TrainingEntities())
            {
                foreach (int itemId in itemIds)
                {
                    BAS_TODOITEM deleteItem = new BAS_TODOITEM() { ID = itemId };
                    //BAS_TODOITEM deleteItem = GetById(itemId);
                    context.Entry<BAS_TODOITEM>(deleteItem).State = EntityState.Deleted;
                }
                context.SaveChanges();
            }

            return true;
        }

        //delete an elements of a sequence.
        public static bool Delete(params BAS_TODOITEM[] items)
        {
            //List<BAS_TODOITEM> deleteItemList = null;

            //deleteItemList = GetById(itemIds);
            //if (deleteItemList == null)
            //{
            //    return false;
            //}

            //_context.BAS_TODOITEM.Remove(deleteItemList);

            //_context.SaveChanges();

            using (TrainingEntities context = new TrainingEntities())
            {
                foreach (BAS_TODOITEM item in items)
                {
                    BAS_TODOITEM deleteItem = new BAS_TODOITEM() { ID = item.ID };
                    //BAS_TODOITEM deleteItem = GetById(item.ID);
                    context.Entry<BAS_TODOITEM>(deleteItem).State = EntityState.Deleted;
                }
                context.SaveChanges();
            }

            return true;
        }

        //Returns the elements of a sequence that the item's DESCRIPTION Contains the substring of description.
        public static List<BAS_TODOITEM> Search(string description)
        {
            List<BAS_TODOITEM> itemList = new List<BAS_TODOITEM>();

            using (TrainingEntities context = new TrainingEntities())
            {
                var items = from item in context.BAS_TODOITEM orderby item.SORTING ascending where item.DESCRIPTION.Contains(description) select item;
                foreach (var item in items)
                {
                    itemList.Add(item);
                }
            }

            return itemList;
        }
    }
}
