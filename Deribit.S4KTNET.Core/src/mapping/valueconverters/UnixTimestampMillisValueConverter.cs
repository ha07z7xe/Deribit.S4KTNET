using System;
using System.Collections.Generic;
using System.Linq;
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

    class ArrayUnixTimestampMillisValueConveter : IValueConverter<IList<long>, IList<DateTime>>
    {
        public IList<DateTime> Convert(IList<long> sourceMember, ResolutionContext context)
        {
            return sourceMember.Select(x => DeribitMappingExtensions.UnixTimeStampMillisToDateTimeUtc(x)).ToArray();
        }
    }
}
