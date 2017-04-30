using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drawor.Tools
{
    public class ToolsDataTImecs
    {
        public DateTime PegarHoraioDeBrasilia()
        {
            DateTime timeUtc = DateTime.UtcNow;
            TimeZoneInfo kstZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"); // Brasilia/BRA
          return TimeZoneInfo.ConvertTimeFromUtc(timeUtc, kstZone);
        }
    }
}