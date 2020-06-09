using System;
using System.Collections.Generic;
using System.Text;

namespace TkMultiSelect
{

    public class MultiSelectItem
    {
        public MultiSelectItem(string id, string text, bool isSelected = false, string dbKeyId = null)
        {
            Id = id;
            Text = text;
            IsSelected = isSelected;
            DbKeyId = dbKeyId;
        }

        public string Id { get; set; }

        public string Text { get; set; }

        public bool IsSelected { get; set; }

        public string DbKeyId { get; set; }
    }

    public class MultiSelectItems
    {
        public MultiSelectItems()
        {
            Data = new List<MultiSelectItem>();
        }

        public List<MultiSelectItem> Data { get; }
    }

    //public class MultiSelectItemsSelected
    //{
    //    public MultiSelectItemsSelected()
    //    {
    //        Data = new List<MultiSelectItem>();
    //    }

    //    public List<MultiSelectItem> Data { get; }
    //}   
}
