using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookLibrary.Data
{
    /// <summary>
    /// 数据库迁移
    /// </summary>
    public static class DBMigration
    {
        /// <summary>
        /// 数据库迁移
        /// </summary>
        public static void Migrate()
        {
            using var dc = new DataContext();
            dc.Database.Migrate();
        }
    }
}
