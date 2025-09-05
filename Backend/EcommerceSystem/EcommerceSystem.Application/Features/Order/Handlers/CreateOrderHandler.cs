using AutoMapper;
using EcommerceSystem.Application.DTOs.Responses.Order;
using EcommerceSystem.Application.Features.Order.Commands;
using EcommerceSystem.Application.Interfaces;
using EcommerceSystem.Application.Interfaces.Repositories;
using EcommerceSystem.Domain.Common.Enum;
using EcommerceSystem.Domain.Entities;
using MediatR;

namespace EcommerceSystem.Application.Features.Order.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVoucherRepository _voucherRepository;

        public CreateOrderHandler(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IVoucherRepository voucherRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _voucherRepository = voucherRepository;
        }

        public async Task<OrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = request.Order;
            decimal totalAmount = 0;
            var orderDetails = new List<OrderDetailEntity>();

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                // 1. Tính tổng tiền từ sản phẩm
                foreach (var item in order.Items)
                {
                    var product = await _productRepository.GetByIdAsync(item.ProductId);
                    if (product == null || product.Stock < item.Quantity)
                    {
                        throw new Exception($"Product {item.ProductId} not available or not enough stock");
                    }

                    var unitPrice = product.Price;
                    totalAmount += unitPrice * item.Quantity;

                    product.Stock -= item.Quantity;
                    await _productRepository.UpdateAsync(product);

                    orderDetails.Add(new OrderDetailEntity
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = unitPrice
                    });
                }

                // 2. Áp dụng voucher nếu có
                decimal discountAmount = 0;
                if (order.VoucherId.HasValue)
                {
                    var voucher = await _voucherRepository.GetByIdAsync(order.VoucherId.Value);
                    if (voucher == null || !voucher.IsActive || voucher.ExpiredAt < DateTime.UtcNow)
                    {
                        throw new Exception("Voucher is not valid");
                    }

                    discountAmount = totalAmount * (voucher.Discount / 100);
                }

                decimal finalAmount = totalAmount - discountAmount;

                // 3. Tạo order
                var orders = new OrderEntity
                {
                    CustomerId = order.CustomerId,
                    TotalAmount = finalAmount,
                    Status = OrderStatus.Pending,
                    ShippingAddress = order.ShippingAddress,
                    PaymentMethod = order.PaymentMethod,
                    PhoneNumber = order.PhoneNumber,
                    CreatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified),
                    OrderDetails = orderDetails
                };

                var createdOrder = await _orderRepository.AddAsync(orders);

                await _unitOfWork.CommitTransactionAsync();

                // 4. Trả về response
                return new OrderResponse
                {
                    OrderId = createdOrder.OrderId,
                    CustomerId = createdOrder.CustomerId,
                    TotalAmount = createdOrder.TotalAmount,
                    Status = createdOrder.Status,
                    ShippingAddress = createdOrder.ShippingAddress,
                    PaymentMethod = createdOrder.PaymentMethod,
                    PhoneNumber = createdOrder.PhoneNumber,
                    CreatedAt = createdOrder.CreatedAt,
                    Details = createdOrder.OrderDetails.Select(od => new OrderDetailResponse
                    {
                        ProductId = od.ProductId,
                        Quantity = od.Quantity,
                        UnitPrice = od.UnitPrice
                    }).ToList()
                };
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
