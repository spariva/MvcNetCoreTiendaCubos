using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNetCoreTiendaCubos.Models
{
    [Table("Compras")]

    public class Compra
    {
//        id_compra INT NOT NULL,
//id_cubo INT NOT NULL,
//    cantidad INT NOT NULL,
//    precio INT NOT NULL,
//    fechapedido DATETIME NOT NULL
        [Key]
        [Column("id_compra")]
        public int IdCompra { get; set; }
        [Column("id_cubo")]
        public int IdCubo { get; set; }
        [Column("cantidad")]
        public int Cantidad { get; set; }
        [Column("precio")]
        public int Precio { get; set; }
        [Column("fechapedido")]
        public DateTime FechaPedido { get; set; }
    }
}

