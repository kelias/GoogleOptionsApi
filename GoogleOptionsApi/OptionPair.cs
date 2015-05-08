using System;

namespace GoogleOptionsApi
{
    public class OptionPair
    {
        public decimal Strike { get; set; }
        public DateTime Expiry { get; set; }
        public Option Call { get; set; }
        public Option Put { get; set; }
    }
}