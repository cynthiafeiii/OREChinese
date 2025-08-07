using System.ComponentModel.DataAnnotations.Schema;

namespace OREChinese.Data
{
    [Table("Video")]
    public class Video
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AdminId { get; set; } = 1;
        public int UnitId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string ImageName { get; set; }

        //public IFormFile Image { get; set; }
    }


}
