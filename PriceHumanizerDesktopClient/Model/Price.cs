using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceHumanizerDesktopClient.Model
{
    public class Price : INotifyPropertyChanged
    {
        public string GivenPrice
        {
            get
            {
                return _givenPrice;
            }

            set
            {
                if (_givenPrice != value)
                {
                    _givenPrice = value;
                    RaisePropertyChanged("GivenPrice");
                    RaisePropertyChanged("HumanizedPrice");
                }
            }
        }

        public string HumanizedPrice
        {
            get
            {
                return _givenPrice + "  " + _givenPrice;
            }

            set
            {
                if (_humanizedPrice != value)
                {
                    _humanizedPrice = value;
                    RaisePropertyChanged("HumanizedPrice");
                }
            }
        }

        private string _givenPrice;
        private string _humanizedPrice;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));            
        }
    }
}
