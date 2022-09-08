using System;

namespace NetChallenge.Domain
{
    public class Booking
    {
        private TimeSpan _duration;
        private string _userName;
        private static readonly TimeSpan MIN_DURATION = new TimeSpan(00,00,00); //TimeSpan hh-mm-ss

        public string LocationName { get; set; }
        public string OfficeName { get; set; }

        public DateTime DateTime { get; set; }
        public TimeSpan Duration
        {
            get { return _duration; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (TimeSpan.Compare(MIN_DURATION, value)>=0) throw new ArgumentException(nameof(value), $"TimeSpan out of range min value: {MIN_DURATION}"); //If TimeSpan.Compare(x,y) >= 0 then x is <= y
                _duration = value;
            }
        }
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException(nameof(value), "Cannot be empty or null");
                _userName = value;
            }
        }
    }
}