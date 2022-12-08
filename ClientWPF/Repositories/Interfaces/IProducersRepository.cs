using ModelsLibrary.Models;
using System.Collections.Generic;

namespace ClientWPF.Repositories.Interfaces
{
    public interface IProducersRepository
    {
        List<Producer> GetAllProducers();
        List<Producer> GetAllProducersByCategoryId(int categoryId);
        List<Producer> GetProducersByRateAsc();
        List<Producer> GetProducersByRateDesc();
        List<Producer> GetProducersByContaintsLetters(string phrase);
        Producer GetProducerById(int producerId);
        Producer GetProducerByName(string producerName);
        void DeleteProducersByCategoryId(int categoryId);
        void DeleteProducerById(int producerId);
        void AddProducer(Producer newProducer);
        int UpdateProducer(Producer changedProducer);
    }
}
