using PriceHumanizerDesktopClient.PriceHuminizerHostServiceReference;
using PriceHumanizerDesktopClient.UserControls;
using System;
using System.Collections.ObjectModel;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Windows;

namespace PriceHumanizerDesktopClient
{

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            //MainWindowGetOutputFromInput = async (s) => await GetHumanizePriceAsync(s);
            //MainWindowGetOutputFromInput = async (s) => await GetHumanizePriceAsync(s);
            //MainWindowGetOutputFromInput = async (s) => await Task.Run(() => (s + " " + s + " " + s));
        }

        private void SelfMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindowRows.Add(new Row("999 222,00", MainWindowGetOutputFromInput));
            MainWindowRows.Add(new Row("322 444 666,88", MainWindowGetOutputFromInput));
            MainWindowRows.Add(new Row("465 444 666,99", MainWindowGetOutputFromInput));
            MainWindowRows.Add(new Row("282 222,99", MainWindowGetOutputFromInput));
            MainWindowRows.Add(new Row("666 555 888", MainWindowGetOutputFromInput));
            MainWindowRows.Add(new Row("322 444 666,88", MainWindowGetOutputFromInput));
            MainWindowRows.Add(new Row("222 212,99", MainWindowGetOutputFromInput));
            MainWindowRows.Add(new Row("999 555 888", MainWindowGetOutputFromInput));
            MainWindowRows.Add(new Row(MainWindowGetOutputFromInput));
            MainWindowRows.Add(new Row(MainWindowGetOutputFromInput));

            MainWindowSomeText = "Some Better Test Autoinitialisatioon";
            //MainWindowGetOutputFromInput = async (s) => await GetHumanizePriceAsync(s);
        }

        public static DependencyProperty MainWindowGetOutputFromInputProperty =
            DependencyProperty.Register("GetOutputFromInput", 
                typeof(Func<string, Task<string>>), 
                typeof(MainWindow), 
                new PropertyMetadata(new Func<string, Task<string>> (async (s) => await GetHumanizePriceAsync(s))));
        public Func<string, Task<string>> MainWindowGetOutputFromInput
        {
            get { return (Func<string, Task<string>>)GetValue(MainWindowGetOutputFromInputProperty); }
            set { SetValue(MainWindowGetOutputFromInputProperty, value); }
        }

        public static DependencyProperty MainWindowSomeTextProperty =
            DependencyProperty.Register("MainWindowSomeText", typeof(string), typeof(MainWindow));
        public string MainWindowSomeText
        {
            get { return (string)GetValue(MainWindowSomeTextProperty); }
            set { SetValue(MainWindowSomeTextProperty, value); }
        }

        public static DependencyProperty MainWindowRowsProperty =
            DependencyProperty.Register("MainWindowRows", 
                typeof(Collection<Row>), 
                typeof(MainWindow), 
                new PropertyMetadata(new ObservableCollection<Row>()));
        public Collection<Row> MainWindowRows
        {
            get { return (Collection<Row>)GetValue(MainWindowRowsProperty); }
            set { SetValue(MainWindowRowsProperty, value); }
        }

        private async void GetPriceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {               
                var text = (await CallHumanizePriceAsync(PriceTextBox.Text)).humanizedPrice;
                SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
                speechSynthesizer.Speak(text);
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private static async Task<string> GetHumanizePriceAsync(string price)
        {
            return (await CallHumanizePriceAsync(price)).humanizedPrice;
        }

        private static async Task<PriceHumanizerResponse> CallHumanizePriceAsync(string price)
        {
            IPriceHumanizerService service = new PriceHumanizerServiceClient();

            var priceHumanizerRequest = new PriceHumanizerRequest();
            priceHumanizerRequest.price = price;

            return await Task.Factory.FromAsync(service.BeginHumanizePrice, HumanizePriceEnded, priceHumanizerRequest, service);            
        }

        public static PriceHumanizerResponse HumanizePriceEnded(IAsyncResult result)
        {
            var priceHumanizerService = (IPriceHumanizerService)result.AsyncState;
            PriceHumanizerResponse PriceHumanizerResponse = priceHumanizerService.EndHumanizePrice(result);

            return PriceHumanizerResponse;
        }
    }
}

