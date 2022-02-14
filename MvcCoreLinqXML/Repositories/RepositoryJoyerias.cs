using MvcCoreLinqXML.Models;
using MvcCoreLinqXML.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCoreLinqXML.Repositories
{
    public class RepositoryJoyerias
    {
        XDocument document;

        public RepositoryJoyerias(PathProviders providers)
        {
            string filename = "joyerias.xml";
            string path = providers.MapPath(filename,Folders.Documents);
            this.document = XDocument.Load(path);
        }

        public List<Joyeria> GetJoyerias()
        {
            var consulta = from datos in this.document.Descendants("joyeria")
                           select datos;
            List<Joyeria> joyerias = new List<Joyeria>();
            foreach(XElement elem in consulta)
            {
                Joyeria joyeria = new Joyeria();
                //PARA ACCEDER A UNA ETIQUETA UTILIZAMOS Element
                //PARA ACCEDER A UN ATRIBUTO UTILIZAMOS Attribute
                joyeria.Nombre = elem.Element("nombrejoyeria").Value;
                joyeria.CIF = elem.Attribute("cif").Value;
                joyeria.Telefono = elem.Element("telf").Value;
                joyeria.Direccion = elem.Element("direccion").Value;
                joyerias.Add(joyeria);
            }
            return joyerias;
        }
    }
}
