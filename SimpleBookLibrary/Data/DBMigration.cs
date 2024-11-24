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
            var dc = App.Current.ServiceProvider.GetService<DataContext>();
            dc.Database.Migrate();
        }
    }
}
