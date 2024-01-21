using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Watermark.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)] //100 karakter olacak.
        public string? Name { get; set; }
        [Column(TypeName = "decimal(18,2)")]//virgülden sonra kaç karakter olsun. cloumn ile belirtilir.toplam 18 karakter uzunluğunda olabilir 16 karakteri vigülden önce 2 karakteri virgülden sonra olacak. şekildedir.
        public decimal Price { get; set; }
        [Range(1, 100)] //1 ile 100 arasında olabilir.
        public int Stock { get; set; }
        [StringLength(100)]
        public string? ImageName { get; set; }
    }
}
