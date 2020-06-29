using System;

namespace ATMStatusMonitoring
{
    class ATMStatus
    {
        public int Id { get; set; }
        //public string Number { get; set; } 
        //TODO: при entity возможно не верно задана модель. Уточнить
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string Editor { get; set; }
        public string Comment { get; set; }
    }
}
