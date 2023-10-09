using api.Database;
using api.Models;
using api.Models.Dto;

namespace api.Repositories;

public class UserRepository : BaseRepository<User, UserDto>
{
    public UserRepository(DatabaseContext db) : base(db)
    {
    }

    public override User Create(UserDto input)
    {
        var newUser = this.db.Users.Add(new User
        {
            FullName = input.FullName,
            Email = input.Email,
            AvatarUrl = input.AvatarUrl,
        });
        this.db.SaveChanges();
        return newUser.Entity;
    }

    public override User Delete(int id)
    {
        var user = this.db.Users.Find(id) ?? throw new Exception("user not found");
        this.db.Users.Remove(user);
        this.db.SaveChanges();
        return user;
    }

    public override IEnumerable<User> FindAll() => this.db.Users;

    public override User FindById(int id) => this.db.Users.Find(id);

    public override User Update(int id, UserDto input)
    {
        var user = this.db.Users.Find(id) ?? throw new Exception("user not found");

        if (input.FullName != "" || input.FullName != user.FullName)
        {
            user.FullName = input.FullName;
        }

        if (input.Email != "" || input.Email != user.Email)
        {
            user.Email = input.Email;
        }

        if (input.AvatarUrl != null && input.AvatarUrl != "" || input.AvatarUrl != user.AvatarUrl)
        {
            user.AvatarUrl = input.AvatarUrl;
        }

        this.db.Users.Update(user);
        this.db.SaveChanges();
        return user;
    }

    public User FindOrCreate(UserDto input)
    {
        try
        {
            var user = this.db.Users.Where(u => u.Email == input.Email).First();
            return user;
        }
        catch (Exception)
        {
            return this.Create(input);
        }
    }
}
