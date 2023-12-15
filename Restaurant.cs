using System;
using System.Text;
using System.Xml;

namespace TeamOppgaveC
{
    internal class Restaurant
    {
        public static Restaurant restaurant;

        private List<Table> _tables = new List<Table>();
        private List<Reservation> _reservations = new List<Reservation>();
        private int _openingHour;
        private int _closingHour;
        private string _name;

        public Restaurant(string name, int openHour, int closeHour)
        {
            _openingHour = openHour;
            _closingHour = closeHour;
            _name = name;
            
            restaurant = this; // så man kan hente ut tider fra Tables, via GetOpeningHours();
        }

        public Table AddTable(string name, int guests)
        {
            Table table = new Table(name, guests);
            _tables.Add(table);
            return table;

        }

        public Reservation CreateReservation(string resName, string resPhoneN, int guests, DateTime resTime) 
        {
            
            // Lager en liste over alle bord som er store nok, og ledig.
            var availableTable = _tables.Where(t => t.GetChairs() >= guests && !t.IsBooked(resTime)).ToList<Table>();

            Reservation reservation = new Reservation(resName, resPhoneN, guests, resTime);

            //Om det er noen bord ledig..
            if (availableTable.Count > 0)
            {
                
                var selectedTable = availableTable[0]; // Ta det første bordet vi fant..
                selectedTable.BookTable(resTime); //reserver det, og sett tiden det er reservert
                reservation.SetDescription(guests, resTime); //sett description på reservasjonen
                _reservations.Add(reservation); //legg reservasjonen til den lokale listen
            }
            return reservation;
        }

        public int[] GetOpeningHours()
        {
            var hours = new int[2] {_openingHour, _closingHour};
            return hours;

        }

     
        // ------------------- Work in progress ------------------
        public string GetAllReservationsForOneDay(DateTime dtime)
        {
            //var timeSlots = new List<int>() {"16:00" };
            //dtime.Date;
            StringBuilder timeTable = new StringBuilder();
            timeTable.Append("\nBranch: "+_name+"\n\n");
            timeTable.Append(PrintTableHeaders());

            return timeTable.ToString();

        }

        string PrintTableHeaders()
        {
            StringBuilder headers = new StringBuilder();
            for (int i = 0; i < _tables.Count; i++)
            {
                if (i > 0) headers.Append("|");
                headers.Append(' ', 10).Append("Bord " + _tables[i].GetTableName()).Append(' ', 10);
            }
            return headers.ToString();
        }

        
    }




}
