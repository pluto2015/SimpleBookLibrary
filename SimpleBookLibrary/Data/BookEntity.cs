using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookLibrary.Data
{
    /// <summary>
    /// 图书
    /// </summary>
    public class BookEntity:BaseEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Column(TypeName ="varchar(20)")]
        public string Code { set; get; }
        /// <summary>
        /// 书名
        /// </summary>
        [Column(TypeName = "varchar(500)")]
        public string Name { set; get; }
        /// <summary>
        /// 出版社
        /// </summary>
        [Column(TypeName = "varchar(500)")]
        public string Publisher { set; get; }
        /// <summary>
        /// 作者
        /// </summary>
        [Column(TypeName = "varchar(500)")]
        public string Author { set; get; }
        /// <summary>
        /// 价格
        /// </summary>
        public double? Price { set; get; }
        /// <summary>
        /// 购买日期，时间戳utc
        /// </summary>
        public long? PurchaseDateTime { set; get; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { set; get; }
        /// <summary>
        /// 科室id
        /// </summary>
        [ForeignKey(nameof(Department))]
        [Column(TypeName = "varchar(36)")]
        public string DepartmentId { set; get; }
        /// <summary>
        /// 科室
        /// </summary>
        public DepartmentEntity Department { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column(TypeName = "varchar(1000)")]
        public string Remark { set; get; }
    }
}
