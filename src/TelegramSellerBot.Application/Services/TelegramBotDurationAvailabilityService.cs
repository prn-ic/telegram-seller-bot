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
        private readonly ITelegramBotDurationAvailabilityRepository _telegramDurationAvailabilityRepository;
        private readonly ITelegramBotRepository _telegramBotRepository;
        private readonly ITelegramBotDurationAvailabilityRepository _availabilityRepository;
        private readonly IMapper _mapper;

        public TelegramBotDurationAvailabilityService(
            ITelegramBotDurationAvailabilityRepository telegramDurationAvailabilityRepository,
            ITelegramBotRepository telegramBotRepository,
            IMapper mapper
,
            ITelegramBotDurationAvailabilityRepository availabilityRepository)
        {
            _telegramDurationAvailabilityRepository = telegramDurationAvailabilityRepository;
            _telegramBotRepository = telegramBotRepository;
            _mapper = mapper;
            _availabilityRepository = availabilityRepository;
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
                new(service, request.Duration, request.Cost);
            var result = await _telegramDurationAvailabilityRepository.AddAsync(
                telegramBotDuration,
                cancellationToken
            );

            return _mapper.Map<TelegramBotDurationAvailabilityDto>(result);
        }

        public async Task<IEnumerable<TelegramBotDurationAvailabilityDto>> GetAsync(
            Guid serviceId,
            CancellationToken cancellationToken = default
        )
        {
            var result = (
                await _telegramDurationAvailabilityRepository.GetAsync(cancellationToken)
            ).Where(x => x.TelegramBot!.Id == serviceId);
            return _mapper.Map<IEnumerable<TelegramBotDurationAvailabilityDto>>(result);
        }

        public async Task<TelegramBotDurationAvailabilityDto> GetAsync(
            int id,
            CancellationToken cancellationToken = default
        )
        {
            var result = await _telegramDurationAvailabilityRepository.GetAsync(
                id,
                cancellationToken
            );
            return _mapper.Map<TelegramBotDurationAvailabilityDto>(result);
        }

        public async Task<TelegramBotDurationAvailabilityDto> GetAsync(Guid serviceId, int durationId, CancellationToken cancellationToken = default)
        {
            TelegramBotDurationAvailability availability =
                await  _availabilityRepository.GetAsync(
                    serviceId,
                    durationId,
                    cancellationToken
                ) ?? throw new InvalidRequestException("The availability wasn't found");

            return _mapper.Map<TelegramBotDurationAvailabilityDto>(availability);
        }

        public async Task RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            await _telegramDurationAvailabilityRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
