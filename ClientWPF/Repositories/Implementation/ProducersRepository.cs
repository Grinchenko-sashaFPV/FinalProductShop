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
            throw new NotImplementedException();
        }

        public Producer GetProducersById(int producerId)
        {
            throw new NotImplementedException();
        }

        public List<Producer> GetProducersByRateAsc()
        {
            throw new NotImplementedException();
        }

        public List<Producer> GetProducersByRateDesc()
        {
            throw new NotImplementedException();
        }
    }
}
