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
    public partial class BookModel:BaseModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        [ObservableProperty]
        private string _code;
        /// <summary>
        /// 书名
        /// </summary>
        [ObservableProperty]
        private string _name;
        /// <summary>
        /// 出版社
        /// </summary>
        [ObservableProperty]
        private string _publisher;
        /// <summary>
        /// 作者
        /// </summary>
        [ObservableProperty]
        private string _author;
        /// <summary>
        /// 价格
        /// </summary>
        [ObservableProperty]
        private double? _price;
        /// <summary>
        /// 购买日期，时间戳utc
        /// </summary>
        [ObservableProperty]
        private DateTime? _purchaseDateTime;
        /// <summary>
        /// 数量
        /// </summary>
        [ObservableProperty]
        private int _count;
        /// <summary>
        /// 科室id
        /// </summary>
        [ObservableProperty]
        private string _departmentId;
        /// <summary>
        /// 科室
        /// </summary>
        [ObservableProperty]
        private DepartmentEntity _department;
        /// <summary>
        /// 备注
        /// </summary>
        [ObservableProperty]
        private string _remark;
    }
}
