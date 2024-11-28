using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookLibrary.Model
{
    /// <summary>
    /// 搜索条件
    /// </summary>
    public partial class SearchBookModel:ObservableObject
    {
        /// <summary>
        /// 搜索书名
        /// </summary>
        [ObservableProperty]
        private string _searchBookName;
        /// <summary>
        /// 搜索作者名
        /// </summary>
        [ObservableProperty]
        private string _searchAuthor;
        /// <summary>
        /// 搜索科室
        /// </summary>
        [ObservableProperty]
        private string _searchDepartment;
        /// <summary>
        /// 搜索借阅人
        /// </summary>
        [ObservableProperty]
        private string _searchBorrower;
        /// <summary>
        /// 搜索购买时间-开始
        /// </summary>
        [ObservableProperty]
        private DateTime? _searchBuyStartDate;
        /// <summary>
        /// 搜索购买时间-截至
        /// </summary>
        [ObservableProperty]
        private DateTime? _searchBuyEndDate;
        /// <summary>
        /// 搜索借阅时间-开始
        /// </summary>
        [ObservableProperty]
        private DateTime? _searchBorrowStartDate;
        /// <summary>
        /// 搜索借阅时间-截至
        /// </summary>
        [ObservableProperty]
        private DateTime? _searchBorrowEndDate;
        /// <summary>
        /// 搜索归还时间-开始
        /// </summary>
        [ObservableProperty]
        private DateTime? _searchReturnStartDate;
        /// <summary>
        /// 搜索归还时间-截至
        /// </summary>
        [ObservableProperty]
        private DateTime? _searchReturnEndDate;
    }
}
