using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2
{
    class Record
    {
        /// <summary>
        /// A class to store and access the interests and the period time of them
        /// </summary>
        
        //Properties of the class
        private DateTime startdate;
        public DateTime Startdate 
        {
            get { return startdate; }
            set { startdate = value; }
        }

        private DateTime enddate;
        public DateTime Enddate
        {
            get { return enddate; }
            set { enddate = value; }
        }

        private double lawfulinterest;
        public double Lawfulinterest
        {
            get { return lawfulinterest; }
            set { lawfulinterest = value; }
        }

        private double defaultinterest;
        public double Defaultinterest
        {
            get { return defaultinterest; }
            set { defaultinterest = value; }
        }

        //Constractor of Record items
        public Record(DateTime startdate, DateTime enddate, double lawfulinterest, double defaultinterest) 
        {
            this.startdate = startdate;
            this.enddate = enddate;
            this.lawfulinterest = lawfulinterest;
            this.defaultinterest = defaultinterest;
        }

        //A  private static method to store interests and their periods of time in a list
        private static List<Record> Privrecordlist()
        {
            List<Record> Interestlist = new List<Record>();
            Interestlist.Add(new Record(new DateTime(2000, 1, 1), new DateTime(2000, 1, 16), 0.19, 0.21));
            Interestlist.Add(new Record(new DateTime(2000, 1, 17), new DateTime(2000, 1, 26), 0.165, 0.185));
            Interestlist.Add(new Record(new DateTime(2000, 1, 27), new DateTime(2000, 3, 8), 0.16, 0.18));
            Interestlist.Add(new Record(new DateTime(2000, 3, 9), new DateTime(2000, 4, 19), 0.1525, 0.1725));
            Interestlist.Add(new Record(new DateTime(2000, 4, 20), new DateTime(2000, 6, 28), 0.145, 0.165));
            Interestlist.Add(new Record(new DateTime(2000, 6, 29), new DateTime(2000, 9, 5), 0.14, 0.16));
            Interestlist.Add(new Record(new DateTime(2000, 9, 6), new DateTime(2000, 11, 14), 0.1325, 0.1525));
            Interestlist.Add(new Record(new DateTime(2000, 11, 15), new DateTime(2000, 11, 28), 0.1275, 0.1475));
            Interestlist.Add(new Record(new DateTime(2000, 11, 29), new DateTime(2000, 12, 12), 0.1225, 0.1425));
            Interestlist.Add(new Record(new DateTime(2000, 12, 13), new DateTime(2000, 12, 26), 0.115, 0.135));
            Interestlist.Add(new Record(new DateTime(2000, 12, 27), new DateTime(2001, 5, 10), 0.1075, 0.1275));
            Interestlist.Add(new Record(new DateTime(2001, 5, 11), new DateTime(2001, 8, 30), 0.105, 0.125));
            Interestlist.Add(new Record(new DateTime(2001, 8, 31), new DateTime(2001, 9, 17), 0.1025, 0.1225));
            Interestlist.Add(new Record(new DateTime(2001, 9, 18), new DateTime(2001, 11, 8), 0.0975, 0.1175));
            Interestlist.Add(new Record(new DateTime(2001, 11, 9), new DateTime(2002, 12, 5), 0.0925, 0.1125));
            Interestlist.Add(new Record(new DateTime(2002, 12, 6), new DateTime(2003, 3, 6), 0.0875, 0.1075));
            Interestlist.Add(new Record(new DateTime(2003, 3, 7), new DateTime(2003, 6, 5), 0.085, 0.105));
            Interestlist.Add(new Record(new DateTime(2003, 6, 6), new DateTime(2005, 12, 5), 0.08, 0.1));
            Interestlist.Add(new Record(new DateTime(2005, 12, 6), new DateTime(2006, 3, 7), 0.0825, 0.1025));
            Interestlist.Add(new Record(new DateTime(2006, 3, 8), new DateTime(2006, 6, 14), 0.0850, 0.1050));
            Interestlist.Add(new Record(new DateTime(2006, 6, 15), new DateTime(2006, 8, 8), 0.0875, 0.1075));
            Interestlist.Add(new Record(new DateTime(2006, 8, 9), new DateTime(2006, 10, 10), 0.09, 0.11));
            Interestlist.Add(new Record(new DateTime(2006, 10, 11), new DateTime(2006, 12, 12), 0.0925, 0.1125));
            Interestlist.Add(new Record(new DateTime(2006, 12, 13), new DateTime(2007, 3, 13), 0.095, 0.115));
            Interestlist.Add(new Record(new DateTime(2007, 3, 14), new DateTime(2007, 6, 12), 0.0975, 0.1175));
            Interestlist.Add(new Record(new DateTime(2007, 6, 13), new DateTime(2008, 7, 8), 0.1, 0.12));
            Interestlist.Add(new Record(new DateTime(2008, 7, 9), new DateTime(2008, 10, 7), 0.1025, 0.1225));
            Interestlist.Add(new Record(new DateTime(2008, 10, 8), new DateTime(2008, 10, 8), 0.0975, 0.1175));
            Interestlist.Add(new Record(new DateTime(2008, 10, 9), new DateTime(2008, 11, 10), 0.0925, 0.1125));
            Interestlist.Add(new Record(new DateTime(2008, 11, 11), new DateTime(2008, 12, 9), 0.0875, 0.1075));
            Interestlist.Add(new Record(new DateTime(2008, 12, 10), new DateTime(2009, 3, 10), 0.08, 0.1));
            Interestlist.Add(new Record(new DateTime(2009, 3, 11), new DateTime(2009, 4, 7), 0.075, 0.095));
            Interestlist.Add(new Record(new DateTime(2009, 4, 8), new DateTime(2009, 5, 12), 0.0725, 0.0925));
            Interestlist.Add(new Record(new DateTime(2009, 5, 13), new DateTime(2011, 4, 12), 0.0675, 0.0875));
            Interestlist.Add(new Record(new DateTime(2011, 4, 13), new DateTime(2011, 7, 12), 0.07, 0.09));
            Interestlist.Add(new Record(new DateTime(2011, 7, 13), new DateTime(2011, 11, 8), 0.0725, 0.0925));
            Interestlist.Add(new Record(new DateTime(2011, 11, 9), new DateTime(2011, 12, 13), 0.07, 0.09));
            Interestlist.Add(new Record(new DateTime(2011, 12, 14), new DateTime(2012, 7, 10), 0.0675, 0.0875));
            Interestlist.Add(new Record(new DateTime(2012, 7, 11), new DateTime(2013, 5, 7), 0.065, 0.085));
            Interestlist.Add(new Record(new DateTime(2013, 5, 8), new DateTime(2013, 11, 12), 0.06, 0.08));
            Interestlist.Add(new Record(new DateTime(2013, 11, 13), new DateTime(2014, 6, 10), 0.0575, 0.0775));
            Interestlist.Add(new Record(new DateTime(2014, 6, 11), new DateTime(2014, 9, 9), 0.054, 0.074));
            Interestlist.Add(new Record(new DateTime(2014, 9, 10), new DateTime(2016, 3, 15), 0.053, 0.073));
            Interestlist.Add(new Record(new DateTime(2016, 3, 16), DateTime.Today, 0.0525, 0.0725));

            return Interestlist;
        }

        //A public method to call the private one
        public static List<Record> Recordlist() 
        {
            List<Record> Interestlist = Privrecordlist();
            return Interestlist;
        }
    }
}
