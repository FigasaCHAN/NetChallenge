using System.Collections.Generic;
using NetChallenge.Abstractions;
using NetChallenge.Domain;

namespace NetChallenge.Infrastructure
{
    public class OfficeRepository : IOfficeRepository
    {
        private List<Office> _repository = new List<Office>() { };

        public IEnumerable<Office> AsEnumerable()
        {
            return _repository;
        }

        public void Add(Office item)
        {
            _repository.Add(item);
        }
    }
}