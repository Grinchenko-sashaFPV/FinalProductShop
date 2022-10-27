using ModelsLibrary.Models;
using System.Collections.Generic;

namespace ClientWPF.Repositories.Interfaces
{
    public interface IProducersRepository
    {
        List<Producer> GetAllProducers();
        List<Producer> GetProducersByRateAsc();
        List<Producer> GetProducersByRateDesc();
        Producer GetProducersById(int producerId);
        Producer GetProducerByName(string producerName);
    }
}
