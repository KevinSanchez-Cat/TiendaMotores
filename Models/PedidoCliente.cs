using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiendaMotores.Models
{
    public class PedidoCliente
    {
        private ContextoTienda db = new ContextoTienda();
        private List<Detalle_compra> detalle_compra;

        public PedidoCliente()
        {
            detalle_compra = db.Detalle_compra.ToList();

        }
        public Compra compra {
            get; 
            set;
        }
        public string fecha
        {
            get;set;
        }
        public string envio
        {
            get; set;

        }
        public string estatus
        {
            get;
            set;
        }
        public string total
        {
            get;
            set;

        }
        public List<Detalle_compra> compraProductos
        {
            get;
            set;
        }

        
    }
}