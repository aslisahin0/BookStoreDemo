using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.ValueObjects
{
    public record Price(decimal Amount, string Currency);
}
