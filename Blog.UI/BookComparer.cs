using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI
{
    public class BookComparer : IEqualityComparer<Book>
    {
        public bool Equals(Book x, Book y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] Book obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
