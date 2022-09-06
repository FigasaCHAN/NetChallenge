using System.Collections.Generic;
using NetChallenge.Abstractions;
using NetChallenge.Domain;

namespace NetChallenge.Infrastructure
{
    public class BookingRepository : IBookingRepository
    {
        private List<Booking> _repository = new List<Booking>(){};

        public IEnumerable<Booking> AsEnumerable()
        {
            return _repository;
        }

        public void Add(Booking item)
        {
            _repository.Add(item);
        }
    }
}