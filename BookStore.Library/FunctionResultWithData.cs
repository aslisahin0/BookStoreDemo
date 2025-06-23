using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Library
{
    public class FunctionResultWithData<T> : FunctionResult
    {
        public T Data { get; set; }
    }
}
