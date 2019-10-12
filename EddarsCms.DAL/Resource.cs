using Core.DAL;
using Core.DAL.SqlServer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.DAL
{
    public class Resource
    {



        private Resource()
        {

        }
        private static IUnitOfWork _uow;

        public static IUnitOfWork UoW
        {
            get
            {
                if (_uow == null)
                {
                    _uow = new UnitOfWork(new EddarsCmsDbContext());
                }
                return _uow;
            }
        }
    }
}
