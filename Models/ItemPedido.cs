using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiendaMotores.Models
{
    public class ItemPedido
    {

        public int idOrd
        {
            get;
            set;
        }
        public Producto product
        {
            get;
            set;
        }
        public int Cantidad
        {
            get; 
            set;
        }
    }
}