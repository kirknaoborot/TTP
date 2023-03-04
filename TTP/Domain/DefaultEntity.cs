using Microsoft.EntityFrameworkCore;
using TTP.Models;

namespace TTP.Domain
{
    public class DefaultEntity
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _configuration;

        public DefaultEntity(IConfiguration configuration)
        {
            _configuration = configuration;

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("TTPConnection"));

            _context = new ApplicationContext(optionsBuilder.Options);           
        }

        public async Task AddDefultEntity()
        {
            var services = new List<Service>
            {
                new Service("images/services/6.png", "Аренда стола", "300"),
                new Service("images/services/3.png", "Детские групповые тренировки", "500"),
                new Service("images/services/8.png", "Взрослые групповые тренировки продвинутого уровня", "600"),
                new Service("images/services/1.png", "Индивидуальные тренировки детей и взрослых", "от 1000"),
                new Service("images/services/2.png", "Разбор матча", "400"),
                new Service("images/services/5.png", "Организация турнира", "15000"),
                new Service("images/services/7.png", "Тренер на турнир", "1300"),
            };

            var coaches = new List<Coach>
            {
                new Coach("images/coaches/kraev_2.png", "Краев Валерий", "Главный тренер. Тренер групповых, индивидуальных тренировок", "Мастер спорта"),
                new Coach("images/coaches/olonov.png", "Олонов Александр", "Тренер групповых, индивидуальных тренировок", "Мастер спорта"),
                new Coach("images/coaches/shemelin.png", "Шемелин Егор", "Тренер групповых, индивидуальных тренировок", "Кандидат в мастера спорта"),
                new Coach("images/coaches/baldova.png", "Балдова Надежда", "Тренер начальной подготовки. Групповые, индивидульные тренировки", "Кандидат в мастера спорта"),
            };

            _context.Services.AddRange(services);
            _context.Coachs.AddRange(coaches);

            await _context.SaveChangesAsync();
        }
    }
}
