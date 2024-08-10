using AutoMapper;
using TelegramSellerBot.Core.Contracts;
using TelegramSellerBot.Core.Dtos;
using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Core.Exceptions;
using TelegramSellerBot.Core.Repositories;

namespace TelegramSellerBot.Application.Services
{
    public class TelegramBotService : ITelegramBotService
    {
        private readonly ITelegramBotRepository _telegramBotRepository;
        private readonly IMapper _mapper;

        public TelegramBotService(ITelegramBotRepository telegramBotRepository, IMapper mapper)
        {
            _telegramBotRepository = telegramBotRepository;
            _mapper = mapper;
        }

        public async Task<TelegramBotDto> AddAsync(
            TelegramBotDto telegramBotDto,
            CancellationToken cancellationToken = default
        )
        {
            TelegramBot telegramBot =
                new(
                    telegramBotDto.Name!,
                    telegramBotDto.Description!,
                    telegramBotDto.TelegramBotLink!
                );

            var result = await _telegramBotRepository.AddAsync(telegramBot, cancellationToken);

            return _mapper.Map<TelegramBotDto>(result);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            TelegramBot telegramBot =
                await _telegramBotRepository.GetAsync(id, cancellationToken)
                ?? throw new InvalidRequestException("The service wasn't found");

            await _telegramBotRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<TelegramBotDto> GetAsync(
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            TelegramBot telegramBot =
                await _telegramBotRepository.GetAsync(id, cancellationToken)
                ?? throw new InvalidRequestException("The service wasn't found");

            return _mapper.Map<TelegramBotDto>(telegramBot);
        }

        public async Task<IEnumerable<TelegramBotDto>> GetAsync(
            CancellationToken cancellationToken = default
        )
        {
            var result = await _telegramBotRepository.GetAsync(cancellationToken);
            return _mapper.Map<IEnumerable<TelegramBotDto>>(result);
        }
    }
}
