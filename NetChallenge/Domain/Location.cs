using System;
using System.Collections.Generic;

namespace NetChallenge.Domain
{
    public class Location
    {
        private string _name;
        private string _neighborhood;
        public string Name
        {
            get { return _name; }
            set 
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException(nameof(value), "Cannot be empty or null") ;
                _name = value; 
            }
        }

        public string Neighborhood
        {
            get { return _neighborhood; }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException(nameof(value), "Cannot be empty or null");
                _neighborhood = value;
            }
        }

        public IEnumerable<Office> Offices { get; set; }

    }
}