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
        public DepartmentService() { 
        }
        public DepartmentEntity GetDepartmentByName(string name)
        {
            using var dc = new DataContext();
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            var entity = dc.Department.FirstOrDefault(x =>x.IsDeleted == false && x.Name.ToLower().Equals(name.Trim().ToLower()));
            if (entity == null)
            {
                entity = new DepartmentEntity {
                    Id = Guid.NewGuid().ToString(),
                    Name = name.Trim(),
                    Created = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    IsDeleted = false,
                };
                dc.Department.Add(entity);
                dc.SaveChanges();
            }

            return entity;
        }
    }
}
