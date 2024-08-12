using AutoMapper;
using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Contracts;
using TelegramSellerBot.Core.Dtos;
using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Core.Exceptions;
using TelegramSellerBot.Core.Repositories;
using TelegramSellerBot.Core.Requests;

namespace TelegramSellerBot.Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ITelegramBotRepository _telegramBotRepository;
        private readonly ITelegramBotDurationAvailabilityRepository _availabilityRepository;
        private readonly ISubscriptionHistoryRepository _subscriptionHistoryRepository;

        private readonly IMapper _mapper;

        public SubscriptionService(
            ISubscriptionRepository subscriptionRepository,
            ITelegramBotRepository telegramBotRepository,
            IMapper mapper,
            ITelegramBotDurationAvailabilityRepository availabilityRepository
,
            ISubscriptionHistoryRepository subscriptionHistoryRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _telegramBotRepository = telegramBotRepository;
            _mapper = mapper;
            _availabilityRepository = availabilityRepository;
            _subscriptionHistoryRepository = subscriptionHistoryRepository;
        }

        public async Task<SubscriptionDto> CreateAsync(
            CreateSubscriptionRequest request,
            CancellationToken cancellationToken = default
        )
        {
            TelegramBot telegramBot =
                await _telegramBotRepository.GetAsync(request.TelegramServiceId)
                ?? throw new InvalidRequestException("The service wasn't found");

            Subscription subscription = new(request.TelegramUserId, telegramBot, request.Duration);
            var result = await _subscriptionRepository.AddAsync(subscription, cancellationToken);

            TelegramBotDurationAvailability availability =
                await _availabilityRepository.GetAsync(
                    telegramBot.Id,
                    (int)request.Duration,
                    cancellationToken
                ) ?? throw new InvalidRequestException("The availability wasn't found");

            SubscriptionHistory history = new SubscriptionHistory(
                subscription,
                telegramBot,
                availability.Cost,
                result.StatusId,
                DateTime.UtcNow
            );

            await _subscriptionHistoryRepository.AddAsync(history, cancellationToken);

            return _mapper.Map<SubscriptionDto>(result);
        }

        public async Task DeclineAsync(
            Guid subscriptionId,
            CancellationToken cancellationToken = default
        )
        {
            Subscription subscription =
                await _subscriptionRepository.GetAsync(subscriptionId, cancellationToken)
                ?? throw new InvalidRequestException("The subscription wasn't found");

            subscription.StatusId = SubscriptionStatuses.Inactive;
            await _subscriptionRepository.UpdateAsync(subscription, cancellationToken);
        }

        public async Task<SubscriptionDto> GetAsync(
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            var result =
                await _subscriptionRepository.GetAsync(id, cancellationToken)
                ?? throw new InvalidRequestException("The subscription wasn't found");

            return _mapper.Map<SubscriptionDto>(result);
        }

        public async Task<IEnumerable<SubscriptionDto>> GetAsync(
            string userId,
            CancellationToken cancellationToken = default
        )
        {
            var result = await _subscriptionRepository.GetAsync(cancellationToken);
            result = result.Where(s => s.TelegramUserId == userId);

            return _mapper.Map<IEnumerable<SubscriptionDto>>(result);
        }

        public async Task<SubscriptionDto> UpdateAsync(
            UpdateSubscriptionRequest request,
            CancellationToken cancellationToken = default
        )
        {
            Subscription subscription =
                await _subscriptionRepository.GetAsync(request.SubscriptionId, cancellationToken)
                ?? throw new InvalidRequestException("The subscription wasn't found");

            subscription.DurationId = request.Duration;
            subscription.ModifiedAt = DateTime.UtcNow;
            subscription.StatusId = request.Status;
            var result = await _subscriptionRepository.UpdateAsync(subscription, cancellationToken);

            return _mapper.Map<SubscriptionDto>(result);
        }
    }
}
