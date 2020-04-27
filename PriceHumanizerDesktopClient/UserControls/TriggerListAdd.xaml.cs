using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PriceHumanizerDesktopClient.UserControls
{
    public partial class TriggerListAdd : UserControl
    { 
        public TriggerListAdd()
        {
            InitializeComponent();
        }

        public void LoadRows()
        {
            Rows.Add(new Row("111 221 331,44", GetOutputFromInput));
            Rows.Add(new Row("182 331,44", GetOutputFromInput));
            Rows.Add(new Row("528 331,44", GetOutputFromInput));
            Rows.Add(new Row("3 660,44", GetOutputFromInput));
            Rows.Add(new Row("221 331,44", GetOutputFromInput));
            Rows.Add(new Row("528 331,44", GetOutputFromInput));
            Rows.Add(new Row("3 660,44", GetOutputFromInput));
            Rows.Add(new Row("111 221 331,44", GetOutputFromInput));
            Rows.Add(new Row(GetOutputFromInput));
            Rows.Add(new Row(GetOutputFromInput));
        }

        private void bMain_Click(object sender, RoutedEventArgs e)
        {
            Rows.Add(new Row(tbMain.Text, GetOutputFromInput));
            LoadRows();
        }

        private void SelfTriggerListAdd_Loaded(object sender, RoutedEventArgs e)
        {
            LoadRows();
        }

        public static DependencyProperty GetOutputFromInputProperty =
            DependencyProperty.Register("GetOutputFromInput", 
                typeof(Func<string, Task<string>>), 
                typeof(TriggerListAdd));
        public Func<string, Task<string>> GetOutputFromInput
        {
            get { return (Func<string, Task<string>>)GetValue(GetOutputFromInputProperty); }
            set { SetValue(GetOutputFromInputProperty, value); }
        }

        public static DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", 
                typeof(ObservableCollection<Row>), 
                typeof(TriggerListAdd), 
                new PropertyMetadata(new ObservableCollection<Row>()));
        public ObservableCollection<Row> Rows
        {
            get { return (ObservableCollection<Row>)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        public static DependencyProperty TextableProperty =
            DependencyProperty.Register("Textable", typeof(string), typeof(TriggerListAdd));
        public string Textable
        {
            get { return (string)GetValue(TextableProperty); }
            set { SetValue(TextableProperty, value); }
        }      
    }    

    public class Row : INotifyPropertyChanged
    {
        public Row(string input, Func<string, Task<string>> outputFunc)
        {
            _getOutputFromInput = outputFunc;
            _input = input;            
        }

        public Row() { }

        public Row(Func<string, Task<string>> outputFunc)
        {
            _getOutputFromInput = outputFunc;
        }

        public string Input
        {
            get
            {
                return _input;
            }

            set
            {
                if (_input != value)
                {
                    _input = value;
                    RaisePropertyChanged("Input");
                    RaisePropertyChanged("Output");
                }
            }
        }

        private readonly Func<string, Task<string>> _getOutputFromInput;

        public string Output
        {
            get
            {
                if (_getOutputFromInput != null)
                    _getOutputFromInput(_input).ContinueWith((t) => { _output = t.Result; RaisePropertyChanged("Output"); });
                return _output;
            }
            set
            {
                _output = value;
                RaisePropertyChanged("Output");
            }
        }

        private string _input;
        private string _output;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }

    public class PromnptEvent : Prompt
    {
        public PromnptEvent(string textToSpeak) : base(textToSpeak)
        { }

        public bool Value
        {
            get { return this.Value; }
            set
            {
                // (1)
                // set "Value"
                this.Value = value;
                // raise event for value changed
                OnValueChanged(null);
            }
        }

        public new bool IsCompleted
        {
            get { return base.IsCompleted; }
            //OnValueChanged(null); 
        }
        // create an event for the value change
        // this is extra classy, as you can edit the event right
        // from the property window for the control in visual studio
        [Category("Action")]
        [Description("Fires when the value is changed")]
        public event EventHandler ValueChanged;

        protected virtual void OnValueChanged(EventArgs e)
        {
            // (2)
            // Raise the event
            if (ValueChanged != null)
                ValueChanged(this, e);
        }
    }
}
