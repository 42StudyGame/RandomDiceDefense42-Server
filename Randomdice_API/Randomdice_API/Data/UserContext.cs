using Microsoft.EntityFrameworkCore;
using RandomDice_Login.Models;

namespace Randomdice_API.Data
{
    public class UserContext : DbContext // DB와 상호작용하는 게이트웨이 역할.
    {
        // 외부코드()가 구성을 전달할 수 있고, 동일한 DbContext코드를 테스트코드-프로덕션코드-타공급자끼리도 사용 가능 ->?????
        public UserContext (DbContextOptions<UserContext> options) : base(options)
        {

        }

        // DB에 생성될 테이블 들. 테이블의 이름은 기본적으로 탬플릿 인자의 것+s형태를 띄는게 좋...다?
        public DbSet<UserModel> UserModels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().ToTable("t_user");
        }
    }
}
