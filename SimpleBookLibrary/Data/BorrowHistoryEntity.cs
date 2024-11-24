using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookLibrary.Data
{
    /// <summary>
    /// 借阅记录
    /// </summary>
    public class BorrowHistoryEntity:BaseEntity
    {
        /// <summary>
        /// 图书Id
        /// </summary>
        [Column(TypeName ="varchar(36)")]
        public string BookId { set; get; }
        /// <summary>
        /// 图书
        /// </summary>
        public BookEntity Book { set; get; }
        /// <summary>
        /// 借阅时间
        /// </summary>
        public long BorrowDateTime { set; get; }
        [Column(TypeName = "varchar(36)"), ForeignKey(nameof(Borrower))]
        public string BorrowerId { set; get; }
        /// <summary>
        /// 借阅人
        /// </summary>
        public BorrowerEntity Borrower { set; get; }
        /// <summary>
        /// 归还时间
        /// </summary>
        public long? ReturnDateTime { set; get; }
        /// <summary>
        /// 借阅数量
        /// </summary>
        public int BorrowCount { set; get; }
    }
}
