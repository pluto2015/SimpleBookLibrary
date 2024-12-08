using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleBookLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookLibrary.Service
{
    public class BorrowHistoryService : IBorrowHistoryService
    {
        public BorrowHistoryService()
        {
        }

        public void AddBorrowHistory(string bookId, string borrowerId, int count)
        {
            using var dc = new DataContext();
            var timestemp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var entity = new BorrowHistoryEntity
            {
                Id = Guid.NewGuid().ToString(),
                IsDeleted = false,
                BookId = bookId,
                BorrowerId = borrowerId,
                BorrowCount = count,
                BorrowDateTime = timestemp,
                Created = timestemp
            };
            dc.BorrowHistory.Add(entity);
            dc.SaveChanges();
        }

        public List<BorrowHistoryEntity> SearchBorrowHistory(string bookName)
        {
            using var dc = new DataContext();
            return dc.BorrowHistory.Include(x => x.Borrower)
                .Include(x => x.Book)
                .Where(x => x.Book.Name.ToLower().Equals(bookName.ToLower()))
                .OrderByDescending(x=>x.ReturnDateTime)
                .ThenByDescending(x=>x.BorrowDateTime)
                .ToList();
        }
    }
}
