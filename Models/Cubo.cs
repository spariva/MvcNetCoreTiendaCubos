using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MvcNetCoreTiendaCubos.Models
{
    [Table("CUBOS")]
    public class Cubo
    {
        //        CREATE TABLE CUBOS(
        //    id_cubo INT NOT NULL PRIMARY KEY,
        //    nombre VARCHAR(500) NOT NULL,
        //    modelo VARCHAR(500) NOT NULL,
        //    marca VARCHAR(500) NOT NULL,
        //    imagen VARCHAR(500) NOT NULL,
        //    precio INT NOT NULL
        //);
        [Key]
        [Column("id_cubo")]
        public int IdCubo { get; set; }
        [Column("nombre")]
        public string Nombre { get; set; }
        [Column("modelo")]
        public string Modelo { get; set; }
        [Column("marca")]
        public string Marca { get; set; }
        [Column("imagen")]
        public string Imagen { get; set; }
        [Column("precio")]
        public int Precio { get; set; }

    }
}
