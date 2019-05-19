using System;
using AutoMapper;

namespace Deribit.S4KTNET.Core.Mapping
{
    class UnixTimestampMillisValueConverter : IValueConverter<long, DateTime>, IValueConverter<DateTime, long>
    {
        public DateTime Convert(long sourceMember, ResolutionContext context)
        {
            return sourceMember.UnixTimeStampMillisToDateTimeUtc();
        }

        public long Convert(DateTime sourceMember, ResolutionContext context)
        {
            return sourceMember.UnixTimeStampDateTimeUtcToMillis();
        }
    }
}
