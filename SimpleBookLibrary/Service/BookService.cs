using AutoMapper;
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
        protected readonly IDepartmentService _departmentService;
        protected readonly IMapper _mapper;
        public BookService()
        {
            _departmentService = App.Current.ServiceProvider.GetService<IDepartmentService>();
            _mapper = App.Current.ServiceProvider.GetService<IMapper>();
        }

        public void AddBook(string bookName, string author, string department, double? price, DateTime? buyTime, int count, string code,string remark,string publisher)
        {
            using var dc = new DataContext();
            var entity = dc.Books.FirstOrDefault(x=>x.IsDeleted == false && x.Name.ToLower().Equals(bookName.Trim().ToLower()));
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
                dc.Books.Update(entity);
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
                dc.Books.Add(entity);
            }
            dc.SaveChanges();
        }

        public void EditBook(BookModel book)
        {
            using var dc = new DataContext();
            var entity = _mapper.Map<BookEntity>(book);
            entity.Updated = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            dc.Books.Update(entity);
            dc.SaveChanges();
        }

        public BookEntity GetBookByName(string name)
        {
            using var dc = new DataContext();
            var entity = dc.Books.Where(x=>x.IsDeleted==false && x.Name.ToLower().Equals(name.ToLower())).FirstOrDefault();
            return entity;
        }

        public List<string> GetBookNames()
        {
            using var dc = new DataContext();
            return dc.Books.Select(book => book.Name).ToList();
        }

        public List<BookEntity> SearchBooks(SearchBookModel searchBook)
        {
            using var dc = new DataContext();
            var fileter = dc.Books
                .Include(x => x.Department)
                .Where(x => x.IsDeleted == false);
            if (!string.IsNullOrEmpty(searchBook.SearchBookName))
            {
                fileter = fileter.Where(x => x.Name.ToLower().Contains(searchBook.SearchBookName.Trim().ToLower()));
            }
            if (!string.IsNullOrEmpty(searchBook.SearchAuthor)) 
            {
                fileter = fileter.Where(x => x.Author.ToLower().Contains(searchBook.SearchAuthor.Trim().ToLower()));
            }
            if (!string.IsNullOrEmpty(searchBook.SearchDepartment))
            {
                fileter = fileter.Where(x => x.Department != null && x.Department.Name.ToLower().Contains(searchBook.SearchDepartment.Trim().ToLower()));
            }
            if (!string.IsNullOrEmpty(searchBook.SearchBorrower))
            {
                var books =dc.BorrowHistory.Where(x => x.Borrower != null && x.Borrower.Name.ToLower().Contains(searchBook.SearchBorrower.Trim().ToLower()))
                   .DistinctBy(x =>x.Book.Name).Select(x=>x.Book.Name.ToLower())
                   .ToList();
                fileter = fileter.Where(x => books.Contains(x.Name.ToLower()));
            }
            if (searchBook.SearchBuyStartDate != null || searchBook.SearchBuyEndDate != null)
            {
                if(searchBook.SearchBuyStartDate != null)
                {
                    DateTimeOffset dateTimeOffset = TimeZoneInfo.ConvertTimeToUtc(searchBook.SearchBuyStartDate.Value);
                    fileter = fileter.Where(x => x.PurchaseDateTime != null && x.PurchaseDateTime >= dateTimeOffset.ToUnixTimeMilliseconds());
                }
                if (searchBook.SearchBuyEndDate != null)
                {
                    DateTimeOffset dateTimeOffset = TimeZoneInfo.ConvertTimeToUtc(searchBook.SearchBuyEndDate.Value);
                    fileter = fileter.Where(x => x.PurchaseDateTime != null && x.PurchaseDateTime <= dateTimeOffset.ToUnixTimeMilliseconds());
                }
            }
            if (searchBook.SearchBorrowStartDate != null || searchBook.SearchBorrowEndDate != null)
            {
                var filter1 = dc.BorrowHistory.Where(x => x.IsDeleted == false);
                if (searchBook.SearchBorrowStartDate != null)
                {
                    DateTimeOffset dateTimeOffset = TimeZoneInfo.ConvertTimeToUtc(searchBook.SearchBorrowStartDate.Value);
                    filter1 = filter1.Where(x => x.BorrowDateTime >= dateTimeOffset.ToUnixTimeMilliseconds());
                }
                if (searchBook.SearchBorrowEndDate != null)
                {
                    DateTimeOffset dateTimeOffset = TimeZoneInfo.ConvertTimeToUtc(searchBook.SearchBorrowEndDate.Value);
                    filter1 = filter1.Where(x => x.BorrowDateTime <= dateTimeOffset.ToUnixTimeMilliseconds());
                }
                var books = filter1.DistinctBy(x=>x.Book.Name).Select(x=>x.Book.Name.ToLower()).ToList();
                fileter = fileter.Where(x => books.Contains(x.Name.ToLower()));
            }
            if (searchBook.SearchReturnStartDate != null || searchBook.SearchReturnEndDate != null)
            {
                var filter1 = dc.BorrowHistory.Where(x => x.IsDeleted == false);
                if (searchBook.SearchReturnStartDate != null)
                {
                    DateTimeOffset dateTimeOffset = TimeZoneInfo.ConvertTimeToUtc(searchBook.SearchReturnStartDate.Value);
                    filter1 = filter1.Where(x => x.ReturnDateTime!=null && x.ReturnDateTime.Value >= dateTimeOffset.ToUnixTimeMilliseconds());
                }
                if (searchBook.SearchReturnEndDate != null)
                {
                    DateTimeOffset dateTimeOffset = TimeZoneInfo.ConvertTimeToUtc(searchBook.SearchReturnEndDate.Value);
                    filter1 = filter1.Where(x => x.ReturnDateTime!=null && x.ReturnDateTime.Value <= dateTimeOffset.ToUnixTimeMilliseconds());
                }
                var books = filter1.DistinctBy(x => x.Book.Name).Select(x => x.Book.Name.ToLower()).ToList();
                fileter = fileter.Where(x => books.Contains(x.Name.ToLower()));
            }

            return  fileter.ToList();
        }

    }
}
