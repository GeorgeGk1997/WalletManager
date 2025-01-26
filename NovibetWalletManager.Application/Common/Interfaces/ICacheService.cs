﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Application.Common.Interfaces
{
    public interface ICacheService
    {
        Task SetAsync<T>(string key, T value, TimeSpan expiration);
        Task<T> GetAsync<T>(string key);
    }
}
