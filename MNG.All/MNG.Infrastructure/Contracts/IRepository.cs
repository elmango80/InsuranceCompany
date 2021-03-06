﻿using MNG.Infrastructure.Models;

namespace MNG.Infrastructure.Contracts
{
    public interface IRepository<T> where T : class, new()
    {
        ResponseModels<T> GetData();
    }
}
