using System.ComponentModel.DataAnnotations;

namespace QuizPop.DAL
{
    public interface IEntity
    {
        [Key]
        public int Id { get; init; }
    }
}
