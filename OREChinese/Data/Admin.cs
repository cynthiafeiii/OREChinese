using System.ComponentModel.DataAnnotations.Schema;

namespace OREChinese.Data
{
    [Table("Admin")]
    public class Admin
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public bool IsDeleted { get; set; }
    }
}
