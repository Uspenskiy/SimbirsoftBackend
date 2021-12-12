using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbirsoftBackend.Tests.Helpers
{
    public static class UnitOfWorkHelper
    {
        internal static Mock<IUnitOfWork> GetMock()
        {
            var context = new DbContextHelper().Context;
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(s => s.Repository<BaseEntity>())
                .Returns(new GenericRepository<BaseEntity>(context));
            return unitOfWork;
        }
    }
}
