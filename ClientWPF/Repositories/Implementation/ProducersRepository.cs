using ModelsLibrary.Models;
using ClientWPF.Repositories.Implementation.Manager;
using ClientWPF.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ClientWPF.Repositories.Implementation
{
    public class ProducersRepository : IProducersRepository
    {
        private readonly ModelsManager _dbManager;
        public ProducersRepository()
        {
            _dbManager = new ModelsManager();
        }
        public List<Producer> GetAllProducers()
        {
            return _dbManager.Producers.ToList();
        }

        public Producer GetProducerByName(string producerName)
        {
            return _dbManager.Producers.Find(producerName);
        }

        public Producer GetProducersById(int producerId)
        {
            return _dbManager.Producers.Find(producerId);
        }

        public List<Producer> GetProducersByRateAsc()
        {
            return _dbManager.Producers.OrderBy(p => p.Rate).ToList();
        }

        public List<Producer> GetProducersByRateDesc()
        {
            return _dbManager.Producers.OrderByDescending(p => p.Rate).ToList();
        }
    }
}
