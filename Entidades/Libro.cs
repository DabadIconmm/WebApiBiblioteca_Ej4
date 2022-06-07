using System.ComponentModel.DataAnnotations;

namespace Ejercicio_Sesión_1.Entidades
{
    public class Libro
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; } //añadir limite de caracteres 150max
        public int Paginas { get; set; } //rango entre 1 - 10000
        public int EditorialId { get; set; }
        public Editorial Editorial { get; set; }
    }
}
