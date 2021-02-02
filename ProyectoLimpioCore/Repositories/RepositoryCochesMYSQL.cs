using ProyectoLimpioCore.Data;
using ProyectoLimpioCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoLimpioCore.Repositories
{
    public class RepositoryCochesMYSQL : IRepositoryCoches
    {
        HospitalContext Context;

        public RepositoryCochesMYSQL(HospitalContext context)
        {
            this.Context = context;
        }

        public List<Coche> GetCoches()
        {
            return this.Context.Coches.ToList();
        }

        public Coche BuscarCoche(int id)
        {
            return this.Context.Coches.Where(z => z.IdCoche == id).FirstOrDefault();
        }

        private int GetMaxIdCoche()
        {
            int id = (from datos in this.Context.Coches
                      select datos.IdCoche).Max() + 1;
            return id;
        }

        public void InsertarCoche(string modelo, string marca, string conductor, string imagen)
        {
            int id = this.GetMaxIdCoche();
            Coche coche = new Coche();
            coche.IdCoche = id;
            coche.Marca = marca;
            coche.Modelo = modelo;
            coche.Conductor = conductor;
            coche.Imagen = imagen;
            this.Context.Coches.Add(coche);
            this.Context.SaveChanges();
        }

        public void ModificarCoche(Coche coche)
        {
            Coche c = this.BuscarCoche(coche.IdCoche);
            c.Marca = coche.Marca;
            c.Modelo = coche.Modelo;
            c.Conductor = coche.Conductor;
            c.Imagen = coche.Imagen;
            this.Context.SaveChanges();
        }

        public void EliminarCoche(int id)
        {
            Coche c = this.BuscarCoche(id);
            this.Context.Coches.Remove(c);
            this.Context.SaveChanges();
        }

        public List<Coche> BuscarMarca(string marca)
        {
            var consulta = from datos in this.Context.Coches
                           where datos.Marca.ToLower().Contains(marca.ToLower())
                           select datos;
            return consulta.ToList();
        }
    }
}
