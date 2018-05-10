namespace Falla2018.Models
{
    using System;

    public class Act
    {
        public int IdAct { get; set; }
        public string Titular { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaActo { get; set; }
        public string HoraActo { get; set; }
        public string Precio { get; set; }
        public string PrecioInfantiles { get; set; }
        public bool ActoOficial { get; set; }
        public string Imagen { get; set; }
        public string ImagenFullPath
        {
            get
            {
                return string.Format(
                    "http://aacvalencia.es/Falla2018/{0}",
                    Imagen500.Substring(1));
            }
        }
        public string Imagen500 { get; set; }
        public bool PagInicio { get; set; }
        public bool YaEfectuado { get; set; }
    }
}
