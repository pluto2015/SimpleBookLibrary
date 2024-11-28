using CommunityToolkit.Mvvm.ComponentModel;
using SimpleBookLibrary.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookLibrary.Model
{
    public partial class BorrowHistoryModel:BaseModel
    {
        /// <summary>
        /// 图书Id
        /// </summary>
        [ObservableProperty]
        private string _bookId;
        /// <summary>
        /// 图书
        /// </summary>
        [ObservableProperty]
        private BookModel _book;
        /// <summary>
        /// 借阅时间
        /// </summary>
        [ObservableProperty]
        private DateTime _borrowDateTime;
        [ObservableProperty]
        private string _borrowerId;
        /// <summary>
        /// 借阅人
        /// </summary>
        [ObservableProperty]
        private string _borrower;
        /// <summary>
        /// 归还时间
        /// </summary>
        [ObservableProperty]
        private DateTime? _returnDateTime;
        /// <summary>
        /// 借阅数量
        /// </summary>
        [ObservableProperty]
        private int _borrowCount;
    }
}
