using System;

namespace ATMStatusMonitoring
{
    class ATMStatus
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string Editor { get; set; }
        public string Comment { get; set; }
    }
}
