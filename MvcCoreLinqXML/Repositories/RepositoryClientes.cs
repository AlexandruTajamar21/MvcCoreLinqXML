using MvcCoreLinqXML.Models;
using MvcCoreLinqXML.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCoreLinqXML.Repositories
{
    public class RepositoryClientes
    {
        private XDocument document;
        private string path;

        public RepositoryClientes(PathProviders pathprov)
        {
            this.path = pathprov.MapPath("ClientesID.xml", Folders.Documents);
            this.document = XDocument.Load(this.path);
        }

        public List<Cliente> GetClientes()
        {
            var consulta = from datos in this.document.Descendants("CLIENTE")
                           select datos;
            List<Cliente> listClientes = new List<Cliente>();

            foreach(XElement dato in consulta)
            {
                Cliente cliente = new Cliente();
                cliente.IdCliente = int.Parse(dato.Element("IDCLIENTE").Value);
                cliente.Nombre = dato.Element("NOMBRE").Value;
                cliente.Direccion = dato.Element("DIRECCION").Value;
                cliente.Email = dato.Element("EMAIL").Value;
                cliente.Imagen = dato.Element("IMAGENCLIENTE").Value;
                listClientes.Add(cliente);
            }
            return listClientes;
        }

        public Cliente FindCliente(int id)
        {
            var consulta = from datos in this.document.Descendants("CLIENTE")
                           where datos.Element("IDCLIENTE").Value == id.ToString()
                           select datos;

            XElement dato = consulta.FirstOrDefault();
            Cliente cliente = new Cliente{
                IdCliente = int.Parse(dato.Element("IDCLIENTE").Value),
                Nombre = dato.Element("NOMBRE").Value,
                Direccion = dato.Element("DIRECCION").Value,
                Email = dato.Element("EMAIL").Value,
                Imagen = dato.Element("IMAGENCLIENTE").Value
            };
            return cliente;
        }

        private XElement FindXElementCliente(string idCliente)
        {
            var consulta = from datos in this.document.Descendants("CLIENTE")
                           where datos.Element("IDCLIENTE").Value == idCliente
                           select datos;
            return consulta.FirstOrDefault();
        }

        public void DeleteCliente(int id)
        {
            XElement xElement = this.FindXElementCliente(id.ToString());
            xElement.Remove();
            this.document.Save(this.path);
        }

        public void UpdateCliente(int id, string nombre, string direccion, string email, string imagen)
        {
            XElement xElement = this.FindXElementCliente(id.ToString());
            xElement.Element("NOMBRE").Value = nombre;
            xElement.Element("DIRECCION").Value = direccion;
            xElement.Element("EMAIL").Value = email;
            xElement.Element("IMAGENCLIENTE").Value = imagen;
            this.document.Save(this.path);
        }

        public void AddClient(int id, string nombre, string direccion, string email, string imagen)
        {
            XElement root = new XElement("CLIENTE");
            root.Add(new XElement("IDCLIENTE", id));
            root.Add(new XElement("NOMBRE", nombre));
            root.Add(new XElement("DIRECCION", direccion));
            root.Add(new XElement("EMAIL", email));
            root.Add(new XElement("IMAGENCLIENTE", imagen));
            this.document.Element("CLIENTES").Add(root);
            this.document.Save(this.path);
        }
    }
}
