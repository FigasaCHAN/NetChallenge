using System;

namespace NetChallenge.Domain
{
    public class Office
    {
        private string _name;
        private int _maxCapacity;
        private string[] _availableResources;

        private const int MIN_CAPACITY = 1;
        private const int MIN_RESOURCES = 0;


        public Location Location { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException(nameof(value), "Cannot be empty or null");
                _name = value;
            }
        }

        public int MaxCapacity
        {
            get { return _maxCapacity; }
            set
            {
                if(value<MIN_CAPACITY) throw new ArgumentOutOfRangeException(nameof(value),$"The minimum value is {MIN_CAPACITY}.");
                _maxCapacity = value;
            }
        }
        public string[] AvailableResources
        {
            get { return _availableResources; }
            set
            {
                if(value == null) throw new ArgumentNullException(nameof(value));
                if(value.Length < MIN_RESOURCES) throw new ArgumentOutOfRangeException(nameof(value), $"The minimum length value is {MIN_RESOURCES}.");
                _availableResources = value;
            }
        }
    }
}