using System;

namespace NetChallenge.Domain
{
    public class Booking
    {
        private TimeSpan _duration;
        private static readonly TimeSpan MIN_DURATION = new TimeSpan(00,00,00); //TimeSpan hh-mm-ss

        public string OfficeName { get; set; }

        public DateTime DateTime { get; set; }
        public TimeSpan Duration
        {
            get { return _duration; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (TimeSpan.Compare(MIN_DURATION, value)==1) throw new ArgumentException(nameof(value), $"TimeSpan out of range min value: {MIN_DURATION}");
                _duration = value;
            }
        }
        public string UserName { get; set; }
    }
}