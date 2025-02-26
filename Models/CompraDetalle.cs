using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNetCoreTiendaCubos.Models
{
    [Table("v_compradetalle")]
    public class CompraDetalle
    {
    //    cubos.nombre as nombrecubo, cubos.precio as preciocubo, compra.cantidad,
    //compra.precio as preciocompra, compra.fechapedido
        [Key]
        [Column("nombrecubo")]
        public string NombreCubo { get; set; }
        [Column("preciocubo")]
        public int PrecioCubo { get; set; }
        [Column("cantidad")]
        public int Cantidad { get; set; }
        [Column("preciocompra")]
        public int PrecioCompra { get; set; }
        [Column("fechapedido")]
        public DateTime FechaPedido { get; set; }


    }
}
