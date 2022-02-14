using MvcCoreLinqXML.Models;
using MvcCoreLinqXML.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCoreLinqXML.Repositories
{
    public class RepositoryPeliculas
    {
        private XDocument document;
        private string path;
        private PathProviders pathprov;


        public RepositoryPeliculas(PathProviders pathprov)
        {
            this.pathprov = pathprov;
            this.path = pathprov.MapPath("peliculas.xml", Folders.Documents);
            this.document = XDocument.Load(this.path);
        }

        public List<Pelicula> GetPeliculas()
        {
            this.path = pathprov.MapPath("peliculas.xml", Folders.Documents);
            this.document = XDocument.Load(this.path);

            var consulta = from datos in this.document.Descendants("pelicula")
                           select datos;
            List<Pelicula> listaPeliculas = new List<Pelicula>();

            foreach (XElement dato in consulta)
            {
                Pelicula peli = new Pelicula();
                peli.idPelicula = int.Parse(dato.Attribute("idpelicula").Value);
                peli.Titulo = dato.Element("titulo").Value;
                peli.TituloOriginal = dato.Element("titulooriginal").Value;
                peli.Descripcion = dato.Element("descripcion").Value;
                peli.Poster = dato.Element("poster").Value;
                listaPeliculas.Add(peli);
            }
            return listaPeliculas;
        }

        public List<Escena> GetEscenasPelicula(int id)
        {
            this.path = pathprov.MapPath("escenas.xml", Folders.Documents);
            this.document = XDocument.Load(this.path);

            var consulta = from datos in this.document.Descendants("escena")
                           where datos.Attribute("idpelicula").Value == id.ToString()
                           select datos;
            List<Escena> listaEscenas = new List<Escena>();

            foreach (XElement dato in consulta)
            {
                Escena escena = new Escena();
                escena.idPelicula = int.Parse(dato.Attribute("idpelicula").Value);
                escena.Tituloescena = dato.Element("tituloescena").Value;
                escena.Descripcion = dato.Element("descripcion").Value;
                escena.Imagen = dato.Element("imagen").Value;
                listaEscenas.Add(escena);
            }
            return listaEscenas;
        }
    }
}
