using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CoreDemo.Models
{
    public class AddWriterImage
    {
        public int WriterId { get; set; }
        public string WriterName { get; set; }
        public string WriterAbout { get; set; }
        public string WriterCity { get; set; }
        public IFormFile WriterImage { get; set; }
        public bool WriterStatus { get; set; }
        public string WriterMail { get; set; }
        [DataType(DataType.Password)]
        public string WriterPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("WriterPassword", ErrorMessage = "Şifreler eşleşmiyor. Lütfen tekrar deneyin!")]
        public string ConfirmPassword { get; set; }
    }
}
