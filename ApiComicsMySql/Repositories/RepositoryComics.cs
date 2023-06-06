using ApiComicsMySql.Data;
using ApiComicsMySql.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiComicsMySql.Repositories
{
    public class RepositoryComics
    {
        private ComicsContext context;

        public RepositoryComics(ComicsContext context)
        {
            this.context = context;
        }

        public async Task<List<Comic>> GetComicsAsync()
        {
            return await this.context.Comics.ToListAsync();
        }

        public async Task<Comic> FindComicAsync(int id)
        {
            return await this.context.Comics.
                FirstOrDefaultAsync(x => x.IdComic == id);
        }

        private async Task<int> GetMaxIdComicAsync()
        {
            return await this.context.Comics.MaxAsync(x => x.IdComic) + 1;
        }

        public async Task CreateComic(string nombre, string imagen)
        {
            Comic com = new Comic
            {
                IdComic = await this.GetMaxIdComicAsync(),
                Nombre = nombre,
                Imagen = imagen
            };
            this.context.Comics.Add(com);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateComic(int id, string nombre, string imagen)
        {
            Comic com = await this.FindComicAsync(id);
            com.Nombre = nombre;
            com.Imagen = imagen;
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteComic(int id)
        {
            Comic com = await this.FindComicAsync(id);
            this.context.Comics.Remove(com);
            await this.context.SaveChangesAsync();
        }
    }
}
