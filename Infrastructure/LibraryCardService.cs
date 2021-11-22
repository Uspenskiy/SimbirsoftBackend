using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class LibraryCardService : ILibraryCardService
    {

        public async Task<bool> TakeBook(Book book, Person person)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ReturnBook(Book book, Person person)
        {
            throw new NotImplementedException();
        }
    }
}
