using ProyectoLimpioCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoLimpioCore.Repositories
{
    public interface IRepositoryCoches
    {
        List<Coche> GetCoches();
        Coche BuscarCoche(int id);
        void InsertarCoche(String modelo, String marca, String conductor, String imagen);
        void ModificarCoche(Coche coche);
        void EliminarCoche(int id);
        List<Coche> BuscarMarca(String marca);
    }
}
