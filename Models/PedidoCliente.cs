using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiendaMotores.Models
{
    public class PedidoCliente
    {
        private TIENDAEntities db = new TIENDAEntities();
        private List<Compra> detalle_compra;
        public PedidoCliente()
        {
            detalle_compra = db.Compra.ToList();
        }
        
    }
}