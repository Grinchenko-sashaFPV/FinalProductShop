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

        public void AddProducer(Producer newProducer)
        {
            _dbManager.Producers.Add(newProducer);
            _dbManager.SaveChanges();
        }

        public void DeleteProducerById(int producerId)
        {
            _dbManager.Producers.Remove(GetProducerById(producerId));
            _dbManager.SaveChanges();
        }

        public void DeleteProducersByCategoryId(int categoryId)
        {
            foreach (var producer in _dbManager.Producers)
            {
                if(producer.CategoryId == categoryId)
                    _dbManager.Producers.Remove(producer);
            }
            _dbManager.SaveChanges();
        }

        public List<Producer> GetAllProducers()
        {
            return _dbManager.Producers.ToList();
        }

        public List<Producer> GetAllProducersByCategoryId(int categoryId)
        {
            return _dbManager.Producers.Where(p => p.CategoryId == categoryId).ToList();
        }

        public Producer GetProducerByName(string producerName)
        {
            return _dbManager.Producers.Find(producerName);
        }

        public List<Producer> GetProducersByContaintsLetters(string phrase)
        {
            return _dbManager.Producers.Where(p => p.Name.Contains(phrase)).ToList();
        }

        public Producer GetProducerById(int producerId)
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

        public int UpdateProducer(Producer changedProducer)
        {
            _dbManager.Producers.Find(changedProducer.Id).Name = changedProducer.Name;
            return _dbManager.SaveChanges();
        }
    }
}
