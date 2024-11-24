using Microsoft.Extensions.DependencyInjection;
using SimpleBookLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookLibrary.Service
{
    public class DepartmentService : IDepartmentService
    {
        protected readonly DataContext _dc;
        public DepartmentService() { 
            _dc = App.Current.ServiceProvider.GetService<DataContext>();
        }
        public DepartmentEntity GetDepartmentByName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return null;
            }
            var entity = _dc.Department.FirstOrDefault(x =>x.IsDeleted == false && x.Name.ToLower().Equals(name.Trim().ToLower()));
            if (entity == null)
            {
                entity = new DepartmentEntity {
                    Id = Guid.NewGuid().ToString(),
                    Name = name.Trim(),
                    Created = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    IsDeleted = false,
                };
                _dc.Department.Add(entity);
                _dc.SaveChanges();
            }

            return entity;
        }
    }
}
