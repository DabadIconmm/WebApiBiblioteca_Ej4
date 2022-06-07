namespace WebApiBiblioteca.DTOs
{
    // 4.4.a
    public class DTOEditorialLibro
    {
        public int IdEditorial { get; set; }
        public string Nombre { get; set; }
        public List<DTOLibroItem> Libros { get; set; }
    }

    public class DTOLibroItem
    {
        public int IdLibro { get; set; }
        public string Nombre { get; set; }
        public int Paginas { get; set; }
    }
}
