using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookLibrary.Data
{
    public class BorrowerEntity:BaseEntity
    {
        /// <summary>
        /// 借阅人
        /// </summary>
        [Column(TypeName = "varchar(500)")]
        public string Name { set; get; }
    }
}
