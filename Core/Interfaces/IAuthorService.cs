using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAuthorService
    {
        Task<Author> GetAuthor(string name, string lastName, string middlename);
    }
}
