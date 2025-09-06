using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Application.DTOs.Responses.Order;
using EcommerceSystem.Application.Features.Order.Commands;
using EcommerceSystem.Application.Interfaces;
using EcommerceSystem.Application.Interfaces.Repositories;
using EcommerceSystem.Domain.Common.Enum;
using MediatR;

namespace EcommerceSystem.Application.Features.Order.Handlers
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public UpdateOrderHandler(
            IOrderRepository orderRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var dto = request.Order;
                var existingOrder = await _orderRepository.GetByIdAsync(dto.OrderId);
                if (existingOrder == null)
                {
                    throw new Exception($"Order with ID {dto.OrderId} not found.");
                }

                //Parse string -> enum
                if (!Enum.TryParse<OrderStatus>(dto.Status, true, out var newStatus))
                {
                    throw new Exception($"Invalid order status: {dto.Status}");
                }
                existingOrder.Status = newStatus;

                if (!string.IsNullOrEmpty(dto.ShippingAddress))
                {
                    existingOrder.ShippingAddress = dto.ShippingAddress;
                }
                if (!string.IsNullOrEmpty(dto.PaymentMethod))
                {
                    existingOrder.PaymentMethod = dto.PaymentMethod;
                }
                var updatedOrder = await _orderRepository.UpdateAsync(existingOrder);
                await _unitOfWork.CommitTransactionAsync();
                return new OrderResponse
                {
                    OrderId = updatedOrder.OrderId,
                    CustomerId = updatedOrder.CustomerId,
                    TotalAmount = updatedOrder.TotalAmount,
                    Status = updatedOrder.Status,
                    ShippingAddress = updatedOrder.ShippingAddress,
                    PaymentMethod = updatedOrder.PaymentMethod,
                    PhoneNumber = updatedOrder.PhoneNumber,
                    CreatedAt = updatedOrder.CreatedAt,
                    Details = updatedOrder.OrderDetails.Select(d => new OrderDetailResponse
                    {
                        ProductId = d.ProductId,
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice
                    }).ToList()
                };
            }
            finally
            {
                _unitOfWork.RollbackTransactionAsync();
            }
        }
    }
}
