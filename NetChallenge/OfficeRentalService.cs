using System;
using System.Collections.Generic;
using System.Linq;
using NetChallenge.Abstractions;
using NetChallenge.Domain;
using NetChallenge.Dto.Input;
using NetChallenge.Dto.Output;

namespace NetChallenge
{
    public class OfficeRentalService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IBookingRepository _bookingRepository;

        public OfficeRentalService(ILocationRepository locationRepository, IOfficeRepository officeRepository, IBookingRepository bookingRepository)
        {
            _locationRepository = locationRepository;
            _officeRepository = officeRepository;
            _bookingRepository = bookingRepository;
        }

        public void AddLocation(AddLocationRequest request)
        {
            IEnumerable<Location> locations = _locationRepository.AsEnumerable();

            bool exists = (from location in locations where location.Name == request.Name select location).FirstOrDefault() != null;

            if (exists) throw new Exception("The location entered already exists");

            _locationRepository.Add(new Location() { Name = request.Name, Neighborhood = request.Neighborhood });
        }

        public void AddOffice(AddOfficeRequest request)
        {
            IEnumerable<Location> locations = _locationRepository.AsEnumerable();

            bool existsLocation = (from location in locations where location.Name == request.LocationName select location).FirstOrDefault() != null;
            if (!existsLocation) throw new Exception("The location does not exist");

            IEnumerable<Office> offices = _officeRepository.AsEnumerable();
            bool existsOffice = (from office in offices where office.Name == request.Name && office.LocationName == request.LocationName select office).FirstOrDefault() != null;
            if (existsOffice) throw new Exception("The office entered already exists");

            _officeRepository.Add(new Office() { LocationName = request.LocationName, Name = request.Name, MaxCapacity = request.MaxCapacity, AvailableResources = (string[]) request.AvailableResources  });; 
        }

        public void BookOffice(BookOfficeRequest request)
        {
            
            IEnumerable<Booking> bookings = _bookingRepository.AsEnumerable(); 
            var offices = GetOffices(request.LocationName);

            var officeFilter = (from office in offices where office.Name == request.OfficeName && office.LocationName == request.LocationName select office);

            bool existsOffice = officeFilter.FirstOrDefault() != null;
            if (!existsOffice) throw new Exception("The office does not exist");


            var officeBookings = GetBookings(request.LocationName, request.OfficeName);

            var overwriteBookings = (from booking in officeBookings 
                          where (booking.DateTime >= request.DateTime && booking.DateTime < request.DateTime.Add(request.Duration) ) || 
                          (request.DateTime >= booking.DateTime && request.DateTime < booking.DateTime.Add(booking.Duration))
                          select booking);

            bool isOverwrite = overwriteBookings.FirstOrDefault() != null;
            if(isOverwrite) throw new Exception("The office is already reserved");
            _bookingRepository.Add(new Booking() { LocationName = request.LocationName, UserName = request.UserName, OfficeName = request.OfficeName, DateTime = request.DateTime, Duration = request.Duration  } );

        }

        public IEnumerable<BookingDto> GetBookings(string locationName, string officeName)
        {
            List<BookingDto> bookingDtos = new List<BookingDto>();

            var bookings = _bookingRepository.AsEnumerable();

            var filterBookings = (from booking in bookings where booking.LocationName == locationName && booking.OfficeName == officeName select booking);

            foreach (var item in filterBookings)
            {
                bookingDtos.Add(new BookingDto() { LocationName = item.LocationName, OfficeName = item.OfficeName, UserName = item.UserName, DateTime = item.DateTime, Duration = item.Duration} );
            }


            return bookingDtos;
        }

        public IEnumerable<LocationDto> GetLocations()
        {
            List<LocationDto> locations = new List<LocationDto>();
            foreach (var item in _locationRepository.AsEnumerable())
            {
                locations.Add(new LocationDto() { Name = item.Name, Neighborhood = item.Neighborhood });
            }
            return locations;
        }

        public IEnumerable<OfficeDto> GetOffices(string locationName)
        {
   
            List<OfficeDto> officesDTO = new List<OfficeDto>();
            var offices = _officeRepository.AsEnumerable();

            var filterOffices = from office in offices where office.LocationName == locationName select office;

            foreach (var item in filterOffices)
            {
                officesDTO.Add(new OfficeDto() { LocationName = item.LocationName, Name = item.Name, MaxCapacity = item.MaxCapacity, AvailableResources = item.AvailableResources} );
            }
            return officesDTO;
        }

        public IEnumerable<OfficeDto> GetOfficeSuggestions(SuggestionsRequest request)
        {
            throw new NotImplementedException();
        }
    }
}