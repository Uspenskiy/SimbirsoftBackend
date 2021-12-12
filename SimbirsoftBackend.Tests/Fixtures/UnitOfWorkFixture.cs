using Core.Interfaces;
using SimbirsoftBackend.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbirsoftBackend.Tests.Fixtures
{
    public class UnitOfWorkFixture
    {
        public IUnitOfWork Create()
        {
            var mock = UnitOfWorkHelper.GetMock();
            return mock.Object;
        }
    }
}
