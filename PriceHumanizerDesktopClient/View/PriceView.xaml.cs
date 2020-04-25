using PriceHumanizerDesktopClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PriceHumanizerDesktopClient.View
{
    public partial class PriceView : UserControl
    {
        public PriceView()
        {
            LoadRows();
            InitializeComponent();
        }
               
        public ObservableCollection<Price> Prices
        {
            get;
            set;
        }

        public void LoadRows()
        {
            var rows = new ObservableCollection<Price>();

            rows.CollectionChanged += _innerRow_RowCollectionChanged;

            rows.Add(new Price { GivenPrice = "221 331,44" });
            rows.Add(new Price { GivenPrice = "2 331,44" });
            rows.Add(new Price { GivenPrice = "331,44" });
            rows.Add(new Price { GivenPrice = "0,44" });

            Prices = rows;
        }

        private void _innerRow_RowCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Object item in e.NewItems)
                {
                    ((INotifyPropertyChanged)item).PropertyChanged += RowPropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                foreach (Object item in e.OldItems)
                {
                    ((INotifyPropertyChanged)item).PropertyChanged -= RowPropertyChanged;
                }
            }
        }

        private void RowPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var s = sender;
            var args = e;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var w = new PersonListView();
            w.ShowActivated = true;
            w.Show();
        }
    }
}
