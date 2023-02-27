using System.Data.Entity;

namespace QuizPop.DAL
{
    public class Repository<TEntity> where TEntity : Type, IEntity
    {
        public void Add(TEntity entity)
        {
            using var context = new QuizPopContext();
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            using var context = new QuizPopContext();
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            using var context = new QuizPopContext();
            context.Entry(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public TEntity GetById(int id)
        {
            using var context = new QuizPopContext();
            return context.Set<TEntity>().Find(id);
        }

        public List<TEntity> GetAll()
        {
            using var context = new QuizPopContext();
            return context.Set<TEntity>().ToList();
        }
    }
}
