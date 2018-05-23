using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace ThreadingApp
{
    public class ItemContainer
    {
        public int Maximum { get;}

        private List<Item> _container = new List<Item>();

        public ItemContainer(int maximum)
        {
            Maximum = maximum;
        }

        public void AddToContainer(int id ,int item)
        {
            if (_container.Count != 0)
                if (_container.Count == Maximum || _container[0].IdThread == id)
                    _container.Remove(_container[0]);

            _container.Add(new Item
            {
                IdThread = id,
                Value = item
            });
        }

        public int GetCount(int id)
        {
            return _container.FindAll(x => x.IdThread == id).Count;
        }
    }
}
