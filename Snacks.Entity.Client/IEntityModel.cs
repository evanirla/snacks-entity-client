using System;
using System.Collections.Generic;
using System.Text;

namespace Snacks.Entity.Client
{
    public interface IEntityModel
    {
        object Key { get; set; }

        string IdempotencyKey { get; set; }
    }

    public interface IEntityModel<TKey> : IEntityModel
    {
        new TKey Key { get; set; }
    }
}
