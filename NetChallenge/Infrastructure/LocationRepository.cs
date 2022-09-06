using System.Collections.Generic;
using NetChallenge.Abstractions;
using NetChallenge.Domain;

namespace NetChallenge.Infrastructure
{
    public class LocationRepository : ILocationRepository
    {
        private List<Location> _repository = new List<Location>() { };
        public IEnumerable<Location> AsEnumerable()
        {
            return _repository;
        }

        public void Add(Location item)
        {
            _repository.Add(item);
        }
    }
}