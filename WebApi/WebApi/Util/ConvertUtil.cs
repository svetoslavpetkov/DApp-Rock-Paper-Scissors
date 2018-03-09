using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Util
{
    public static class ConvertUtil
    {
        public static DateTime GetDate(long solidityValue)
        {
            return new DateTime(1970, 1, 1).AddSeconds(solidityValue).ToLocalTime();
        }

        public static string GetDateString(long solidityValue)
        {
            return GetDate(solidityValue).ToString("yyyy/M/d hh:mm");
        }

    }
}
