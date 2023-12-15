namespace TeamOppgaveC
{
    internal class Table
    {
        private string _name;
        private int _chairs;
        private bool _isBooked;
        private DateTime _bookingStartTime;
        private DateTime _bookingEndTime;

        public Table(string name, int chairs)
        {

            _name = name;
            _chairs = chairs;
            _isBooked = false;
            var hours = Restaurant.restaurant.GetOpeningHours();
            
            DateTime today = DateTime.Now;
            // Åpningstid hardkodet til mellom 16-20.
            // For at bookingtiden skal kunne sammenlignes riktig må man ha dagens dato +1 år, evt +1md eller +1dag
            _bookingStartTime = new DateTime(today.Year, today.Month, today.Day, hours[0], 0, 0);
            _bookingEndTime = new DateTime(today.Year+1, today.Month, today.Day, hours[1], 0, 0);

        }

        public int GetChairs()
        {
            return _chairs;
        }

        public bool IsBooked(DateTime dtime)
        {
            if (_isBooked) {return _isBooked;}

            _isBooked = !(dtime >= _bookingStartTime && dtime.AddHours(2) <= _bookingEndTime);
            
            return _isBooked;
        }


        public void BookTable(DateTime dtime) {
            _bookingStartTime = dtime; 
            _bookingEndTime = dtime.AddHours(2); // Et bestilt bord varer i 2 timer.
            _isBooked= true;
        }

        public string GetDescription()
        {
            return $"Bord {_name} har plass til {_chairs} personer";
        }

        public string GetTableName() { return _name; }
    }
}
