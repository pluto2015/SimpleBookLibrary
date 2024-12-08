using SimpleBookLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookLibrary.Service
{
    public class BorrowerService:IBorrowerService
    {
        public BorrowerEntity AddBorrower(string name)
        {
            using var dc = new DataContext();
            var entity = dc.Borrowers.Where(x=>x.Name.ToLower().Equals(name.Trim().ToLower())).FirstOrDefault();
            if (entity == null)
            {
                entity = new BorrowerEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name.Trim(),
                    IsDeleted = false,
                    Created = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()

                };
                dc.Borrowers.Add(entity);
                dc.SaveChanges();
            }

            return entity;
        }

        public List<BorrowerEntity> SearchBorrowers(string name)
        {
            using var dc = new DataContext();
            return dc.Borrowers.Where(x => !x.IsDeleted && x.Name.ToLower().Contains(name.Trim().ToLower())).ToList();
        }
    }
}
