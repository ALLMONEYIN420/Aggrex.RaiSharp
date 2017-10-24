using System;
using System.Collections.Generic;
using System.Text;

namespace Aggrex.Database.Models
{
    public class TransferTransactionModel : IDataModel
    {
        public int Id { get; set; }
        public double Amount { get; set; }
    }
}
