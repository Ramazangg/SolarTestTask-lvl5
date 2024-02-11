using AutoMapper;
using Org.BouncyCastle.Utilities;
using SolarTestTask_lvl5.Contracts.User;
using SolarTestTask_lvl5.Domain;
using System.Timers;

namespace SolarTestTask_lvl5.AppData.Contexts
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly Mail.IMailService _mailService;

        public UserService(IUserRepository userRepository,IMapper mapper,Mail.IMailService mailService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _mailService = mailService;
        }


        public async Task<Guid> CreateUserAsync(CreateUserRequest createUser, byte[] photo, CancellationToken cancellation)
        {
            var user = new User();
            user.FIO = createUser.FIO;
            user.email = createUser.email;
            user.Photo = photo;
            user.Id = Guid.NewGuid();
            user.BirthDate = DateTime.Parse(createUser.BirthDate).ToUniversalTime();

            await _userRepository.AddAsync(user, cancellation);

            System.Timers.Timer timer = new System.Timers.Timer(Math.Abs((DateTime.Now - user.BirthDate).Milliseconds));
            timer.Elapsed += async (object? sender, ElapsedEventArgs e)
                => {await _mailService.SendMessage($"{user.FIO}, от всего сердца поздравляем вас с днем рождения!"
                    ,user.email
                    ,cancellation);};
            timer.Start();
            timer.AutoReset = false;
            

            return user.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            var user = await _userRepository.FindById(id,cancellation);

            await _userRepository.DeleteAsync(user, cancellation);
        }

        public async Task<InfoUserResponse> EditUserAsync(Guid Id, EditUserRequest edit, CancellationToken cancellation)
        {
            var existingUser = await _userRepository.FindById(Id, cancellation);
            await _userRepository.EditUserAsync(_mapper.Map(edit, existingUser), cancellation);

            return _mapper.Map<InfoUserResponse>(existingUser);


        }

        public async Task<IReadOnlyCollection<InfoUserResponse>> GetAll()
        {
            return _userRepository.GetAll().Select(u=>new InfoUserResponse
            {
                Id = u.Id,
                BirthDate = u.BirthDate,
                email = u.email,
                FIO = u.FIO,
                Photo = u.Photo
            }).ToList();
        }

        public async Task<InfoUserResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            return _mapper.Map<InfoUserResponse>(await _userRepository.FindById(id, cancellation));
        }
    }
}
