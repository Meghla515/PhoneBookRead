using Cassandra;
using Cassandra.Data.Linq;
using Cassandra.Mapping;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookReadPersistence.Repository.GenericRepository
{
    public class Repository<T> : IRepository<T>
    {
        private ISession session { get; set; }
        private IMapper mapper { get; set; }
        private Cluster cluster { get; set; }


        public Repository(IConfiguration config)
        {
            var clusters = Cluster.Builder().AddContactPoints("127.0.0.1").Build();
            session = clusters.Connect();
            mapper = new Mapper(session);
            cluster = clusters;
            GetOrCreateKeySpace();
        }

        public Table<T> CreateTableIfNotExist()
        {
            var table = new Table<T>(session);
            table.CreateIfNotExists();
            return table;
        }


        public void GetOrCreateKeySpace()
        {
            Dictionary<string, string> replication = new Dictionary<string, string>()
            {
                { "class", "SimpleStrategy" },
                { "replication_factor", "1" }
            };
            session.CreateKeyspaceIfNotExists("phonebookread", replication, true);

            session.ChangeKeyspace("phonebookread");
        }

        public T Get(string cql, params object[] args)
        {
            return mapper.FirstOrDefault<T>(cql, args);
        }

        public IEnumerable<T> GetAll(string cql, params object[] queryParams)
        {
            return mapper.Fetch<T>(cql, queryParams);
        }

        public IEnumerable<T> GetAll()
        {
            return mapper.Fetch<T>();
        }

        public IEnumerable<T> GetByCql(string cql, params object[] queryParams)
        {
            return mapper.Fetch<T>(cql, queryParams);
        }

        public T Insert(T entity)
        {
            mapper.Insert<T>(entity);
            return entity;
        }

        public void InsertBatch(IList<T> list)
        {
            if (list != null && list.Count > 0)
            {
                var batch = mapper.CreateBatch();
                foreach (var item in list)
                {
                    batch.Insert<T>(item);
                }
                mapper.Execute(batch);
            }
        }

        public void Update(T entity)
        {
            mapper.Update<T>(entity);
        }

        public void UpdateBatch(IList<T> list)
        {
            if (list != null && list.Count > 0)
            {
                var batch = mapper.CreateBatch();
                foreach (var item in list)
                {
                    batch.Update<T>(item);
                }
                mapper.Execute(batch);
            }
        }

        public void Delete(T entity)
        {
            mapper.Delete<T>(entity);
        }

        public void Execute(string query)
        {
            mapper.Execute(new Cql(query));
        }

        public void Dispose()
        {
            session.Dispose();
            cluster.Dispose();
        }
    }
}
