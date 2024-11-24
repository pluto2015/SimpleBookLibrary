using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SimpleBookLibrary.Model
{
    public partial class BaseModel:ObservableObject
    {
        /// <summary>
        /// 主键，guid
        /// </summary>
        [ObservableProperty]
        private string _id;
        /// <summary>
        /// 创建时间,时间戳
        /// </summary>
        [ObservableProperty]
        private DateTime _created;
        /// <summary>
        /// 更新时间,时间戳
        /// </summary>
        [ObservableProperty]
        private DateTime? _updated;
        /// <summary>
        /// 软删除
        /// </summary>
        [ObservableProperty]
        private bool _isDeleted;
    }
}
