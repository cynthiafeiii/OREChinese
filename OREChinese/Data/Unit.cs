using System.ComponentModel.DataAnnotations.Schema;

namespace OREChinese.Data
{
    [Table("Unit")]
    public class Unit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }

    }
}
