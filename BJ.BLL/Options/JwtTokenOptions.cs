namespace BJ.BLL.Options
{
    public class JwtTokenOptions
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public int ExpireDays { get; set; }
    }
}
