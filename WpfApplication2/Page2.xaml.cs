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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : System.Windows.Controls.Page
    {
        public Page2()
        {
            InitializeComponent();

            //Columns of listview have equal width and they fit the whole space of listview
            var x = listView.ActualWidth / 7;
            foreach (GridViewColumn col in listGridView.Columns)
            {
                col.Width = x;
            }
        }

        public class MyItem //Class of listview items
        { 
            public DateTime From { get; set; }
            public DateTime To { get; set; }
            public double Days { get; set; }
            public string Lawfulinterest { get; set; }
            public string Lawfulamount { get; set; }
            public string Defaultinterest { get; set; }
            public string Defaultamount { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e) //Button click event to navigate the user back to page1
        {
            this.NavigationService.Navigate(new Page1());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //Button click event to export the data of page2 to excel workbook
        {
            Microsoft.Win32.SaveFileDialog dlg = new SaveFileDialog(); //Open save file dialog
            dlg.FileName = "Workbook"; //default file name
            dlg.DefaultExt = ".xls"; //default file extension
            dlg.Filter = "Excel Workbook|*.xls;*.xlsx"; //filter files by extension

            
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application(); //Creating an interface for excel
            Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet); //Creating an excel workbook with one worksheet 
            Worksheet ws = (Worksheet)app.ActiveSheet; //Getting access to active worksheet
            app.Visible = false;

            //Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            //Process save file dialog box results
            if (result == true) //if user press ok
            {
                Mouse.OverrideCursor = Cursors.Wait; //Waiting cursor
                //Inserting listview headers in excel file
                string[] array = new string[7] { "From", "To", "Days", "Lawful Interest", "Lawful Amount", "Default Interest", "Default Amount" };
                int rowCount = 1;
                int columnCount = array.GetLength(0);
                Range range = (Range)ws.Cells[1, 1];
                range = range.Resize[rowCount, columnCount];
                range.Value = array;
                /* ws.Cells[1, 1] = "From";
                 ws.Cells[1, 2] = "To";
                 ws.Cells[1, 3] = "Days";
                 ws.Cells[1, 4] = "Lawful Interest";
                 ws.Cells[1, 5] = "Lawful Amount";
                 ws.Cells[1, 6] = "Default Interest";
                 ws.Cells[1, 7] = "Default Amount";*/
                int i = 2;

                //Inserting listview items in excel file
                foreach (MyItem item in listView.Items)
                {
                    string[] array2 = new string[7] { item.From.ToString("dd/MM/yyyy"), item.To.ToString("dd/MM/yyyy"), item.Days.ToString(), item.Lawfulinterest, item.Lawfulamount, item.Defaultinterest, item.Defaultamount };
                    int rowCount2 = 1;
                    int columnCount2 = array2.GetLength(0);
                    Range range2 = (Range)ws.Cells[i, 1];
                    range2 = range2.Resize[rowCount2, columnCount2];
                    range2.Value = array2;
                    /*ws.Cells[i, 1] = item.From.ToString("dd/MM/yyyy");
                    ws.Cells[i, 2] = item.To.ToString("dd/MM/yyyy");
                    ws.Cells[i, 3] = item.Days;
                    ws.Cells[i, 4] = item.Lawfulinterest;
                    ws.Cells[i, 5] = item.Lawfulamount;
                    ws.Cells[i, 6] = item.Defaultinterest;
                    ws.Cells[i, 7] = item.Defaultamount;*/
                    i++;
                }
                i++;

                //Inserting original amount of money, total amount of money of interests and total amount of money with interests in excel file
                string[] array3 = { "Original amount", " ", " ", " ", laworigamount.Text, " ", defaultorigamount.Text };
                ws.Range[ws.Cells[i, 1], ws.Cells[i, 2]].Merge();
                ws.Range[ws.Cells[i, 1], ws.Cells[i, 7]].Value = array3;
                i++;
                string[] array4 = { "Interests", " ", " ", " ", lawinterests.Text, " ", defaultinterests.Text };
                ws.Range[ws.Cells[i, 1], ws.Cells[i, 2]].Merge();
                ws.Range[ws.Cells[i, 1], ws.Cells[i, 7]].Value = array4;
                i++;
                string[] array5 = { "Total amount", " ", " ", " ", lawtotal.Text, " ", defaulttotal.Text };
                ws.Range[ws.Cells[i, 1], ws.Cells[i, 2]].Merge();
                ws.Range[ws.Cells[i, 1], ws.Cells[i, 7]].Value = array5;

                //Saving excel file
                wb.SaveAs(dlg.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                Mouse.OverrideCursor = Cursors.Arrow; //Arrow cursor
                MessageBox.Show("Your data has been successfully exported", "Message", MessageBoxButton.OK, MessageBoxImage.Information); //Information message for successful import
            }
            app.Quit(); //Close interface for ecel
        }

        private void listView_SizeChanged(object sender, SizeChangedEventArgs e) //Event when listview size is changed 
        {
            //Columns of listview have equal width and they fit the whole space of listview
            var x = listView.ActualWidth / 7;
            foreach (GridViewColumn col in listGridView.Columns) 
            {
                col.Width = x;
            }

        }
    }
}
