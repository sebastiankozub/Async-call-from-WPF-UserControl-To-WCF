using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace PriceHumanizerDesktopClient.View
{
    /// <summary>
    /// Interaction logic for PersonListView.xaml
    /// </summary>
    public partial class PersonListView : Window
    {
        public PersonListView()
        {
            using (var db = new AdventureWorks2017Context())
            {

                var query = from b in db.Person
                            orderby b.LastName
                            select b;

                Persons = query.ToList();

                db.Person.Add(new Person { FirstName="Sebastian", LastName="Kozub" });

                //Console.WriteLine("All All student in the database:");

                //foreach (var item in query)
                //{
                //    Console.WriteLine(item.FirstName + " " + item.LastName);
                //}

                //Console.WriteLine("Press any key to exit...");
                //Console.ReadKey();

            }

            InitializeComponent();
        }

        public List<Person> Persons
        {
            get; set;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            datagridzik.ItemsSource = Persons;
            datagridzik.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
        }
    }
}
