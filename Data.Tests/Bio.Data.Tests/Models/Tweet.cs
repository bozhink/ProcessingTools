namespace ProcessingTools.Bio.Data.Tests.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Tweet
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(140)]
        public string Content { get; set; }
    }
}
