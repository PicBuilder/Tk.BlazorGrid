using System;
using System.Collections.Generic;
using System.Text;

namespace TkMultiSelect
{

    public class MultiSelectItem
    {
        public MultiSelectItem(string id, string text)
        {
            Id = id;
            Text = text;
        }

        public string Id { get; set; }

        public string Text { get; set; }
    }

    public class MultiSelectItems
    {
        public MultiSelectItems()
        {
            Data = new List<MultiSelectItem>();
        }

        public List<MultiSelectItem> Data { get; }
    }

    public class MultiSelectItemsSelected
    {
        public MultiSelectItemsSelected()
        {
            Data = new List<MultiSelectItem>();
        }

        public List<MultiSelectItem> Data { get; }
    }   
}
