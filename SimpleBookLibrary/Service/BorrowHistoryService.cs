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
        protected readonly DataContext _dc;
        public BorrowHistoryService()
        {
            _dc = App.Current.ServiceProvider.GetService<DataContext>();
        }

        public List<BorrowHistoryEntity> SearchBorrowHistory(string bookName)
        {
            return _dc.BorrowHistory.Include(x => x.Borrower)
                .Include(x => x.Book)
                .Where(x => x.Book.Name.ToLower().Equals(bookName.ToLower()))
                .OrderByDescending(x=>x.ReturnDateTime)
                .ThenByDescending(x=>x.BorrowDateTime)
                .ToList();
        }
    }
}
