﻿using Link.ExpertManagement.Domain.Model.Entities;
using Link.ExpertManagement.Domain.Model.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Link.ExpertManagement.Infrastructure.DataAccess.MongoDb.Repositories
{
    public class ExpertRepository : IExpertRepository
    {
        private readonly IMongoCollection<Expert> _experts;

        public ExpertRepository(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("DefaultConnection"));
            var database = client.GetDatabase("LinkDb");
            _experts = database.GetCollection<Expert>("Experts");
        }

        public async Task<List<Expert>> Get()
        {
            var e = _experts;
            var experts = await _experts.Find(expert => true).ToListAsync();

            return experts;
        }

        public async Task<Expert> Get(ExpertId id)
        {
            var exp = await _experts.FindAsync(expert => expert.Id == id);
            if (exp.Current == null)
            {
                return null;
            }

            return await exp.SingleAsync();
        }

        public List<Expert> Get(List<ExpertId> ids)
        {
            var experts = new List<Expert>();
            ids.ForEach(async id => experts.Add(await Get(id)));

            return experts;
        }

        public async Task<Expert> Create(Expert expert)
        {
            await _experts.InsertOneAsync(expert);
            return expert;
        }

        public void Update(ExpertId id, Expert expert)
        {
            _experts.ReplaceOneAsync(exp => exp.Id == id, expert);
        }

        public void Remove(ExpertId expertId)
        {
            _experts.DeleteOneAsync(exp => exp.Id == expertId);
        }
    }
}
