using System;
using MeBank.Models.Concrete;

namespace MeBank.Models
{
    public class TransferItem
    {
        public Transfer Transfer { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Currency { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}