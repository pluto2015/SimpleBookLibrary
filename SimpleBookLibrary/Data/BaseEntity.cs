using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookLibrary.Data
{
    public class BaseEntity
    {
        /// <summary>
        /// 主键，guid
        /// </summary>
        [Key]
        [Column(TypeName = "varchar(36)")]
        public string Id { set; get; }
        /// <summary>
        /// 创建时间,时间戳
        /// </summary>
        public long Created { get; set; }
        /// <summary>
        /// 更新时间,时间戳
        /// </summary>
        public long? Updated { set; get; }
        /// <summary>
        /// 软删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
