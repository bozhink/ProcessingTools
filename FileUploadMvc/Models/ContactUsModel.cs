namespace FileUploadMvc.Models
{
    using System.Web;

    public class ContactUsModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public HttpPostedFileBase attachment { get; set; }
    }
}
