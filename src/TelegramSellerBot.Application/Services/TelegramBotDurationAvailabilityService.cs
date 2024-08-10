using AutoMapper;
using TelegramSellerBot.Core.Contracts;
using TelegramSellerBot.Core.Dtos;
using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Core.Exceptions;
using TelegramSellerBot.Core.Repositories;
using TelegramSellerBot.Core.Requests;
using TelegramSellerBot.Persistense.Repositories;

namespace TelegramSellerBot.Application.Services
{
    public class TelegramBotDurationAvailabilityService : ITelegramBotDurationAvailabilityService
    {
        private readonly TelegramBotDurationAvailabilityRepository _telegramDurationAvailabilityService;
        private readonly ITelegramBotRepository _telegramBotRepository;
        private readonly IMapper _mapper;

        public TelegramBotDurationAvailabilityService(
            TelegramBotDurationAvailabilityRepository telegramDurationAvailabilityService,
            ITelegramBotRepository telegramBotRepository,
            IMapper mapper
        )
        {
            _telegramDurationAvailabilityService = telegramDurationAvailabilityService;
            _telegramBotRepository = telegramBotRepository;
            _mapper = mapper;
        }

        public async Task<TelegramBotDurationAvailabilityDto> AddAsync(
            CreateTelegramBotDurationAvailabilityRequest request,
            CancellationToken cancellationToken = default
        )
        {
            TelegramBot service =
                await _telegramBotRepository.GetAsync(request.ServiceId, cancellationToken)
                ?? throw new InvalidRequestException("The service wasn't found");

            TelegramBotDurationAvailability telegramBotDuration =
                new(service.Id, request.Duration, request.Cost);
            var result = await _telegramDurationAvailabilityService.AddAsync(
                telegramBotDuration,
                cancellationToken
            );

            return _mapper.Map<TelegramBotDurationAvailabilityDto>(result);
        }

        public async Task RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            await _telegramDurationAvailabilityService.DeleteAsync(id, cancellationToken);
        }
    }
}
