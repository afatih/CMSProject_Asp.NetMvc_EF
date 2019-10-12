using Core.Results;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DAL.SqlServer.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, new()
        {
            return new Repository<TEntity>(_dbContext);
        }

        public ServiceResult Save()
        {
          DbContextTransaction transaction=  _dbContext.Database.BeginTransaction();
            int ess=0;
            try
            {
              ess=  _dbContext.SaveChanges();
                transaction.Commit();
                if (ess>0)
                {
                    return new ServiceResult(ProcessStateEnum.Success, "İşlem başarılı");
                }
                else
                {
                    return new ServiceResult(ProcessStateEnum.Warning, "İşlem başarısız");
                }
                
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return new ServiceResult(ProcessStateEnum.Error, ex.Message);
            }
        }
    }
}
