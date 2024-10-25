
namespace ArticleManufacturerService.Infrastructure.HttpClients.TecDoc.DTOs
{
    
    public class DataResponse
    {
        public List<AddressResponse> Array { get; set; }
    }

    public class AddressResponse
    {
        public string AddressName { get; set; }
        public int AddressType { get; set; }
        public string City { get; set; }
        public string City2 { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string LogoDocId { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string WwwURL { get; set; }
        public string Zip { get; set; }
        public string ZipCountryCode { get; set; }
    }

}
