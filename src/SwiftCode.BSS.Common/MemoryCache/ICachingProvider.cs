using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftCode.BSS.Common.MemoryCache;

public interface ICachingProvider
{
    object Get(string cacheKey);

    void Set(string cacheKey, object cacheValue);
}
