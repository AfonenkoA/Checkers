using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkers.Data.Entity;

internal static class EntityValues
{
    public const int InvalidId = -1;
    public const string InvalidString = "";
    public static readonly IEnumerable<int> InvalidEnumerable = Enumerable.Empty<int>();
    public static readonly DateTime InvalidDate = DateTime.MinValue;
}