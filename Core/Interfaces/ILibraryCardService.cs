using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ILibraryCardService
    {
        Task<Boolean> TakeBook(Book book, Person person);

        Task<Boolean> ReturnBook(Book book, Person person);
    }
}
