using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoLimpioCore.Data;
using ProyectoLimpioCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#region PROCEDIMIENTOS

//create procedure insertarcoche
//(@marca nvarchar(50), @modelo nvarchar(50), @conductor nvarchar(50), @imagen nvarchar(50))
//as
//    declare @idMaximo int
//    select @idMaximo =  max(idcoche) +1  from coches
//   insert into coches values(@idMaximo, @marca, @modelo, @conductor, @imagen)
//go

#endregion

namespace ProyectoLimpioCore.Repositories
{
    public class RepositoryCochesSQL: IRepositoryCoches
    {
        HospitalContext Context;

        public RepositoryCochesSQL(HospitalContext context)
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

        public void InsertarCoche(String modelo, String marca, String conductor, String imagen)
        {
            String sql = "insertarcoche @modelo, @marca, @conductor, @imagen";
            SqlParameter parammodelo = new SqlParameter("@modelo", modelo);
            SqlParameter parammarca = new SqlParameter("@marca", marca);
            SqlParameter paramconduc = new SqlParameter("@conductor", conductor);
            SqlParameter paramimagen = new SqlParameter("@imagen", imagen);
            this.Context.Database.ExecuteSqlRaw(sql, parammodelo, parammarca, paramconduc, paramimagen);
        }

        public void ModificarCoche(Coche c)
        {
            Coche coche = this.BuscarCoche(c.IdCoche);
            coche.Marca = c.Marca;
            coche.Modelo = c.Modelo;
            coche.Conductor = c.Conductor;
            coche.Imagen = c.Imagen;
            this.Context.SaveChanges();
        }

        public void EliminarCoche(int id)
        {
            Coche coche = this.BuscarCoche(id);
            this.Context.Coches.Remove(coche);
            this.Context.SaveChanges();
        }

        public List<Coche> BuscarMarca(String marca)
        {
            var consulta = from datos in this.Context.Coches
                           where datos.Marca.ToLower().Contains(marca.ToLower())
                           select datos;
            return consulta.ToList();
        }
    }
}
