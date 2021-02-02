using ProyectoLimpioCore.Helpers;
using ProyectoLimpioCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProyectoLimpioCore.Repositories
{
    public class RepositoryCochesXML : IRepositoryCoches
    {
        PathProvider PathProvider;
        private XDocument docxml;
        private String path;

        public RepositoryCochesXML(PathProvider pathprovider)
        {
            this.PathProvider = pathprovider;
            this.path = this.PathProvider.MapPath("coches.xml", Folders.Documents);
            this.docxml = XDocument.Load(this.path);
        }

        public List<Coche> GetCoches()
        {
            var consulta = from datos in this.docxml.Descendants("coche")
                           select new Coche
                           {
                               IdCoche = int.Parse(datos.Element("idcoche").Value)
                               ,
                               Marca = datos.Element("marca").Value
                               ,
                               Modelo = datos.Element("modelo").Value
                               ,
                               Conductor = datos.Element("conductor").Value
                               ,
                               Imagen = datos.Element("imagen").Value
                           };
            return consulta.ToList();
        }

        public Coche BuscarCoche(int id)
        {
            var consulta = from datos in this.docxml.Descendants("coche")
                           .Where(x => x.Element("idcoche").Value == id.ToString())
                           select new Coche
                           {
                               IdCoche = int.Parse(datos.Element("idcoche").Value)
                               ,
                               Marca = datos.Element("marca").Value
                               ,
                               Modelo = datos.Element("modelo").Value
                               ,
                               Conductor = datos.Element("conductor").Value
                               ,
                               Imagen = datos.Element("imagen").Value
                           };
            return consulta.FirstOrDefault();
        }

        private XElement GetXElementCoche(int id)
        {
            var consulta = from datos in this.docxml.Descendants("coche")
                           where datos.Element("idcoche").Value == id.ToString()
                           select datos;
            return consulta.FirstOrDefault();
        }

        private int GetMaxXElementCoche()
        {
            int id = (from datos in this.docxml.Descendants("coche")
                      select int.Parse(datos.Element("idcoche").Value)).Max() + 1;
            return id;
        }

        public void InsertarCoche(string modelo, string marca, string conductor, string imagen)
        {
            int id = this.GetMaxXElementCoche();
            XElement elemcoche = new XElement("coche");
            elemcoche.Add(new XElement("idcoche", id), new XElement("marca", marca)
                , new XElement("modelo", modelo), new XElement("conductor", conductor)
                , new XElement("imagen", imagen));
            this.docxml.Element("coches").Add(elemcoche);
            this.docxml.Save(this.path);
        }

        public void ModificarCoche(Coche coche)
        {
            XElement elemcoche = this.GetXElementCoche(coche.IdCoche);
            elemcoche.Element("marca").Value = coche.Marca;
            elemcoche.Element("modelo").Value = coche.Modelo;
            elemcoche.Element("conductor").Value = coche.Conductor;
            elemcoche.Element("imagen").Value = coche.Imagen;
            this.docxml.Save(this.path);
        }

        public void EliminarCoche(int id)
        {
            XElement elemcoche = this.GetXElementCoche(id);
            elemcoche.Remove();
            this.docxml.Save(this.path);
        }

        public List<Coche> BuscarMarca(string marca)
        {
            var consulta = from datos in this.docxml.Descendants("coche")
                           where datos.Element("marca").Value.ToLower().Contains(marca.ToLower())
                           select new Coche
                           {
                               IdCoche = int.Parse(datos.Element("idcoche").Value)
                               ,
                               Marca = datos.Element("marca").Value
                               ,
                               Modelo = datos.Element("modelo").Value
                               ,
                               Conductor = datos.Element("conductor").Value
                               ,
                               Imagen = datos.Element("imagen").Value
                           };
            return consulta.ToList();
        }
    }
}
