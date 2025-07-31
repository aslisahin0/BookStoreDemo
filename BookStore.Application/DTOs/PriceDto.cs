using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DTOs
{
    public class PriceDto
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "TRY"; // Varsayılan değer
    }

}
