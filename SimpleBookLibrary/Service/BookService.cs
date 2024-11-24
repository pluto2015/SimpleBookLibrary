using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleBookLibrary.Data;
using SimpleBookLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookLibrary.Service
{
    public class BookService : IBookService
    {
        protected readonly DataContext _dc;
        protected readonly IDepartmentService _departmentService;
        public BookService()
        {
            _dc = App.Current.ServiceProvider.GetService<DataContext>();
            _departmentService = App.Current.ServiceProvider.GetService<IDepartmentService>();
        }

        public void AddBook(string bookName, string author, string department, double? price, DateTime? buyTime, int count, string code,string remark,string publisher)
        {
            var entity = _dc.Books.FirstOrDefault(x=>x.IsDeleted == false && x.Name.ToLower().Equals(bookName.Trim().ToLower()));
            var departmentEntity = _departmentService.GetDepartmentByName(department);
            if (entity != null)
            {
                entity.Count += count;
                entity.Updated = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                if(!string.IsNullOrEmpty(code))
                {
                    entity.Code = code.Trim();
                }
                if(!string.IsNullOrEmpty(author))
                {
                    entity.Author = author.Trim();
                }
                if(!string.IsNullOrEmpty(remark))
                {
                    entity.Remark = remark.Trim();
                }
                if(departmentEntity != null)
                {
                    entity.DepartmentId = departmentEntity.Id;
                }
                if (buyTime != null) {
                    DateTimeOffset dateTimeOffset = TimeZoneInfo.ConvertTimeToUtc(buyTime.Value);
                    entity.PurchaseDateTime = dateTimeOffset.ToUnixTimeMilliseconds();
                }
                if(!string.IsNullOrEmpty(publisher))
                {
                    entity.Publisher = publisher.Trim();
                }
                entity.Price = price;
                _dc.Books.Update(entity);
            }else
            {
                entity = new BookEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    IsDeleted = false,
                    DepartmentId = departmentEntity?.Id,
                    Author = string.IsNullOrEmpty(author) ? null : author.Trim(),
                    Code = string.IsNullOrEmpty(code) ? null : code.Trim(),
                    Count = count,
                    Created = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    Name = string.IsNullOrEmpty(bookName) ? null : bookName.Trim(),
                    Price = price,
                    Publisher = string.IsNullOrEmpty(publisher) ? null : publisher.Trim(),
                    PurchaseDateTime = buyTime == null? null : ((DateTimeOffset)(TimeZoneInfo.ConvertTimeToUtc(buyTime.Value))).ToUnixTimeMilliseconds(),
                    Remark = remark
                };
                _dc.Books.Add(entity);
            }
            _dc.SaveChanges();
        }

        public List<string> GetBookNames()
        {
            return _dc.Books.Select(book => book.Name).ToList();
        }

        public List<BookEntity> SearchBooks(string bookName, string author, string department, string borrower, DateTime? buyTimeStart, DateTime? buyTimeEnd, 
            DateTime? borrowTimeStart, DateTime? borrowTimeEnd, DateTime? returnTimeStart, DateTime? returnTimeEnd)
        {
            var fileter = _dc.Books
                .Include(x => x.Department)
                .Where(x => x.IsDeleted == false);
            if (!string.IsNullOrEmpty(bookName))
            {
                fileter = fileter.Where(x => x.Name.ToLower().Contains(bookName.Trim().ToLower()));
            }
            if (!string.IsNullOrEmpty(author)) 
            {
                fileter = fileter.Where(x => x.Author.ToLower().Contains(author.Trim().ToLower()));
            }
            if (!string.IsNullOrEmpty(department))
            {
                fileter = fileter.Where(x => x.Department != null && x.Department.Name.ToLower().Contains(department.Trim().ToLower()));
            }
            if (!string.IsNullOrEmpty(borrower))
            {
                var books =_dc.BorrowHistory.Where(x => x.Borrower != null && x.Borrower.Name.ToLower().Contains(borrower.Trim().ToLower()))
                   .DistinctBy(x =>x.Book.Name).Select(x=>x.Book.Name.ToLower())
                   .ToList();
                fileter = fileter.Where(x => books.Contains(x.Name.ToLower()));
            }
            if (buyTimeStart != null || buyTimeEnd != null)
            {
                if(buyTimeStart != null)
                {
                    DateTimeOffset dateTimeOffset = TimeZoneInfo.ConvertTimeToUtc(buyTimeStart.Value);
                    fileter = fileter.Where(x => x.PurchaseDateTime != null && x.PurchaseDateTime >= dateTimeOffset.ToUnixTimeMilliseconds());
                }
                if (buyTimeEnd != null)
                {
                    DateTimeOffset dateTimeOffset = TimeZoneInfo.ConvertTimeToUtc(buyTimeEnd.Value);
                    fileter = fileter.Where(x => x.PurchaseDateTime != null && x.PurchaseDateTime <= dateTimeOffset.ToUnixTimeMilliseconds());
                }
            }
            if (borrowTimeStart != null || borrowTimeEnd != null)
            {
                var filter1 = _dc.BorrowHistory.Where(x => x.IsDeleted == false);
                if (borrowTimeStart != null)
                {
                    DateTimeOffset dateTimeOffset = TimeZoneInfo.ConvertTimeToUtc(borrowTimeStart.Value);
                    filter1 = filter1.Where(x => x.BorrowDateTime >= dateTimeOffset.ToUnixTimeMilliseconds());
                }
                if (borrowTimeEnd != null)
                {
                    DateTimeOffset dateTimeOffset = TimeZoneInfo.ConvertTimeToUtc(borrowTimeEnd.Value);
                    filter1 = filter1.Where(x => x.BorrowDateTime <= dateTimeOffset.ToUnixTimeMilliseconds());
                }
                var books = filter1.DistinctBy(x=>x.Book.Name).Select(x=>x.Book.Name.ToLower()).ToList();
                fileter = fileter.Where(x => books.Contains(x.Name.ToLower()));
            }
            if (returnTimeStart != null || returnTimeEnd != null)
            {
                var filter1 = _dc.BorrowHistory.Where(x => x.IsDeleted == false);
                if (returnTimeStart != null)
                {
                    DateTimeOffset dateTimeOffset = TimeZoneInfo.ConvertTimeToUtc(returnTimeStart.Value);
                    filter1 = filter1.Where(x => x.ReturnDateTime!=null && x.ReturnDateTime.Value >= dateTimeOffset.ToUnixTimeMilliseconds());
                }
                if (returnTimeEnd != null)
                {
                    DateTimeOffset dateTimeOffset = TimeZoneInfo.ConvertTimeToUtc(returnTimeEnd.Value);
                    filter1 = filter1.Where(x => x.ReturnDateTime!=null && x.ReturnDateTime.Value <= dateTimeOffset.ToUnixTimeMilliseconds());
                }
                var books = filter1.DistinctBy(x => x.Book.Name).Select(x => x.Book.Name.ToLower()).ToList();
                fileter = fileter.Where(x => books.Contains(x.Name.ToLower()));
            }

            return  fileter.ToList();
        }
    }
}
