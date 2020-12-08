using System;
using Inter.Infrastructure.MySQL.Contexts;

namespace Inter.Infrastructure.MySQL.Repositories
{
    public class BaseRepository<TContext>
        where TContext : DefaultContext
    {
        protected TContext Context;
        public BaseRepository(TContext context)
        {
            Context = context;

        }
    }
}
