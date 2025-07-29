using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.ValueObjects
{
    namespace BookStore.Core.ValueObjects
    {
        public record Author
        {
            public string FirstName { get; }
            public string LastName { get; }
            public string FullName => $"{FirstName} {LastName}";

            public Author(string firstName, string lastName)
            {
                if (string.IsNullOrWhiteSpace(firstName))
                    throw new ArgumentException("Ad boş olamaz.", nameof(firstName));

                if (string.IsNullOrWhiteSpace(lastName))
                    throw new ArgumentException("Soyad boş olamaz.", nameof(lastName));

                FirstName = firstName.Trim();
                LastName = lastName.Trim();
            }
        }

        public record Price
        {
            public decimal Amount { get; }
            public string Currency { get; }

            public Price(decimal amount, string currency = "TRY")
            {
                if (amount < 0)
                    throw new ArgumentException("Fiyat negatif olamaz.");

                if (string.IsNullOrWhiteSpace(currency))
                    throw new ArgumentException("Para birimi boş olamaz.");

                Amount = amount;
                Currency = currency.ToUpperInvariant();
            }

            public override string ToString()
            {
                return $"{Amount:F2} {Currency}";
            }
        }
    }

        

}
