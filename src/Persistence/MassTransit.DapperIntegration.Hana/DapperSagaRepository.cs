namespace MassTransit
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Reflection;
    using Dapper;
    using Dapper.Contrib.Extensions;
    using DapperIntegration.Saga;
    using Saga;


    public static class DapperSagaRepository<TSaga>
        where TSaga : class, ISaga
    {
        public static ISagaRepository<TSaga> Create(string connectionString, IsolationLevel isolationLevel = IsolationLevel.Serializable)
        {
            var consumeContextFactory = new SagaConsumeContextFactory<DatabaseContext<TSaga>, TSaga>();

            var options = new DapperOptions<TSaga>(connectionString, isolationLevel);
            var repositoryContextFactory = new DapperSagaRepositoryContextFactory<TSaga>(options, consumeContextFactory);

            return new SagaRepository<TSaga>(repositoryContextFactory);
        }
    }
}
