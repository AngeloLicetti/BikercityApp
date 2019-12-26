using System.Collections.Generic;

namespace BikercityApp.Models.Catalogo.ModeloServicio
{
    public class CatalogoPaginationResponse
    {
        public int records { get; set; }
        public int total { get; set; }
        public List<DetalleProductoModel> rows { get; set; }
    }


}
