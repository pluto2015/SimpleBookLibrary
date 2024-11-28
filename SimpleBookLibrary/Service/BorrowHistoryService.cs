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
