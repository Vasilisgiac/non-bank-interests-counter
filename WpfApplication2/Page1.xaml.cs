using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void calculate_Click(object sender, RoutedEventArgs e)
        {
            

            double parsedvalue;
            //Checking if Textbox1 value contains only numbers or it is unequal to zero or null
            if (!double.TryParse(textbox1.Text, out parsedvalue) || string.IsNullOrWhiteSpace(textbox1.Text.Trim('0')))
            {
                //if Textbox1 value doesn't contain only numbers or it is equal to zero or null .then a warning message appears
                MessageBox.Show("Insert only numbers", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else //if Textbox1 value contains only numbers or it is unequal to zero or not null
            {
                
                textbox1.Text = Math.Round(parsedvalue, 2).ToString(); //Parsing textbox1 text to double number with two digits after decimal point
                if (date1.SelectedDate > date2.SelectedDate) //Checking if date at "from" input is later than "to" input
                {//then selecteddates become null
                    date1.SelectedDate = null; 
                    date2.SelectedDate = null;
                    //and user has to enter again valid dates
                    MessageBox.Show("End date must be later than start date", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else //if date at "from" input is earlier than "to" input
                {
                    //Calling the method to calculate the interests for user's amount of money
                    calculateinterest(date1.SelectedDate, date2.SelectedDate);
                }
            }
        }

        private void calculateinterest(DateTime? from, DateTime? to)
        {
            //Converting parameters to DateTime structure
            var _from = Convert.ToDateTime(from); 
            var _to = Convert.ToDateTime(to);
            //Creating a list with Record type items, this is the list with the different interests and their date bounds
            List<Record> list = Record.Recordlist();
            Page2 page2 = new Page2(); //Creatting a page2 item
            NumberFormatInfo nfi = new CultureInfo("fr-FR", false).NumberFormat;
            nfi.CurrencyGroupSeparator = ",";
            nfi.CurrencyDecimalSeparator = ".";
            NumberStyles styles = NumberStyles.Currency;

            //Getting access to list of interests 
            foreach (Record record in list)
            {
                //Calling checking methods to  compare user's from and to inputs with date bounds of each interest  
                checktimespan1(_from, _to, record, page2);
                checktimespan2(_from, _to, record, page2);
                checktimespan3(_from, _to, record, page2);
                checktimespan4(_from, _to, record, page2);
            }
            double amount = double.Parse(textbox1.Text); //Parsing textbox1 text (amount of money) to double
            page2.laworigamount.Text = amount.ToString("C", nfi); //Assigning textbox1 value to textblock in currency format with euro symbol
            double lawinterests = 0;
            foreach (Page2.MyItem item in page2.listView.Items) 
            {
                item.Lawfulamount = item.Lawfulamount.Replace("," , String.Empty);
                lawinterests += Double.Parse(item.Lawfulamount, styles); //Summing the amount of money gained of each lawful interest
            }
            page2.lawinterests.Text = lawinterests.ToString("C", nfi); //Assigning the sum to textblock in currency format with euro symbol
            page2.lawtotal.Text = (amount + lawinterests).ToString("C", nfi); ; //Assigning the user's amount of money with the sum of lawful interests to textblock in currency format with euro symbol

            amount = double.Parse(textbox1.Text); //Parsing textbox1 text (amount of money) to double
            page2.defaultorigamount.Text = amount.ToString("C", nfi);//Assigning textbox1 value to textblock in currency format with euro symbol
            double defaultinterests = 0;
            foreach (Page2.MyItem item in page2.listView.Items)
            {
                item.Defaultamount = item.Defaultamount.Replace(",", String.Empty);
                defaultinterests += Double.Parse(item.Defaultamount, styles); //Summing the amount of money gained of each default interest
            }
            page2.defaultinterests.Text = defaultinterests.ToString("C", nfi); //Assigning the sum to textblock in currency format with euro symbol
            page2.defaulttotal.Text = (amount + defaultinterests).ToString("C", nfi); ; //Assigning the user's amount of money with the sum of default interests to textblock in currency format with euro symbol
            this.NavigationService.Navigate(page2); //Navigate to Page2
        }

        private bool Iscommon(int year) //This method is checking if the parameter year is common or leap
        {
            if ((year % 400) == 0)
                return false;
            else if ((year % 100) == 0)
                return true;
            else if ((year % 4) == 0)
                return false;
            else
                return true;
        }

        private void checktimespan1(DateTime _from, DateTime _to, Record record, Page2 page2) 
        {
            NumberFormatInfo nfi = new CultureInfo("fr-FR", false).NumberFormat;
            nfi.CurrencyGroupSeparator = ",";
            nfi.CurrencyDecimalSeparator = ".";
            if (_from > record.Startdate && _to < record.Enddate) //Checking if from and to inputs are inside the date bounds of an inerest 
            {
                if (combobox1.SelectedIndex == 0) //Calendar year selected
                {
                    
                        var tempfrom = _from; //temporary from date
                        double result = 0;
                        double result2 = 0;
                        while (tempfrom.Year <= _to.Year) //Calculating each year interests
                        {
                            var daysofyear = Iscommon(tempfrom.Year) ? 365 : 366; //Check if temporary year is common or leap
                            if (tempfrom.Year == _to.Year)
                            {
                                result += double.Parse(textbox1.Text) * ((_to - tempfrom).TotalDays + 1) * record.Lawfulinterest / daysofyear; //Counting the amount of money of lawful interest for this timespan
                                result2 += double.Parse(textbox1.Text) * ((_to - tempfrom).TotalDays + 1) * record.Defaultinterest / daysofyear; //Counting the amount of money of default interest for this timespan
                                break;
                            }
                            var tempto = new DateTime(tempfrom.Year, 12, 31); // temporary to is equal to the last day of a year
                            result += double.Parse(textbox1.Text) * ((tempto - tempfrom).TotalDays + 1) * record.Lawfulinterest / daysofyear; //Counting the amount of money of lawful interest for this timespan
                            result2 += double.Parse(textbox1.Text) * ((tempto - tempfrom).TotalDays + 1) * record.Defaultinterest / daysofyear; //Counting the amount of money of default interest for this timespan
                            tempfrom = tempto.AddDays(1); //temporary from is equal to next day of temporary to
                        }

                        //Insert user's from and to inputs, timespan of them, lawful interest precantage, amount of money of lawful interest, default interest precantage, amount of money of default interest
                        page2.listView.Items.Add(new Page2.MyItem { From = _from, To = _to, Days = ((_to - _from).TotalDays + 1), Lawfulinterest = record.Lawfulinterest.ToString("P"), Lawfulamount = result.ToString("C", nfi), Defaultinterest = record.Defaultinterest.ToString("P"), Defaultamount = result2.ToString("C", nfi) });

                }
                else if (combobox1.SelectedIndex == 1) //360 days year selected
                {
                    var result = double.Parse(textbox1.Text) * ((_to - _from).TotalDays + 1) * record.Lawfulinterest / 360; //Counting the amount of money of lawful interest for this timespan
                    var result2 = double.Parse(textbox1.Text) * ((_to - _from).TotalDays + 1) * record.Defaultinterest / 360; //Counting the amount of money of default interest for this timespan
                    //Insert user's from and to inputs, timespan of them, lawful interest precantage, amount of money of lawful interest, default interest precantage, amount of money of default interest
                    page2.listView.Items.Add(new Page2.MyItem { From = _from, To = _to, Days = ((_to - _from).TotalDays + 1), Lawfulinterest = record.Lawfulinterest.ToString("P"), Lawfulamount = result.ToString("C", nfi), Defaultinterest = record.Defaultinterest.ToString("P"), Defaultamount = result2.ToString("C", nfi) });
                }
            }
        }

        private void checktimespan2(DateTime _from, DateTime _to, Record record, Page2 page2)
        {
            NumberFormatInfo nfi = new CultureInfo("fr-FR", false).NumberFormat;
            nfi.CurrencyGroupSeparator = ",";
            nfi.CurrencyDecimalSeparator = ".";
            if (_from > record.Startdate && _from <= record.Enddate && _to >= record.Enddate)
            {
                if (combobox1.SelectedIndex == 0)
                {
                        var tempfrom = _from;
                        double result = 0;
                        double result2 = 0;
                        while (tempfrom.Year <= record.Enddate.Year)
                        {
                            var daysofyear = Iscommon(tempfrom.Year) ? 365 : 366;
                            if (tempfrom.Year == record.Enddate.Year)
                            {
                                result += double.Parse(textbox1.Text) * ((record.Enddate - tempfrom).TotalDays + 1) * record.Lawfulinterest / daysofyear;
                                result2 += double.Parse(textbox1.Text) * ((record.Enddate - tempfrom).TotalDays + 1) * record.Defaultinterest / daysofyear;
                                break;
                            }
                            var tempto = new DateTime(tempfrom.Year, 12, 31);
                            result += double.Parse(textbox1.Text) * ((tempto - tempfrom).TotalDays + 1) * record.Lawfulinterest / daysofyear;
                            result2 += double.Parse(textbox1.Text) * ((tempto - tempfrom).TotalDays + 1) * record.Defaultinterest / daysofyear;
                            tempfrom = tempto.AddDays(1);
                        }

                    page2.listView.Items.Add(new Page2.MyItem { From = _from, To = record.Enddate, Days = ((record.Enddate - _from).TotalDays + 1), Lawfulinterest = record.Lawfulinterest.ToString("P"), Lawfulamount = result.ToString("C", nfi), Defaultinterest = record.Defaultinterest.ToString("P"), Defaultamount = result2.ToString("C", nfi) });

                }
                else if (combobox1.SelectedIndex == 1)
                {
                    var result = double.Parse(textbox1.Text) * ((record.Enddate - _from).TotalDays + 1) * record.Lawfulinterest / 360;
                    var result2 = double.Parse(textbox1.Text) * ((record.Enddate - _from).TotalDays + 1) * record.Defaultinterest / 360;
                    page2.listView.Items.Add(new Page2.MyItem { From = _from, To = record.Enddate, Days = ((record.Enddate - _from).TotalDays + 1), Lawfulinterest = record.Lawfulinterest.ToString("P"), Lawfulamount = result.ToString("C", nfi), Defaultinterest = record.Defaultinterest.ToString("P"), Defaultamount = result2.ToString("C", nfi) });
                }
            }
        }

        private void checktimespan3(DateTime _from, DateTime _to, Record record, Page2 page2)
        {
            NumberFormatInfo nfi = new CultureInfo("fr-FR", false).NumberFormat;
            nfi.CurrencyGroupSeparator = ",";
            nfi.CurrencyDecimalSeparator = ".";
            if (_from <= record.Startdate && _to >= record.Startdate && _to < record.Enddate)
            {
                if (combobox1.SelectedIndex == 0)
                {
                        var tempfrom = record.Startdate;
                        double result = 0;
                        double result2 = 0;
                        while (tempfrom.Year <= _to.Year)
                        {
                            var daysofyear = Iscommon(tempfrom.Year) ? 365 : 366;
                            if (tempfrom.Year == _to.Year)
                            {
                                result += double.Parse(textbox1.Text) * ((_to - tempfrom).TotalDays + 1) * record.Lawfulinterest / daysofyear;
                                result2 += double.Parse(textbox1.Text) * ((_to - tempfrom).TotalDays + 1) * record.Defaultinterest / daysofyear;
                                break;
                            }
                            var tempto = new DateTime(tempfrom.Year, 12, 31);
                            result += double.Parse(textbox1.Text) * ((tempto - tempfrom).TotalDays + 1) * record.Lawfulinterest / daysofyear;
                            result2 += double.Parse(textbox1.Text) * ((tempto - tempfrom).TotalDays + 1) * record.Defaultinterest / daysofyear;
                            tempfrom = tempto.AddDays(1);
                        }

                        page2.listView.Items.Add(new Page2.MyItem { From = record.Startdate, To = _to, Days = ((_to - record.Startdate).TotalDays + 1), Lawfulinterest = record.Lawfulinterest.ToString("P"), Lawfulamount = result.ToString("C", nfi), Defaultinterest = record.Defaultinterest.ToString("P"), Defaultamount = result2.ToString("C", nfi) });

                }
                else if (combobox1.SelectedIndex == 1)
                {
                    var result = double.Parse(textbox1.Text) * ((_to - record.Startdate).TotalDays + 1) * record.Lawfulinterest / 360;
                    var result2 = double.Parse(textbox1.Text) * ((_to - record.Startdate).TotalDays + 1) * record.Defaultinterest / 360;
                    page2.listView.Items.Add(new Page2.MyItem { From = record.Startdate, To = _to, Days = ((_to - record.Startdate).TotalDays + 1), Lawfulinterest = record.Lawfulinterest.ToString("P"), Lawfulamount = result.ToString("C", nfi), Defaultinterest = record.Defaultinterest.ToString("P"), Defaultamount = result2.ToString("C", nfi) });
                }
            }
        }

        private void checktimespan4(DateTime _from, DateTime _to, Record record, Page2 page2)
        {
            NumberFormatInfo nfi = new CultureInfo("fr-FR", false).NumberFormat;
            nfi.CurrencyGroupSeparator = ",";
            nfi.CurrencyDecimalSeparator = ".";
            if (_from <= record.Startdate && _to >= record.Enddate)
            {
                if (combobox1.SelectedIndex == 0)
                {
                        var tempfrom = record.Startdate;
                        double result = 0;
                        double result2 = 0;
                        while (tempfrom.Year <= record.Enddate.Year)
                        {
                            var daysofyear = Iscommon(tempfrom.Year) ? 365 : 366;
                            if (tempfrom.Year == record.Enddate.Year)
                            {
                                result += double.Parse(textbox1.Text) * ((record.Enddate - tempfrom).TotalDays + 1) * record.Lawfulinterest / daysofyear;
                                result2 += double.Parse(textbox1.Text) * ((record.Enddate - tempfrom).TotalDays + 1) * record.Defaultinterest / daysofyear;
                                break;
                            }
                            var tempto = new DateTime(tempfrom.Year, 12, 31);
                            result += double.Parse(textbox1.Text) * ((tempto - tempfrom).TotalDays + 1) * record.Lawfulinterest / daysofyear;
                            result2 += double.Parse(textbox1.Text) * ((tempto - tempfrom).TotalDays + 1) * record.Defaultinterest / daysofyear;
                            tempfrom = tempto.AddDays(1);
                        }

                        page2.listView.Items.Add(new Page2.MyItem { From = record.Startdate, To = record.Enddate, Days = ((record.Enddate - record.Startdate).TotalDays + 1), Lawfulinterest = record.Lawfulinterest.ToString("P"), Lawfulamount = result.ToString("C", nfi), Defaultinterest = record.Defaultinterest.ToString("P"), Defaultamount = result2.ToString("C", nfi) });

                }
                else if (combobox1.SelectedIndex == 1)
                {
                    var result = double.Parse(textbox1.Text) * ((record.Enddate - record.Startdate).TotalDays + 1) * record.Lawfulinterest / 360;
                    var result2 = double.Parse(textbox1.Text) * ((record.Enddate - record.Startdate).TotalDays + 1) * record.Defaultinterest / 360;
                    page2.listView.Items.Add(new Page2.MyItem { From = record.Startdate, To = record.Enddate, Days = ((record.Enddate - record.Startdate).TotalDays + 1), Lawfulinterest = record.Lawfulinterest.ToString("P"), Lawfulamount = result.ToString("C", nfi), Defaultinterest = record.Defaultinterest.ToString("P"), Defaultamount = result2.ToString("C", nfi) });
                }
            }
        }

       
    }
}
