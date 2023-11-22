namespace E_Test.Security
{
    public  class TokenDecoder
    {
        public  string Id { get; set; }
        public  string Issuer { get; set; }
        public  DateTime ValidFrom { get; set; }
        public  DateTime ValidTo { get; set; }
        public string RoleName { get; set; }
        public string username { get; set; }


    }
}
