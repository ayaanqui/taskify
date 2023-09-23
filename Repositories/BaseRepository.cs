using api.Database;
using api.Models;

namespace api.Repositories;

public abstract class BaseRepository<Model, ModelDto> where Model : BaseModel
{
    public readonly DatabaseContext db;

    public BaseRepository(DatabaseContext db)
    {
        this.db = db;
    }

    public abstract Model Create(ModelDto input);

    public abstract Model Update(int id, ModelDto input);

    public abstract IEnumerable<Model> FindAll();

    public abstract Model FindById(int id);

    public abstract Model Delete(int id);
}
