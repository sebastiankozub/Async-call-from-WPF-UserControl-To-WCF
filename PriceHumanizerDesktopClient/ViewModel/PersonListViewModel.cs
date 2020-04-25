using PriceHumanizerDesktopClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceHumanizerDesktopClient.ViewModel
{
    class PersonListViewModel
    {
        public PersonListViewModel()
        {
            //LoadPrices();
        }

        public ObservableCollection<Price> Prices
        {
            get;
            set;
        }

        public void LoadPrices()
        {
            ObservableCollection<Price> prices = new ObservableCollection<Price>();

            prices.CollectionChanged += _innerStuff_CollectionChanged;

            prices.Add(new Price { GivenPrice = "221 331,44" });
            prices.Add(new Price { GivenPrice = "2 331,44" });
            prices.Add(new Price { GivenPrice = "331,44" });
            prices.Add(new Price { GivenPrice = "0,44" });

            Prices = prices;
        }

        private void _innerStuff_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Object item in e.NewItems)
                {
                    ((INotifyPropertyChanged)item).PropertyChanged += ItemPropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                foreach (Object item in e.OldItems)
                {
                    ((INotifyPropertyChanged)item).PropertyChanged -= ItemPropertyChanged;
                }
            }
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //This will get called when the property of an object inside the collection changes
            var s = sender;
            var args = e;
        }
    }
}
