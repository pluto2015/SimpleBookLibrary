using SimpleBookLibrary.Data;
using SimpleBookLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookLibrary.Service
{
    public interface IBookService
    {
        /// <summary>
        /// 获取书名列表
        /// </summary>
        /// <returns>书名</returns>
        List<string> GetBookNames();
        /// <summary>
        /// 按条件搜索图书
        /// </summary>
        /// <param name="bookName">书名</param>
        /// <param name="author">作者</param>
        /// <param name="department">科室</param>
        /// <param name="borrower">借阅人</param>
        /// <param name="buyTimeStart">购买时间开始</param>
        /// <param name="buyTimeEnd">购买时间截至</param>
        /// <param name="borrowTimeStart">借阅时间开始</param>
        /// <param name="borrowTimeEnd">借阅时间截止</param>
        /// <param name="returnTimeStart">归还时间开始</param>
        /// <param name="returnTimeEnd">归还时间截止</param>
        /// <returns>搜索到的图书</returns>
        List<BookEntity> SearchBooks(string bookName,string author,string department,string borrower,DateTime? buyTimeStart,DateTime? buyTimeEnd,
            DateTime? borrowTimeStart,DateTime? borrowTimeEnd,DateTime? returnTimeStart,DateTime? returnTimeEnd);
        /// <summary>
        /// 添加书籍
        /// </summary>
        /// <param name="bookName">书名</param>
        /// <param name="author">作者</param>
        /// <param name="department">部门</param>
        /// <param name="price">价格</param>
        /// <param name="buyTime">购买日期</param>
        /// <param name="count">数量</param>
        /// <param name="code">书号</param>
        void AddBook(string bookName, string author, string department,double? price,DateTime? buyTime,int count,string code, string remark, string publisher);
    }
}
