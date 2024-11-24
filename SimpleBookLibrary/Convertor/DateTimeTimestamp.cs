using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookLibrary.Convertor
{
    public class TimestampToDateTime : IValueConverter<long, DateTime>
    {
        public DateTime Convert(long sourceMember, ResolutionContext context)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(sourceMember).LocalDateTime;
        }
    }
    public class DateTimeToTimestamp : IValueConverter<DateTime, long>
    {
        public long Convert(DateTime sourceMember, ResolutionContext context)
        {
            DateTimeOffset dateTimeOffset = TimeZoneInfo.ConvertTimeToUtc(sourceMember);
            return dateTimeOffset.ToUnixTimeMilliseconds();
        }
    }

    public class DateTimeToTimestampNull : IValueConverter<DateTime?, long?>
    {
        public long? Convert(DateTime? sourceMember, ResolutionContext context)
        {
            if(sourceMember == null)
            {
                return null;
            }else
            {
                DateTimeOffset dateTimeOffset = TimeZoneInfo.ConvertTimeToUtc(sourceMember.Value);
                return dateTimeOffset.ToUnixTimeMilliseconds();
            }
        }
    }

    public class TimestampToDateTimeNull : IValueConverter<long?, DateTime?>
    {
        public DateTime? Convert(long? sourceMember, ResolutionContext context)
        {
            if (sourceMember == null)
            {
                return null;
            }else
            {
                var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(sourceMember.Value);
                return dateTimeOffset.LocalDateTime;
            }
        }
    }
}
