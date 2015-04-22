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
        private static TrainingEntities _context = new TrainingEntities();

        public EF() { }

        //Returns the first element of a sequence. 
        public static BAS_TODOITEM GetFirst()
        {
            BAS_TODOITEM itemFirst = null;
            var items = from item in _context.BAS_TODOITEM orderby item.ID ascending, item.SORTING ascending select item;
            //if (items.Count() > 0)
            //{
            //    itemFirst = items.First();
            //}
            itemFirst = items.FirstOrDefault();
            return itemFirst;
        }

        //Returns the last element of a sequence.
        public static BAS_TODOITEM GetLast()
        {
            BAS_TODOITEM itemLast = null;
            var items = from item in _context.BAS_TODOITEM orderby item.ID descending, item.SORTING descending select item;
            //if (items.Count() > 0)
            //{
            //    //itemLast = items.LastOrDefault();
            //    itemLast = items.First();
            //}
            itemLast = items.LastOrDefault<BAS_TODOITEM>();

            return itemLast;
        }

        //Returns an element of a sequence by the id.
        public static BAS_TODOITEM GetById(int id)
        {
            BAS_TODOITEM itemGetById = null;
            var items = from item in _context.BAS_TODOITEM where item.ID == id select item;
            if (items.Count() > 0)
            {
                itemGetById = items.First();
            }

            return itemGetById;
        }


        //Returns the all elements of a sequence.
        public static List<BAS_TODOITEM> GetAll()
        {
            List<BAS_TODOITEM> itemList = new List<BAS_TODOITEM>();
            var items = from item in _context.BAS_TODOITEM orderby item.SORTING ascending select item;
            foreach (var item in items)
            {
                itemList.Add(item as BAS_TODOITEM);
            }

            return itemList;
        }

        //Returns the default elements of a sequence.
        public static BAS_TODOITEM GetDefault()
        {
            BAS_TODOITEM itemDefault = null;
            var items = from item in _context.BAS_TODOITEM where item.ISDEFAULT == true select item;
            if (items.Count() > 0)
            {
                itemDefault = items.First();
            }
            //else
            //{
            //    items = from item in _context.BAS_TODOITEM orderby item.SORTING ascending, item.ID ascending select item;
            //}

            return itemDefault;
        }

        //save or update an elements of a sequence.
        public static bool SaveUpdate(BAS_TODOITEM item)
        {
            BAS_TODOITEM itemUpdate = null;
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
                _context.SaveChanges();
            }

            return true;
        }

        //create an elements of a sequence.
        public static bool Create(BAS_TODOITEM item)
        {
            if (item == null)
            {
                return false;
            }

            _context.BAS_TODOITEM.Add(item);
            _context.SaveChanges();
            return true;
        }

        //delete an elements of a sequence.
        public static bool Delete(int itemId)
        {
            BAS_TODOITEM deleteItem = GetById(itemId);
            if (deleteItem == null)
            {
                return false;
            }

            _context.BAS_TODOITEM.Remove(deleteItem);
            _context.SaveChanges();

            //BAS_TODOITEM deleteItem = new BAS_TODOITEM() { ID = itemId };
            //context.Entry<BAS_TODOITEM>(deleteItem).State = EntityState.Deleted;
            //context.SaveChanges();
            return true;
        }

        //Returns the elements of a sequence that the item's DESCRIPTION Contains the substring of description.
        public static List<BAS_TODOITEM> Search(string description)
        {
            List<BAS_TODOITEM> itemList = new List<BAS_TODOITEM>();
            var items = from item in _context.BAS_TODOITEM orderby item.SORTING ascending where item.DESCRIPTION.Contains(description) select item;
            foreach (var item in items)
            {
                itemList.Add(item);
            }

            return itemList;
        }
    }
}
