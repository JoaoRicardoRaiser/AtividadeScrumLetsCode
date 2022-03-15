using AtividadeScrumLetsCode.Entities;
using System.IO;

namespace AtividadeScrumLetsCode.Repositories
{
    public class SalaRepository: GenericRepository<Sala>
    {

        public SalaRepository()
        {
            Host = Directory.GetCurrentDirectory() + @"..\..\..\..\Database\salas.json";
        }

        public void Create(Sala sala)
        {
            var database = GetDatabase();
            database.Add(sala);
            UpdateDatabase(database);
        }
    }
}
