namespace GoogleOptionsApi
{
    public class OptionData
    {
        public Expiry expiry { get; set; }
        public Expiration[] expirations { get; set; }
        public Option[] puts { get; set; }
        public Option[] calls { get; set; }
        public string underlying_id { get; set; }
        public string underlying_price { get; set; }
    }
}